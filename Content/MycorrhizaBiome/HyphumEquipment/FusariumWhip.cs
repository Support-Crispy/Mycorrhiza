using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Mycorrhiza.Content.MycorrhizaBiome.HyphumEquipment
{
    public class FusariumWhip : ModProjectile
    {
        private List<int> hitEnemies = new List<int>();

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.IsAWhip[Type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.DefaultToWhip();

            Projectile.WhipSettings.Segments = 6;

            Projectile.WhipSettings.RangeMultiplier = 1f;
        }


        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player owner = Main.player[Projectile.owner];
            owner.MinionAttackTargetNPC = target.whoAmI;

            if (hitEnemies.Contains(target.whoAmI))
                return;

            hitEnemies.Add(target.whoAmI);

            Projectile.damage = (int)(Projectile.damage * 0.6f);
        }

        private void DrawLine(List<Vector2> list)
        {
            Texture2D texture = TextureAssets.FishingLine.Value;
            Rectangle frame = texture.Frame();
            Vector2 origin = new Vector2(frame.Width / 2, 2);

            Vector2 pos = list[0];
            for (int i = 0; i < list.Count - 1; i++)
            {
                Vector2 element = list[i];
                Vector2 diff = list[i + 1] - element;

                float rotation = diff.ToRotation() - MathHelper.PiOver2;
                Color color = Lighting.GetColor(element.ToTileCoordinates(), Color.White);
                Vector2 scale = new Vector2(1, (diff.Length() + 2) / frame.Height);

                Main.EntitySpriteDraw(texture, pos - Main.screenPosition, frame, color, rotation, origin, scale, SpriteEffects.None, 0);

                pos += diff;
            }
        }

        private Rectangle GetSegmentFrame(int index, int totalPoints)
        {
            int frameWidth = 25;
            int handleHeight = 42;
            int middleHeight = 18;
            int tipHeight = 24;

            if (index == 0)
                return new Rectangle(0, 0, frameWidth, handleHeight);

            if (index == totalPoints - 2)
                return new Rectangle(0, 96, frameWidth, tipHeight);

            switch (index)
            {
                case 1: // First middle piece (closest to handle)
                    return new Rectangle(0, 42, frameWidth, middleHeight);
                case 2: // Second middle piece
                    return new Rectangle(0, 58, frameWidth, middleHeight);
                case 4: // Third middle piece (closest to tip)
                    return new Rectangle(0, 76, frameWidth, 22);
                default: // Fallback for any other indices
                    return new Rectangle(0, 42, frameWidth, middleHeight);
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            List<Vector2> controlPoints = new List<Vector2>();
            Projectile.FillWhipControlPoints(Projectile, controlPoints);

            DrawLine(controlPoints);

            SpriteEffects flip = Projectile.spriteDirection < 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            Texture2D texture = TextureAssets.Projectile[Type].Value;

            Vector2 pos = controlPoints[0];

            for (int i = 0; i < controlPoints.Count - 1; i++)
            {
                Rectangle frame = GetSegmentFrame(i, controlPoints.Count);

                Vector2 origin = new Vector2(frame.Width / 2, frame.Height / 2);

                Vector2 diff = controlPoints[i + 1] - controlPoints[i];
                float rotation = diff.ToRotation() - MathHelper.PiOver2;

                Color color = Lighting.GetColor(controlPoints[i].ToTileCoordinates());

                float scale = 1f;

                if (i == controlPoints.Count - 2)
                {
                    //Adds a subtle pulse to the tip (gross)
                    scale = 1f + (float)Math.Sin(Main.GameUpdateCount * 0.1f) * 0.05f;
                }

                Main.EntitySpriteDraw(
                    texture,
                    pos - Main.screenPosition,
                    frame,
                    color,
                    rotation,
                    origin,
                    scale,
                    flip,
                    0
                );

                pos += diff;
            }

            return false; 
        }
    }
}