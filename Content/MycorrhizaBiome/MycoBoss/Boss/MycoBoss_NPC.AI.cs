using BreadLibrary.Core;
using BreadLibrary.Core.Utilities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace Mycorrhiza.Content.MycorrhizaBiome.MycoBoss.Boss
{
    public partial class MycoBoss_NPC : ModNPC
    {

        public override bool PreAI()
        {
            return base.PreAI();
        }

        public override void AI()
        {
            CurrentAttack?.Update(this);
            // Movement tuning
            const float desiredHeightAboveGround = 570f; // pixels above ground to hold
            const float baseMaxSpeedX = 12f; // base horizontal speed cap
            const float horizontalProportional = 0.08f; // proportional gain for X
            const float horizontalLerp = 0.12f; // smoothing factor for X velocity (0..1)
            const float baseMaxSpeedY = 10f; // base vertical speed cap
            const float verticalProportional = 0.12f; // proportional gain for Y
            const float verticalLerp = 0.08f; // smoothing factor for Y velocity (0..1)
            const float raycastDistance = 2000f; // how far down to look for ground

            // Speed-scaling parameters (increase speed when far away)
            const float speedIncreaseThresholdX = 300f; // distance at which horizontal speed begins to scale
            const float speedIncreaseThresholdY = 200f; // distance at which vertical speed begins to scale
            const float maxSpeedMultiplier = 2.5f; // maximum multiplier applied to base max speeds

            // Horizontal movement: move smoothly toward player's X
            Vector2 playerPos = Main.LocalPlayer.Center;
            float distanceX = playerPos.X - NPC.Center.X;
            float absDistanceX = Math.Abs(distanceX);

            // Compute horizontal multiplier based on how far we are from the target X
            float horizFactor = absDistanceX / speedIncreaseThresholdX; // 0..inf
            float horizMultiplier = MathHelper.Clamp(horizFactor, 1f, maxSpeedMultiplier);

            float effectiveMaxSpeedX = baseMaxSpeedX * horizMultiplier;
            float targetVelX = MathHelper.Clamp(distanceX * horizontalProportional, -effectiveMaxSpeedX, effectiveMaxSpeedX);
            NPC.velocity.X = MathHelper.Lerp(NPC.velocity.X, targetVelX, horizontalLerp);

            float groundY;
            Vector2 downDirection = Vector2.UnitY;

            // Cast multiple rays in a downward arc and average the hit heights to choose levitation height.
            {
                const int rayCount = 5;
                const float arcDegrees = 60f; // total arc below NPC to sample (centered on straight down)
                const float maxTiltDegrees = 18f; // maximum degrees to tilt ray arc in direction of velocity
                List<float> hitYs = new List<float>(rayCount);

                for (int i = 0; i < rayCount; i++)
                {
                    float t = (rayCount == 1) ? 0.5f : (float)i / (rayCount - 1); // normalized 0..1
                    float angleDeg = -arcDegrees / 2f + t * arcDegrees; // -30..+30 for arcDegrees=60

                    // Tilt the sampling arc slightly in the direction of the NPC's horizontal velocity.
                    // Normalize velocity influence by baseMaxSpeedX and clamp to [-1, 1].
                    float velocityFactor = MathHelper.Clamp(NPC.velocity.X / baseMaxSpeedX, -1f, 1f);
                    float angleOffsetDeg = -velocityFactor * maxTiltDegrees;
                    angleDeg += angleOffsetDeg;

                    float angleRad = MathHelper.ToRadians(angleDeg);
                    Vector2 dir = downDirection.RotatedBy(angleRad);
                    Vector2 rayEnd = NPC.Center + dir * raycastDistance;

                    Point? hit = Utilities.RaycastTo(NPC.Center, rayEnd, debug: false);
                    if (hit.HasValue)
                    {
                        // Convert tile coordinate to world Y
                        float hitWorldY = hit.Value.ToWorldCoordinates().Y;
                        hitYs.Add(hitWorldY);
                    }
                }

                if (hitYs.Count > 0)
                {
                    // Use average of hits to smoothly follow terrain beneath the boss.
                    float sum = 0f;
                    for (int i = 0; i < hitYs.Count; i++)
                        sum += hitYs[i];
                    groundY = sum / hitYs.Count;
                }
                else
                {
                    // Fallback if all raycasts miss: assume ground some distance below the NPC
                    groundY = NPC.Center.Y + 600f;
                }
            }

            float desiredY = groundY - desiredHeightAboveGround;
            float distanceY = desiredY - NPC.Center.Y;
            float absDistanceY = Math.Abs(distanceY);

            // Compute vertical multiplier based on vertical distance to desiredY
            float vertFactor = absDistanceY / speedIncreaseThresholdY;
            float vertMultiplier = MathHelper.Clamp(vertFactor, 1f, maxSpeedMultiplier);

            float effectiveMaxSpeedY = baseMaxSpeedY * vertMultiplier;
            float targetVelY = MathHelper.Clamp(distanceY * verticalProportional, -effectiveMaxSpeedY, effectiveMaxSpeedY);
            NPC.velocity.Y = MathHelper.Lerp(NPC.velocity.Y, targetVelY, verticalLerp);
        }

        public override void PostAI()
        {
            
            
            UpdateTendrils();
        }
        void IMultiSegmentNPC.UpdateSegments()
        {
            foreach (ExtraNPCSegment item in ExtraHitBoxes())
            {
                if (item.ImmuneTime > 0)
                {
                    item.ImmuneTime--;
                }
            }

        }

    }
}
