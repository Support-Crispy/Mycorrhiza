using BreadLibrary.Core;
using BreadLibrary.Core.Graphics;
using BreadLibrary.Core.Utilities;
using Microsoft.Xna.Framework;
using Mycorrhiza.Content.MycorrhizaBiome.MycoBoss.Boss.Attacks;
using Mycorrhiza.Content.MycorrhizaBiome.MycoBoss.Boss.Attacks.Mycorrhiza.Content.MycorrhizaBiome.MycoBoss.Boss.Attacks;
using Mycorrhiza.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Mycorrhiza.Content.MycorrhizaBiome.MycoBoss.Boss
{
    public partial class MycoBoss_NPC : ModNPC
    {

        public override bool PreAI()
        {
            Body.Simulate(NPC.velocity*0.6f, NPC.Center, 0, 0.6f, 10, false, 0, false, 0);


            var referenceSpeed = 12f;
            var maxTilt = MathHelper.ToRadians(30f);
            var normalized = MathHelper.Clamp(NPC.velocity.X / referenceSpeed, -1f, 1f);
            var targetRotation = normalized * maxTilt;
            Point? Hit = Utilities.RaycastTo(NPC.Center, NPC.Center + Vector2.UnitY.RotatedBy(targetRotation) * 3000, debug: false);

            if (Hit.HasValue)
            {

                Body.Positions[^1] = Hit.Value.ToWorldCoordinates();
            }
            if (Body.Positions[^1].Distance(Body.OldPositions[^1] ) > 2)
            {
                for(int i = 0; i< 10; i++)
                {

                    Vector2 SpawnPos = Vector2.Lerp(Body.Positions[^1], Body.OldPositions[^1], Main.rand.NextFloat(0,1));
                    Vector2 Velocity = new Vector2(0, -1).RotatedBy(targetRotation + Main.rand.NextFloat(-1, 1));
                    Dust.NewDustPerfect(SpawnPos, DustID.Dirt, Velocity * Main.rand.NextFloat(0.2f, 4));
                }
            }

            return base.PreAI();
        }

        public override void AI()
        {

            float desiredHeightAboveGround = DesiredHeight;
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

            CurrentAttack ??= _MycoBossAttackRegistry.Create(CurrentState);
            CurrentAttack?.Update(this);

            Timer++;
        }

        public override void PostAI()
        {
            if (!ShouldHoldHeadStill)
            {
                var referenceSpeed = 12f;
                var maxTilt = MathHelper.ToRadians(20f);
                var normalized = MathHelper.Clamp(NPC.velocity.X / referenceSpeed, -1f, 1f);
                var targetRotation = normalized * maxTilt;

                // Slightly lerp rotation toward the horizontal-velocity-based target.
                NPC.rotation = NPC.rotation.AngleLerp(targetRotation, 0.2f);

            }
            _lastHeadPlatform = HeadPlatform;
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
        internal class MycoBossHeadPlatformPlayer : ModPlayer
        {
            public override void PreUpdateMovement()
            {
                if (Player.controlDown)
                    return;

                if (Player.velocity.Y < 0f)
                    return;

                Rectangle feetRect = Player.getRect();
                feetRect.Y += Player.height - 12;
                feetRect.Height = 16;

                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC npc = Main.npc[i];

                    if (!npc.active || npc.type != ModContent.NPCType<MycoBoss_NPC>())
                        continue;

                    if (npc.ModNPC is not MycoBoss_NPC boss)
                        continue;

                    RotatedPlatform platform = boss.HeadPlatform;

                    if (!platform.ContainsFeet(feetRect))
                        continue;

                    boss.UpdateStandingOnHead(Player);
                    break;
                }
            }
        }
    }
}
