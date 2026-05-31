
using BreadLibrary.Core;
using BreadLibrary.Core.Utilities;
using BreadLibrary.Core.Verlet;
using Mycorrhiza.Content.MycorrhizaBiome.MycoBoss.Boss.Attacks;
using Mycorrhiza.Content.MycorrhizaBiome.MycoBoss.Boss.Attacks.Mycorrhiza.Content.MycorrhizaBiome.MycoBoss.Boss.Attacks;
using Mycorrhiza.Core;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Mycorrhiza.Content.MycorrhizaBiome.MycoBoss.Boss
{
    [AutoloadBossHead]
    public partial class MycoBoss_NPC : ModNPC
    {
        #region Fields and Properties

        internal static int _BossHeadTexture_Open;

        private _MycoBoss_Attack? CurrentAttack;

        public static string Path;
        public int Timer
        {
            get => (int)NPC.ai[0];
            set => NPC.ai[0] = value;
        }
        public MycoBoss_State CurrentState
        {
            get => (MycoBoss_State)NPC.ai[1];
            set => NPC.ai[1] = (int)value;
        }


        public bool ShouldHoldHeadStill = false;

        public const float BaseDesiredHeight = 270f;
        public float DesiredHeight = 270f;
        public override void Load()
        {
            Path = this.GetPath();

            _BossHeadTexture_Open = Mod.AddBossHeadTexture($"{Path}_Head_Boss_Open", Type);

        }
        public override void SetStaticDefaults()
        {
            LoadAssets();
            NPCID.Sets.MustAlwaysDraw[Type] = true;
        }
        public override void SetDefaults()
        {
            NPC.lifeMax = 10_000;
            NPC.Size = new Vector2(100, 200);
            NPC.boss = true;
            NPC.knockBackResist = 0;

            NPC.noGravity = true;




        }
        #region headPlatform
        private RotatedPlatform _lastHeadPlatform;
        private bool _initializedHeadPlatform;

        public Vector2 HeadPlatformCenter
        {
            get
            {
                // Replace this with the actual visual head center if needed.
                return NPC.Center + new Vector2(0f, -NPC.height * 0.45f);
            }
        }

        public RotatedPlatform HeadPlatform
        {
            get
            {
                return new RotatedPlatform(
                    center: HeadPlatformCenter,
                    width: 230f,
                    thickness: 12f,
                    rotation: HeadRotation
                );
            }
        }
        public float HeadRotation => NPC.rotation;
        /// <summary>
        /// Moves a player as if they are standing on the boss's head.
        /// </summary>
        public void UpdateStandingOnHead(Player player)
        {
            RotatedPlatform platform = HeadPlatform;

            if (!_initializedHeadPlatform)
            {
                _lastHeadPlatform = platform;
                _initializedHeadPlatform = true;
            }

            Vector2 currentFeet = player.Bottom;

            Vector2 currentSurfacePoint = platform.ProjectFeetToTop(currentFeet);
            Vector2 previousSurfacePoint = _lastHeadPlatform.ProjectFeetToTop(currentFeet);

            Vector2 platformDelta = currentSurfacePoint - previousSurfacePoint;

            Vector2 newPosition = player.position;

            // carry player with platform motion/rotation
            newPosition += platformDelta;

            // snap feet to the rotated top surface 
            Vector2 snappedFeet = platform.ProjectFeetToTop(newPosition + new Vector2(player.width * 0.5f, player.height));
            newPosition.Y += snappedFeet.Y - (newPosition.Y + player.height);

            player.velocity.Y = 0f;

            if (!Collision.SolidCollision(newPosition, player.width, player.height))
                player.position = newPosition;

            player.gfxOffY = 0f;
        }
        #endregion


        public VerletChain Body;
        public override void OnSpawn(IEntitySource source)
        {
            PrepareTendrils();
            Body = new VerletChain(20, 5, NPC.Center);

        }
        #endregion

        public Queue<MycoBoss_State> AttackQueue = new();
        public void SetState_State(MycoBoss_State newMycoBoss_State)
        {
            CurrentAttack?.Exit(this);
            CurrentState = newMycoBoss_State;
            Timer = 0;
            CurrentAttack = _MycoBossAttackRegistry.Create(newMycoBoss_State);
            CurrentAttack.Enter(this);

            NPC.netUpdate = true;
        }



        
        internal void MoveToNextState()
        {
            if (AttackQueue.Count > 0)
            {
                SetState_State(AttackQueue.Dequeue());
            }
            if(CurrentState == MycoBoss_State.Replenish_Sporewalkers)
            {
                AttackQueue.Enqueue(MycoBoss_State.Debug);
            }
        }
    }
}
