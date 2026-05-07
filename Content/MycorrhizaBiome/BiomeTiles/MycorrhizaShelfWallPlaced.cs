using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Mycorrhiza.Content.MycorrhizaBiome.BiomeTiles
{
    public class MycorrhizaShelfWallPlaced : ModWall
    {
        public override void SetStaticDefaults()
        {
            Main.wallHouse[Type] = true;

            VanillaFallbackOnModDeletion = WallID.DiamondGemspark;

            AddMapEntry(new Color(150, 150, 150));

            DustType = ModContent.DustType<Dusts.MycoStoneDust>();
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
    }
}