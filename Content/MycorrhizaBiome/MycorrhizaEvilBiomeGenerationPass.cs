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

            // Step 1: Base Biome Conversion Loop
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

            // Step 2: Foliage & Ambient Object Generation Loop
            ushort grassType = (ushort)ModContent.TileType<MoldyGrassPlaced>();
            ushort smallPlantType = (ushort)ModContent.TileType<MycorrhizaSmallPlants>();
            ushort largePlantType = (ushort)ModContent.TileType<MycorrhizaLargePlants>();
            int ambientObjectType = ModContent.TileType<MycorrhizaObjects>();

            for (int x = evilBiomePositionWestBound + 5; x < evilBiomePositionEastBound - 5; x++)
            {
                for (int y = (int)GenVars.worldSurfaceLow; y < Main.worldSurface + 60; y++)
                {
                    if (Main.tile[x, y].HasTile && Main.tile[x, y].TileType == grassType && !Main.tile[x, y - 1].HasTile)
                    {
                        // Boosted density: 70% chance to attempt placing decoration on an empty grass block
                        if (WorldGen.genRand.NextBool(70, 100))
                        {
                            int roll = WorldGen.genRand.Next(100);

                            if (roll < 40)
                            {
                                // Small Plants (40% weight)
                                WorldGen.PlaceTile(x, y - 1, smallPlantType, mute: true, forced: false, style: WorldGen.genRand.Next(7));
                            }
                            else if (roll < 65)
                            {
                                // Large Plants (25% weight)
                                if (!Main.tile[x, y - 2].HasTile)
                                {
                                    WorldGen.PlaceTile(x, y - 1, largePlantType, mute: true, forced: false, style: WorldGen.genRand.Next(12));
                                }
                            }
                            else
                            {
                                // Custom Ambient Object Debris (35% weight)
                                bool clear = true;
                                for (int xOffset = -1; xOffset <= 1; xOffset++)
                                {
                                    for (int yOffset = -2; yOffset <= -1; yOffset++)
                                    {
                                        if (Main.tile[x + xOffset, y + yOffset].HasTile)
                                        {
                                            clear = false;
                                            break;
                                        }
                                    }
                                }

                                if (clear)
                                {
                                    int randomStyle = WorldGen.genRand.Next(5);
                                    WorldGen.PlaceObject(x, y - 1, ambientObjectType, mute: true, style: randomStyle);
                                }
                            }
                        }
                    }
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

        // Step 3: Clean up vanilla forest object intrusions
        public override void PostGenerateEvil()
        {
            if (WorldBiomeGeneration.EvilBiomeGenRanges.Count > 0)
            {
                var lastBounds = WorldBiomeGeneration.EvilBiomeGenRanges[^1];
                ushort customGrass = (ushort)ModContent.TileType<MoldyGrassPlaced>();

                for (int x = lastBounds.Left; x < lastBounds.Right; x++)
                {
                    for (int y = lastBounds.Top; y < lastBounds.Bottom; y++)
                    {
                        if (Main.tile[x, y].HasTile)
                        {
                            ushort currentType = Main.tile[x, y].TileType;

                            // Correct fields: SmallPiles, LargePiles, LargePiles2
                            if (currentType == TileID.SmallPiles || currentType == TileID.LargePiles || currentType == TileID.LargePiles2)
                            {
                                // Check if this vanilla debris is resting directly on your custom moldy grass
                                if (y < Main.maxTilesY - 2 && Main.tile[x, y + 1].HasTile && Main.tile[x, y + 1].TileType == customGrass)
                                {
                                    // Delete it cleanly without spawning items
                                    WorldGen.KillTile(x, y, noItem: true);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}