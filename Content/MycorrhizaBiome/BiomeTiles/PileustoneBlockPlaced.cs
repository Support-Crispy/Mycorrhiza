using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Mycorrhiza.Content.MycorrhizaBiome.BiomeTiles
{
    public class PileustoneBlockPlaced : ModTile
    {

        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;


            AddMapEntry(new Color(255, 255, 255), CreateMapEntryName());
            MineResist = 2f;
            MinPick = 65;
            HitSound = SoundID.Tink;

            DustType = ModContent.DustType<Dusts.MycoStoneDust>();

            TileID.Sets.Corrupt[Type] = true;
            TileID.Sets.Crimson[Type] = true;
            TileID.Sets.Hallow[Type] = true;
        }


        public override void Convert(int i, int j, int conversionType)
        {
            switch (conversionType)
            {
                case BiomeConversionID.Sand:
                case BiomeConversionID.PurificationPowder:
                case BiomeConversionID.Purity:
                    WorldGen.ConvertTile(i, j, TileID.Stone);
                    return;

                case BiomeConversionID.Corruption:
                    WorldGen.ConvertTile(i, j, TileID.Ebonstone);
                    return;

                case BiomeConversionID.Crimson:
                    WorldGen.ConvertTile(i, j, TileID.Crimstone);
                    return;

                case BiomeConversionID.Hallow:
                    WorldGen.ConvertTile(i, j, TileID.Pearlstone);
                    return;
            }

            WorldGen.SquareTileFrame(i, j);
            if (Main.netMode == NetmodeID.MultiplayerClient)
                NetMessage.SendTileSquare(-1, i, j);
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
    }
}