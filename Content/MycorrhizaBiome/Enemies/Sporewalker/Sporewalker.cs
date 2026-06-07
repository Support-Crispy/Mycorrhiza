using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;

namespace Mycorrhiza.Content.MycorrhizaBiome.Enemies.Sporewalker
{
    public class Sporewalker : ModNPC
    {
        private int _animationTimer;

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 10;
        }

        public override void SetDefaults()
        {
            NPC.width = 18;
            NPC.height = 40;
            NPC.damage = 25;
            NPC.defense = 8;
            NPC.lifeMax = 120;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = 150f;
            NPC.knockBackResist = 0.4f;
            NPC.aiStyle = NPCAIStyleID.Fighter;
            AIType = NPCID.AngryBones;
        }

        public override void AI()
        {
            NPC.spriteDirection = NPC.direction;
            if (Main.rand.NextBool(400))
            {
                string path = "Mycorrhiza/Assets/Sounds/Enemies/SporeWalkers/zombie";
                int variant = Main.rand.Next(0, 3);
                path += variant;

                SoundStyle groan = new(path);
                SoundEngine.PlaySound(groan with { PitchVariance = 0.3f, MaxInstances = 0 }, NPC.Center);
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            //Main.NewText($"SpriteDirection: {NPC.spriteDirection} Direction: {NPC.direction}");
            Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;

            int frameWidth = 64;
            int frameHeight = texture.Height / 10;

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

        public override void FindFrame(int frameHeight)
        {
            _animationTimer++;
            if (_animationTimer >= 5)
            {
                _animationTimer = 0;
                NPC.frame.Y += frameHeight;
                if (NPC.frame.Y >= frameHeight * 10)
                    NPC.frame.Y = 0;
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.Player.InModBiome<MycorrhizaBiome>() && spawnInfo.SpawnTileY <= Main.worldSurface ? 0.3f : 0f;
        }
    }
}