using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mycorrhiza.Content.MycorrhizaBiome.Enemies.Moldslime
{
    public class Moldslime : ModNPC
    {
        private int _animationTimer;

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 4;
        }

        public override void SetDefaults()
        {
            NPC.width = 56;
            NPC.height = 38;
            NPC.damage = 40;
            NPC.defense = 8;
            NPC.lifeMax = 200;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = 200f;
            NPC.knockBackResist = 0.1f;
            NPC.aiStyle = NPCAIStyleID.Slime;
            AIType = NPCID.BlueSlime;
            NPC.friendly = false;
            NPC.homeless = false;
        }

        public override void AI()
        {
            NPC.TargetClosest(true);
        }

        public override void FindFrame(int frameHeight)
        {
            if (NPC.velocity.Y == 0f)
            {
                if (NPC.frame.Y == frameHeight * 3)
                {
                    NPC.frame.Y = 0;
                    _animationTimer = 0;
                }

                _animationTimer++;
                if (_animationTimer >= 16)
                {
                    _animationTimer = 0;
                    NPC.frame.Y = NPC.frame.Y == 0 ? frameHeight : 0;
                }
            }
            else if (NPC.velocity.Y < 0f)
            {
                NPC.frame.Y = frameHeight * 2;
                _animationTimer = 0;
            }
            else
            {
                NPC.frame.Y = frameHeight * 3;
                _animationTimer = 0;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;

            int frameWidth = 56;
            int frameHeight = texture.Height / 4;

            Rectangle sourceRect = new Rectangle(0, NPC.frame.Y, frameWidth, frameHeight);

            Vector2 drawOrigin = new Vector2(frameWidth / 2f, frameHeight);
            Vector2 drawPos = new Vector2(NPC.Center.X, NPC.Bottom.Y) - screenPos + new Vector2(0, NPC.gfxOffY);

            SpriteEffects effects = NPC.spriteDirection == 1
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
            return spawnInfo.Player.InModBiome<MycorrhizaBiome>() && spawnInfo.SpawnTileY <= Main.worldSurface ? 0.2f : 0f;
        }
    }
}