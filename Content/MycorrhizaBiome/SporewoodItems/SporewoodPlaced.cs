using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Mycorrhiza.Content.MycorrhizaBiome.SporewoodItems
{
    public class SporewoodPlaced : ModTile
    {

        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            Main.tileBlockLight[Type] = true;
            HitSound = SoundID.Dig;
            AddMapEntry(new Color(0, 0, 0));
            DustType = ModContent.DustType<Dusts.SporewoodDust>();
        }
    }
}