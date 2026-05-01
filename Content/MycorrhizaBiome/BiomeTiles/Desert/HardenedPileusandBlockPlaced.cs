using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Mycorrhiza.Content.MycorrhizaBiome.BiomeTiles.Desert
{
    public class HardenedPileusandBlockPlaced : ModTile
    {

        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;


            AddMapEntry(new Color(255, 255, 255), CreateMapEntryName());
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