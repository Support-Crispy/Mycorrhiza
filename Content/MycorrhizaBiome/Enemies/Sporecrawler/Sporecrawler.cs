using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mycorrhiza.Content.MycorrhizaBiome.Enemies.Sporecrawler
{
    public class Sporecrawler : ModNPC
    {
        private const int STATE_WALK = 0;
        private const int STATE_WINDUP = 1;
        private const int STATE_LEAP = 2;
        private const int STATE_LANDING = 3;

        private const float WalkSpeed = 2f;
        private const float LeapDistance = 200f;
        private const float LeapSpeedX = 8f;
        private const float LeapSpeedY = -10f;
        private const int LeapCooldownMax = 120;
        private const int WindupDuration = 56;
        private const int LandingPauseDuration = 40;

        private ref float AIState => ref NPC.ai[0];
        private ref float AITimer => ref NPC.ai[1];
        private ref float LeapCooldown => ref NPC.ai[2];

        private int _animationTimer;
        private int _currentFrame;

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 7;
        }

        public override void SetDefaults()
        {
            NPC.width = 42;
            NPC.height = 30;
            NPC.damage = 20;
            NPC.defense = 5;
            NPC.lifeMax = 80;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = 100f;
            NPC.knockBackResist = 0.3f;
            NPC.aiStyle = -1;
        }

        public override void AI()
        {
            Player target = Main.player[NPC.target];
            NPC.TargetClosest(true);

            if (LeapCooldown > 0)
                LeapCooldown--;

            switch ((int)AIState)
            {
                case STATE_WALK:
                    Walk(target);
                    break;
                case STATE_WINDUP:
                    Windup();
                    break;
                case STATE_LEAP:
                    Leap();
                    break;
                case STATE_LANDING:
                    Landing();
                    break;
            }

            if (!NPC.noGravity)
            {
                NPC.velocity.Y += 0.4f;
                if (NPC.velocity.Y > 16f)
                    NPC.velocity.Y = 16f;
            }
        }

        private void Walk(Player target)
        {
            NPC.direction = target.Center.X > NPC.Center.X ? 1 : -1;
            NPC.spriteDirection = NPC.direction;

            float distanceToPlayer = Vector2.Distance(NPC.Center, target.Center);
            bool onGround = NPC.velocity.Y == 0f;

            if (distanceToPlayer <= LeapDistance && onGround && LeapCooldown <= 0)
            {
                NPC.velocity.X = 0f;
                AIState = STATE_WINDUP;
                AITimer = 0;
                return;
            }
            NPC.velocity += NPC.DirectionTo(target.Center)*0.1f;
            Collision.StepUp(ref NPC.position, ref NPC.velocity , NPC.width, NPC.height, ref NPC.stepSpeed, ref NPC.gfxOffY);
            NPC.velocity.X = WalkSpeed * NPC.direction;
        }

        private void Windup()
        {
            NPC.velocity.X = 0f;
            AITimer++;

            if (AITimer >= WindupDuration)
            {
                AIState = STATE_LEAP;
                AITimer = 0;
            }
        }

        private void Leap()
        {
            Player target = Main.player[NPC.target];

            if (AITimer == 0)
            {
                NPC.direction = target.Center.X > NPC.Center.X ? 1 : -1;
                NPC.spriteDirection = NPC.direction;
                NPC.velocity.X = LeapSpeedX * NPC.direction;
                NPC.velocity.Y = LeapSpeedY;
                NPC.noGravity = false;
            }

            AITimer++;

            if (AITimer > 10 && NPC.velocity.Y >= 0f)
            {
                AIState = STATE_LANDING;
                AITimer = 0;
            }
        }

        private void Landing()
        {
            NPC.velocity.X *= 0.85f;
            AITimer++;

            if (AITimer >= LandingPauseDuration)
            {
                AIState = STATE_WALK;
                AITimer = 0;
                LeapCooldown = LeapCooldownMax;
            }
        }

        public override void FindFrame(int frameHeight)
        {
            _animationTimer++;
            int column = 0;
            int row = 0;

            switch ((int)AIState)
            {
                case STATE_WALK:
                    if (NPC.velocity.X == 0f)
                    {
                        column = 0;
                        row = 0;
                        _animationTimer = 0;
                    }
                    else
                    {
                        column = 1;
                        if (_animationTimer >= 6)
                        {
                            _animationTimer = 0;
                            _currentFrame++;
                            if (_currentFrame > 4)
                                _currentFrame = 0;
                        }
                        row = _currentFrame;
                    }
                    break;

                case STATE_WINDUP:
                    column = 3;
                    {
                        int ticksPerFrame = WindupDuration / 7;
                        row = (int)(AITimer / ticksPerFrame);
                        if (row > 6) row = 6;
                    }
                    break;

                case STATE_LEAP:
                    if (NPC.velocity.Y < 0f)
                    {
                        column = 4;
                        row = 0;
                    }
                    else
                    {
                        column = 2;
                        row = 0;
                    }
                    break;

                case STATE_LANDING:
                    column = 5;
                    {
                        int ticksPerFrame = LandingPauseDuration / 5;
                        row = (int)(AITimer / ticksPerFrame);
                        if (row > 4) row = 4;
                    }
                    break;
            }

            NPC.frame.X = column * 42;
            NPC.frame.Y = row * frameHeight;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            int frameWidth = 42;
            int frameHeight = 30;

            Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;

            Rectangle sourceRect = new Rectangle(
                NPC.frame.X,
                NPC.frame.Y,
                frameWidth,
                frameHeight
            );

            Vector2 drawOrigin = new Vector2(frameWidth / 2f, frameHeight / 2f);
            Vector2 drawPos = NPC.Center - screenPos + new Vector2(0, NPC.gfxOffY);

            SpriteEffects effects = NPC.spriteDirection == -1
                ? SpriteEffects.FlipHorizontally
                : SpriteEffects.None;

            spriteBatch.Draw(
                texture,
                drawPos,
                sourceRect,
                drawColor,
                NPC.rotation,
                drawOrigin,
                NPC.scale,
                effects,
                0f
            );

            return false;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.Player.InModBiome<MycorrhizaBiome>() && spawnInfo.SpawnTileY <= Main.worldSurface ? 0.3f : 0f;
        }
    }
}