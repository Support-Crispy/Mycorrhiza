using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Mycorrhiza.Content.MycorrhizaBiome.BiomeTiles.Desert
{
    public class PileusandBlockPlaced : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileBrick[Type] = true;
            Main.tileMergeDirt[Type] = false;
            Main.tileBlockLight[Type] = true;

            Main.tileSand[Type] = true;
            TileID.Sets.Conversion.Sand[Type] = true;
            TileID.Sets.ForAdvancedCollision.ForSandshark[Type] = true;
            TileID.Sets.CanBeDugByShovel[Type] = true;
            TileID.Sets.Falling[Type] = true;
            TileID.Sets.Suffocate[Type] = true;
            TileID.Sets.FallingBlockProjectile[Type] = new TileID.Sets.FallingBlockProjectileInfo(ModContent.ProjectileType<FallingPileusandBall>(), 10);

            TileID.Sets.CanBeClearedDuringOreRunner[Type] = true;
            TileID.Sets.GeneralPlacementTiles[Type] = false;
            TileID.Sets.ChecksForMerge[Type] = true;

            TileID.Sets.Corrupt[Type] = true;
            TileID.Sets.Crimson[Type] = true;
            TileID.Sets.Hallow[Type] = true;

            MineResist = 0.5f;
            DustType = ModContent.DustType<Dusts.MycoSandDust>();
            AddMapEntry(new Color(150, 150, 150));
        }

        public override bool HasWalkDust()
        {
            return Main.rand.NextBool(3);
        }

        public override void WalkDust(ref int dustType, ref bool makeDust, ref Color color)
        {
            dustType = DustID.Sand;
        }

        public override void Convert(int i, int j, int conversionType)
        {
            switch (conversionType)
            {
                case BiomeConversionID.Sand:
                case BiomeConversionID.PurificationPowder:
                case BiomeConversionID.Purity:
                    WorldGen.ConvertTile(i, j, TileID.Sand);
                    return;

                case BiomeConversionID.Corruption:
                    WorldGen.ConvertTile(i, j, TileID.Ebonsand);
                    return;

                case BiomeConversionID.Crimson:
                    WorldGen.ConvertTile(i, j, TileID.Crimsand);
                    return;

                case BiomeConversionID.Hallow:
                    WorldGen.ConvertTile(i, j, TileID.Pearlsand);
                    return;
            }

            WorldGen.SquareTileFrame(i, j);
            if (Main.netMode == NetmodeID.MultiplayerClient)
                NetMessage.SendTileSquare(-1, i, j);
        }
    }
}