
using BreadLibrary.Core.Graphics.Particles;
using BreadLibrary.Core.Graphics.Pixelation;
using BreadLibrary.Core.Graphics.Spritebatch;
using BreadLibrary.Core.Utilities;

namespace Mycorrhiza.Content.Particles
{
    internal class MushBoom : BaseParticle<MushBoom>
    {
        public Vector2 Position;
        public Vector2 Rotation;
        public Vector2 Velocity;
        public int TimeLeft;
        public float MaxTime;

        public void Prepare(Vector2 position, Vector2 Velocity, float rotation, int timeLeft)
        {
            this.Velocity = Velocity;
            Position = position;
            Rotation = rotation.ToRotationVector2();
            MaxTime = Math.Max(1, timeLeft);
            TimeLeft = timeLeft;
        }

        public override void Update(ref ParticleRendererSettings settings)
        {
            float rot = Rotation.ToRotation();
            rot += 0.1f;
            Rotation = rot.ToRotationVector2();
            Position += Collision.TileCollision(Position, Velocity, 1, 1, true);

            Velocity *= 0.98f;
            if (TimeLeft-- <= 0)
            {
                ShouldBeRemovedFromRenderer = true;
            }
        }

        public override void Draw(ref ParticleRendererSettings settings, SpriteBatch spritebatch)
        {
            Texture2D tex = Assets.Textures.Misc.smoke_07_a.Asset.Value;

            float maxTime = MaxTime <= 0f ? 1f : MaxTime;
            float lifeProgress = MathHelper.Clamp((maxTime - TimeLeft) / maxTime, 0f, 1f);

            // Phase thresholds
            const float growEnd = 0.3f;
            const float holdEnd = 0.7f;

            // Scale and alpha parameters
            const float minScale = 0.01f;
            const float peakScale = 0.06f;

            float scale;
            float alpha;

            // Smoothstep function for nicer interpolation
            static float SmoothStep(float t)
            {
                t = MathHelper.Clamp(t, 0f, 1f);
                return t * t * (3f - 2f * t);
            }

            if (lifeProgress <= growEnd)
            {
                float t = growEnd <= 0f ? 1f : lifeProgress / growEnd;
                float s = SmoothStep(t);
                scale = MathHelper.Lerp(minScale, peakScale, s);
                alpha = 1f;
            }
            else if (lifeProgress <= holdEnd)
            {
                scale = peakScale;
                alpha = 1f;
            }
            else
            {
                float denom = 1f - holdEnd;
                float t = denom <= 0f ? 1f : (lifeProgress - holdEnd) / denom;
                float s = SmoothStep(t);
                scale = MathHelper.Lerp(peakScale, 0f, s);
                alpha = MathHelper.Lerp(1f, 0f, s);
            }

            Vector2 drawPos = Position - Main.screenPosition;
            Vector2 Origin = tex.Size() / 2f;
            Color color = Color.Lerp(Color.Blue, Color.White, lifeProgress) * alpha;

            var cap = spritebatch.Capture();
            cap.BlendState = BlendState.Additive;
            spritebatch.End();
            spritebatch.Begin(cap);
            Main.EntitySpriteDraw(
                tex,
                drawPos,
                null,
                color,
                Rotation.ToRotation(),
                Origin,
                scale,
                SpriteEffects.None,
                0
            );
            spritebatch.End();
            spritebatch.Begin(cap);
        }
    }
}
