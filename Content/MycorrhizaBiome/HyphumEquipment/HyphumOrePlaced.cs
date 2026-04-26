using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Mycorrhiza.Content.MycorrhizaBiome.HyphumEquipment
{
    public class HyphumOrePlaced : ModTile
    {

        public override void SetStaticDefaults()
        {
            Main.tileLighted[Type] = true;
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileOreFinderPriority[Type] = 690; //figure out what demonite's is

            TileID.Sets.Ore[Type] = true;

            Main.tileShine[Type] = 2000;
            Main.tileShine2[Type] = true;

            AddMapEntry(new Color(250, 250, 250), CreateMapEntryName());
            MineResist = 2f;
            MinPick = 55;
            HitSound = SoundID.Tink;
            Main.tileSpelunker[Type] = true;

        }

        public override bool CanExplode(int i, int j)
        {
            return false;
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 174f / 600f;
            g = 213f / 600f;
            b = 129f / 600f;
        }
    }
}