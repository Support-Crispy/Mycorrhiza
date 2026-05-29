using BreadLibrary.Core.Graphics;
using BreadLibrary.Core.Graphics.Particles;
using BreadLibrary.Core.Utilities;
using Mycorrhiza.Content.MycorrhizaBiome.Enemies.Sporewalker;
using Mycorrhiza.Content.MycorrhizaBiome.MycoBoss.Boss;
using Mycorrhiza.Content.Particles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;

namespace Mycorrhiza.Content.MycorrhizaBiome.MycoBoss
{
    internal class DanglingSporeWalker: ModNPC
    {
        private int _animationTimer;

        public bool OnGround;
        public bool Falling;
        public int Timer
        {
            get => (int)NPC.ai[2];
            set => NPC.ai[2] = value;
        }
        public int _OwnerID
        {
            get => (int)NPC.ai[0];
            set => NPC.ai[0] = value;
        }

        public int TentacleID
        {
            get => (int)NPC.ai[1];
            set => NPC.ai[1] = value;
        }
        public MycoBoss_NPC Owner
        {
            get 
            {
                if (Main.npc.IndexInRange(_OwnerID) && Main.npc[_OwnerID].active && Main.npc[_OwnerID].type == ModContent.NPCType<MycoBoss_NPC>())
                {
                    return (MycoBoss_NPC)Main.npc[_OwnerID].ModNPC;
                }
                return null;
            }
        } 


        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 5;
            this.ExcludeFromBestiary();
        }

        public override void SetDefaults()
        {
            NPC.CloneDefaults(ModContent.NPCType<Sporewalker>());

            NPC.value = 0;



            _OwnerID = -1;
            TentacleID = -1;
            NPC.knockBackResist = 0.0f;
            NPC.aiStyle = -1;
            AIType = -1;
            NPC.noGravity = true;
        }

        public override void OnSpawn(IEntitySource source)
        {
            if(source is not null)
            {
                if(source is EntitySource_Parent parentSource && parentSource.Entity is NPC parentNPC && parentNPC.ModNPC is MycoBoss_NPC boss)
                {
                    _OwnerID = parentNPC.whoAmI;
                }
            }

            if (Owner is not null)
            {
                foreach(MycoTendril tendril in Owner.Tendrils)
                {
                    if (tendril.HasNPC)
                    {
                        continue;
                    }

                    else
                        {
                        TentacleID = tendril.Index;
                        tendril.HasNPC = true;
                        break;
                    }
                }
            }

            NPC.spriteDirection = Main.rand.NextBool() ? 1 : -1;
            NPC.frame.X = Main.rand.Next(0, 3);
        }

        public override void AI()
        {

            if (!OnGround & !Falling)
            {
                if (Owner is null || TentacleID == -1)
                {
                    NPC.active = false;
                    return;
                }

                if (Owner.Tendrils is null || Owner.Tendrils.Count <= TentacleID)
                {
                    NPC.active = false;
                    return;
                }

                if (Owner.Tendrils[TentacleID].Severed && !OnGround)
                {
                    Falling = true;
                    
                }
                else
                {
                    int Max = Owner.Tendrils[TentacleID].Chain.Positions.Length - 1;
                    float rot = Owner.Tendrils[TentacleID].Chain.Positions[Max - 1].AngleFrom(Owner.Tendrils[TentacleID].Chain.Positions[Max]) + MathHelper.PiOver2;
                    NPC.rotation = rot;
                    NPC.Center = Owner.Tendrils[TentacleID].Chain.Positions[^1];

                }
            }



            if (Falling & !OnGround)
            {
                NPC.noGravity = false;
                TentacleID = -1;

                if (!OnGround)
                    NPC.rotation += 0.1f * NPC.spriteDirection;
                Point? hit = BreadLibrary.Core.Utilities.Utilities.RaycastTo(NPC.Center, NPC.Bottom + new Vector2(0, 4), false, true, false);

                if (hit.HasValue && !OnGround)
                {
                    OnGround = true;
                    for (int i = 0; i < 40; i++)
                    {
                        MushBoom Particle = new();
                        Particle.Prepare(NPC.Bottom + new Vector2(0, -3), Main.rand.NextVector2Circular(3, 2) * Main.rand.NextFloat(0.1f, 2), Main.rand.NextFloat(-MathHelper.Pi, MathHelper.Pi), 50);
                        ParticleEngine.ShaderParticles.Add(Particle);
                    }
                    Dust.NewDustPerfect(NPC.Bottom, ModContent.DustType<MycorrhizaWaterSplashDust>(), Main.rand.NextVector2Circular(3, 2) * Main.rand.NextFloat(0.1f, 2));

                }
            }

          


            if (OnGround)
            {
                Timer++;
                NPC.rotation = NPC.rotation.AngleLerp(0, 0.06f);
                NPC.position.X += Main.rand.NextFloat(-1, 1) * (Timer / 40f)*3;
                if(Timer > 60)
                {
                    NPC.CloneDefaults(ModContent.NPCType<Sporewalker>());
                    NPC.Transform(ModContent.NPCType<Sporewalker>());
                }
            }
        }


        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            //Main.NewText($"SpriteDirection: {NPC.spriteDirection} Direction: {NPC.direction}");
            Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;

            int frameWidth = 64;
            int frameHeight = texture.Height / 10;

            Rectangle sourceRect = texture.Frame(3, 5, NPC.frame.X, NPC.frame.Y);

            Vector2 drawOrigin = sourceRect.Size() / 2f;

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
            return;
            _animationTimer++;
            if (_animationTimer >= 5)
            {
                _animationTimer = 0;
                NPC.frame.Y += frameHeight;
                if (NPC.frame.Y >= frameHeight * 10)
                    NPC.frame.Y = 0;
            }
        }

    }
}
