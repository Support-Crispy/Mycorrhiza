using BreadLibrary.Common.Whip;
using BreadLibrary.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Mycorrhiza.Content.MycorrhizaBiome.HyphumEquipment
{
    public class FusariumWhip : BaseWhipProjectile
    {
        private readonly List<int> hitEnemies = new();

        public override void SetDefaults()
        {
            Projectile.DefaultToWhip();

            Projectile.WhipSettings.Segments = 12;
            Projectile.WhipSettings.RangeMultiplier = 1f;
        }

        public override void Prepare()
        {
            hitEnemies.Clear();

            Projectile.Opacity = 0;
            Projectile.WhipSettings.Segments = 6;
            Projectile.WhipSettings.RangeMultiplier = 1f;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Owner.MinionAttackTargetNPC = target.whoAmI;

            if (hitEnemies.Contains(target.whoAmI))
                return;

            hitEnemies.Add(target.whoAmI);

            Projectile.damage = (int)(Projectile.damage * 0.6f);
        }

    
        protected override Asset<Texture2D> WhipHead => ModContent.Request<Texture2D>("Mycorrhiza/Content/MycorrhizaBiome/HyphumEquipment/FusariumWhip_Head");
        protected override Asset<Texture2D> WhipHandle => ModContent.Request<Texture2D>(this.GetPath()+"_Handle");
        protected override void drawHead(Vector2 HeadPos, float BaseRotation, List<Vector2> list, SpriteEffects flip, Color LightColor)
        {
            flip = SpriteEffects.FlipVertically;

            var tex = WhipHead.Value;
            HeadPos = list[^1];
            BaseRotation = list[^1].AngleFrom(list[list.Count - 2]) - MathHelper.PiOver2;
            Main.NewText(_HeadScaleAmount);
            Main.EntitySpriteDraw(tex, HeadPos - Main.screenPosition, null, LightColor, BaseRotation, HeadOrigin(Vector2.Zero), _HeadScaleAmount*5, 0) ;
        }
        protected override void DrawHandle(List<Vector2> list, SpriteEffects flip)
        {
            var drawpos = list[0] - Main.screenPosition;
            var tex = WhipHandle.Value;
            float rot = list[0].AngleTo(list[1]) - MathHelper.PiOver2;

            Main.EntitySpriteDraw(tex, drawpos, null, Color.White, rot, HandleOrigin(_Offset), 0.75f, flip);
        }

        private BasicEffect HeadEffect;
        protected override void DrawOverPrimitive(List<Vector2> points)
        {
            if (Main.netMode == NetmodeID.Server)
                return;


            if (points.Count < 12)
                return;

            Texture2D tex = ModContent.Request<Texture2D>(this.GetPath() + "_Head_Pre").Value;

            EnsureHeadEffect(tex);

            // Draw over the few segments before the head.
            // You can tweak this range.
            int startIndex = points.Count - 12;
            int endIndex = points.Count - 1;

            for (int i = startIndex; i < endIndex; i++)
            {
                DrawTexturedSegmentQuad(
                    points[i],
                    points[i + 1],
                    tex,
                    width: 28f,
                    color: Color.White
                );
            }
        }
        private void EnsureHeadEffect(Texture2D texture)
        {
            GraphicsDevice gd = Main.graphics.GraphicsDevice;

            HeadEffect ??= new BasicEffect(gd)
            {
                TextureEnabled = true,
                VertexColorEnabled = true,
                LightingEnabled = false,
                World = Matrix.Identity
            };

            HeadEffect.Texture = texture;
            HeadEffect.View = Main.GameViewMatrix.TransformationMatrix;
            HeadEffect.Projection = Matrix.CreateOrthographicOffCenter(
                0,
                Main.screenWidth,
                Main.screenHeight,
                0,
                -1f,
                1f
            );
            HeadEffect.World = Matrix.Identity;
        }
        private void DrawTexturedSegmentQuad(Vector2 start, Vector2 end, Texture2D texture, float width, Color color)
        {
            Vector2 direction = end - start;

            if (direction.LengthSquared() <= 0.001f)
                return;

            direction.Normalize();

            Vector2 normal = direction.RotatedBy(MathHelper.PiOver2);

            Vector2 startLeft = start - normal * width * 0.1f;
            Vector2 startRight = start + normal * width * 0.1f;

            Vector2 endLeft = end - normal * width * 0.1f;
            Vector2 endRight = end + normal * width * 0.1f;

            VertexPositionColorTexture[] vertices =
                    {
                // Since your texture was 90 degrees off before, this keeps the swapped UV layout.
                new VertexPositionColorTexture(
                    new Vector3(startLeft - Main.screenPosition, 0f),
                    color,
                    new Vector2(0f, 0f)
                ),

                new VertexPositionColorTexture(
                    new Vector3(startRight - Main.screenPosition, 0f),
                    color,
                    new Vector2(1f, 0f)
                ),

                new VertexPositionColorTexture(
                    new Vector3(endLeft - Main.screenPosition, 0f),
                    color,
                    new Vector2(0f, 1f)
                ),

                new VertexPositionColorTexture(
                    new Vector3(endRight - Main.screenPosition, 0f),
                    color,
                    new Vector2(1f, 1f)
                )
            };

                    short[] indices =
                    {
                0, 1, 2,
                1, 3, 2
            };

            GraphicsDevice gd = Main.graphics.GraphicsDevice;

            gd.RasterizerState = RasterizerState.CullNone;
            gd.SamplerStates[0] = SamplerState.PointClamp;
            gd.Textures[0] = texture;

            foreach (EffectPass pass in HeadEffect.CurrentTechnique.Passes)
            {
                pass.Apply();

                gd.DrawUserIndexedPrimitives(
                    PrimitiveType.TriangleList,
                    vertices,
                    0,
                    vertices.Length,
                    indices,
                    0,
                    2
                );
            }
        }
        private static List<Vector2> ResamplePolyline(List<Vector2> points, float spacing)
        {
            List<Vector2> result = new();
            if (points.Count < 2)
                return result;

            result.Add(points[0]);

            Vector2 prev = points[0];
            float carry = 0f;

            for (int i = 1; i < points.Count; i++)
            {
                Vector2 curr = points[i];
                Vector2 delta = curr - prev;
                float length = delta.Length();

                if (length <= 0.0001f)
                    continue;

                Vector2 dir = delta / length;

                float dist = spacing - carry;
                while (dist <= length)
                {
                    Vector2 sample = prev + dir * dist;
                    result.Add(sample);
                    dist += spacing;
                }

                carry = length - (dist - spacing);
                prev = curr;
            }

            // Ensure the tip is included
            if (result[^1] != points[^1])
                result.Add(points[^1]);

            return result;
        }


        private List<Vector2> SmoothWhipSection(List<Vector2> points, int subdivisionsPerSegment)
        {
            List<Vector2> smoothed = new();

            if (points.Count < 2)
                return smoothed;

            for (int i = 0; i < points.Count - 1; i++)
            {
                Vector2 p0 = points[Math.Max(i - 1, 0)];
                Vector2 p1 = points[i];
                Vector2 p2 = points[i + 1];
                Vector2 p3 = points[Math.Min(i + 2, points.Count - 1)];

                for (int j = 0; j < subdivisionsPerSegment; j++)
                {
                    float t = j / (float)subdivisionsPerSegment;
                    smoothed.Add(Vector2.CatmullRom(p0, p1, p2, p3, t));
                }
            }

            smoothed.Add(points[^1]);
            return smoothed;
        }

        private void DrawTexturedWhipStrip(List<Vector2> points, Texture2D texture, float width, Color color)
        {
            if (points.Count < 2)
                return;

            GraphicsDevice gd = Main.graphics.GraphicsDevice;

            HeadEffect ??= new BasicEffect(gd)
            {
                TextureEnabled = true,
                VertexColorEnabled = true,
                LightingEnabled = false,
                World = Matrix.Identity
            };

            HeadEffect.Texture = texture;
            HeadEffect.View = Main.GameViewMatrix.TransformationMatrix;
            HeadEffect.Projection = Matrix.CreateOrthographicOffCenter(
                0,
                Main.screenWidth,
                Main.screenHeight,
                0,
                -1f,
                1f
            );
            HeadEffect.World = Matrix.Identity;

            VertexPositionColorTexture[] vertices = new VertexPositionColorTexture[points.Count * 2];

            float totalLength = GetTotalLength(points);
            float lengthSoFar = 0f;

            for (int i = 0; i < points.Count; i++)
            {
                Vector2 current = points[i];

                Vector2 direction;

                if (i == 0)
                    direction = points[i + 1] - points[i];
                else if (i == points.Count - 1)
                    direction = points[i] - points[i - 1];
                else
                    direction = points[i + 1] - points[i - 1];

                if (direction.LengthSquared() <= 0.001f)
                    direction = Vector2.UnitY;

                direction.Normalize();

                Vector2 normal = direction.RotatedBy(MathHelper.PiOver2);

                if (i > 0)
                    lengthSoFar += Vector2.Distance(points[i - 1], points[i]);

                float u = totalLength <= 0f ? 0f : lengthSoFar / totalLength;

                Vector2 left = current - normal * width * 0.1f;
                Vector2 right = current + normal * width * 0.1f;

                vertices[i * 2] = new VertexPositionColorTexture(
     new Vector3(left - Main.screenPosition, 0f),
     color,
     new Vector2(0f, u)
 );

                vertices[i * 2 + 1] = new VertexPositionColorTexture(
                    new Vector3(right - Main.screenPosition, 0f),
                    color,
                    new Vector2(1f, u)
                );
            }

            short[] indices = new short[(points.Count - 1) * 6];

            for (int i = 0; i < points.Count - 1; i++)
            {
                int vertexIndex = i * 2;
                int index = i * 6;

                indices[index] = (short)vertexIndex;
                indices[index + 1] = (short)(vertexIndex + 1);
                indices[index + 2] = (short)(vertexIndex + 2);

                indices[index + 3] = (short)(vertexIndex + 1);
                indices[index + 4] = (short)(vertexIndex + 3);
                indices[index + 5] = (short)(vertexIndex + 2);
            }

            gd.RasterizerState = RasterizerState.CullNone;
            gd.SamplerStates[0] = SamplerState.PointClamp;

            foreach (EffectPass pass in HeadEffect.CurrentTechnique.Passes)
            {
                pass.Apply();

                gd.DrawUserIndexedPrimitives(
                    PrimitiveType.TriangleList,
                    vertices,
                    0,
                    vertices.Length,
                    indices,
                    0,
                    indices.Length / 3
                );
            }
        }
        private static float GetTotalLength(List<Vector2> points)
        {
            float length = 0f;

            for (int i = 0; i < points.Count - 1; i++)
                length += Vector2.Distance(points[i], points[i + 1]);

            return length;
        }
        protected override Asset<Texture2D> PrimitiveTex => ModContent.Request<Texture2D>(this.GetPath() + "_Primitive");
        public override float GetWhipWidth(float baseWidth, float t)
        {
            return baseWidth;
        }
        protected override IReadOnlyList<WhipDrawPass> DrawOrder => new[]
        {
            WhipDrawPass.Primitive,
            WhipDrawPass.Handle,
            WhipDrawPass.OverPrimtive,
            WhipDrawPass.Head
        };
    }
}