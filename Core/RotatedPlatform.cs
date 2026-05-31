using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mycorrhiza.Core
{
    public readonly struct RotatedPlatform
    {
        public readonly Vector2 Center;
        public readonly float Width;
        public readonly float Thickness;
        public readonly float Rotation;

        public RotatedPlatform(Vector2 center, float width, float thickness, float rotation)
        {
            Center = center;
            Width = width;
            Thickness = thickness;
            Rotation = rotation;
        }

        public Vector2 Right => Rotation.ToRotationVector2();

        public Vector2 Up => (Rotation - MathHelper.PiOver2).ToRotationVector2();

        public Vector2 Down => -Up;

        public Vector2 TopCenter => Center + Up * (Thickness * 0.5f);

        public Vector2 GetPointOnTop(float localX)
        {
            return TopCenter + Right * localX;
        }

        public Vector2 WorldToLocal(Vector2 worldPoint)
        {
            Vector2 offset = worldPoint - Center;

            return new Vector2(
                Vector2.Dot(offset, Right),
                Vector2.Dot(offset, Down)
            );
        }

        public bool ContainsFeet(Rectangle feetRect, float extraVerticalTolerance = 8f)
        {
            Vector2 feetCenter = feetRect.Center.ToVector2();

            Vector2 local = WorldToLocal(feetCenter);

            bool insideX = Math.Abs(local.X) <= Width * 0.5f;
            bool nearTop = local.Y >= -Thickness * 0.5f - extraVerticalTolerance &&
                           local.Y <= Thickness * 0.5f + extraVerticalTolerance;

            return insideX && nearTop;
        }

        public Vector2 ProjectFeetToTop(Vector2 feetWorld)
        {
            Vector2 local = WorldToLocal(feetWorld);

            float clampedX = MathHelper.Clamp(local.X, -Width * 0.5f, Width * 0.5f);

            return GetPointOnTop(clampedX);
        }
    }
}
