
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Mycorrhiza.Content.MycorrhizaBiome.HyphalSparks
{
    public class LivingHyphalFireBlockPlaced : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileLighted[Type] = true;

            TileID.Sets.CanPlaceNextToNonSolidTile[Type] = true;

            AddMapEntry(new Color(LivingHyphalFireBlock.LightColor));

            AnimationFrameHeight = 90;
        }
        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = LivingHyphalFireBlock.LightColor.X;
            g = LivingHyphalFireBlock.LightColor.Y;
            b = LivingHyphalFireBlock.LightColor.Z;
        }
        public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
        {
            offsetY = 2;
        }

        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            frame = Main.tileFrame[TileID.LivingFire];

            /* This is how it would be done manually, spending 5 ticks on each of 4 frames, looping.
			if (++frameCounter >= 5) {
				frameCounter = 0;
				frame = ++frame % 4;
			}
			*/
        }
    }
}