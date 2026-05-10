using AltLibrary.Core.Generation;
using AltLibrary.Common.Systems;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using Mycorrhiza.Content.MycorrhizaBiome.Plants;
using Mycorrhiza.Content.MycorrhizaBiome.BiomeTiles;
using Mycorrhiza.Content.MycorrhizaBiome.BiomeTiles.Desert;
using System;

namespace Mycorrhiza.Content.MycorrhizaBiome
{
    public class MycorrhizaEvilBiomeGenerationPass : EvilBiomeGenerationPass
    {
        public override string ProgressMessage => "Spreading mold...";

        public override void GenerateEvil(int evilBiomePosition, int evilBiomePositionWestBound, int evilBiomePositionEastBound)
        {
            double depth = Main.worldSurface + 40.0;

            for (int x = evilBiomePositionWestBound; x < evilBiomePositionEastBound; x++)
            {
                depth += WorldGen.genRand.Next(-2, 3);
                depth = Math.Clamp(depth, Main.worldSurface + 30.0, Main.worldSurface + 50.0);

                bool hitGround = false;
                int y = (int)GenVars.worldSurfaceLow;

                while (y < depth)
                {
                    Tile tile = Main.tile[x, y];
                    if (tile.HasTile)
                    {
                        if (!hitGround && y < Main.worldSurface - 1.0)
                        {
                            if (tile.TileType == TileID.Dirt)
                            {
                                WorldGen.grassSpread = 0;
                                WorldGen.SpreadGrass(x, y, TileID.Dirt, ModContent.TileType<MoldyGrassPlaced>(), true);
                            }
                        }

                        hitGround = true;

                        if (tile.TileType == TileID.Stone)
                            tile.TileType = (ushort)ModContent.TileType<PileustoneBlockPlaced>();
                        else if (tile.TileType == TileID.Sand)
                            tile.TileType = (ushort)ModContent.TileType<PileusandBlockPlaced>();
                        else if (tile.TileType == TileID.HardenedSand)
                            tile.TileType = (ushort)ModContent.TileType<HardenedPileusandBlockPlaced>();
                        else if (tile.TileType == TileID.Sandstone)
                            tile.TileType = (ushort)ModContent.TileType<PileusandstoneBlockPlaced>();
                    }
                    y++;
                }
            }

            int worldSurfaceLow = (int)GenVars.worldSurfaceLow;
            WorldBiomeGeneration.EvilBiomeGenRanges.Add(new Microsoft.Xna.Framework.Rectangle(
                evilBiomePositionWestBound,
                worldSurfaceLow,
                evilBiomePositionEastBound - evilBiomePositionWestBound,
                (int)GenVars.worldSurfaceHigh - worldSurfaceLow + 500
            ));
        }

        public override void PostGenerateEvil() { }
    }
}