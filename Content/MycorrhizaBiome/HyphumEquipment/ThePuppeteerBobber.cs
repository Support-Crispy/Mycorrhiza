using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Mycorrhiza.Content.MycorrhizaBiome.HyphumEquipment
{
    public class ThePuppeteerBobber : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.BobberWooden);
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.aiStyle = 61;
            Projectile.lavaWet = true;
            DrawOriginOffsetY = -8;
        }

        public override void ModifyFishingLine(ref Vector2 lineOriginOffset, ref Color lineColor)
        {
            lineOriginOffset = new Vector2(42f, -32f);
            lineColor = new Color(166, 201, 207);
        }
    }
}