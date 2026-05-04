using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Mycorrhiza.Content.MycorrhizaBiome.Dusts
{
    public class MycoMoldDust : ModDust
    {
        public override void SetStaticDefaults()
        {

            Main.tileSolid[Type] = true;
            UpdateType = DustID.Stone;
        }
    }
}