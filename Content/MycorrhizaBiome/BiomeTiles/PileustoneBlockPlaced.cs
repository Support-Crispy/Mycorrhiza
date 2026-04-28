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
            Main.tileLighted[Type] = true;
            Main.tileSolid[Type] = true;


            AddMapEntry(new Color(250, 250, 250), CreateMapEntryName());
            MineResist = 2f;
            MinPick = 65;
            HitSound = SoundID.Tink;

        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
    }
}