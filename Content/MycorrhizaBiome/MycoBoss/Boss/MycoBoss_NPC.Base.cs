
using BreadLibrary.Core;
using BreadLibrary.Core.Utilities;
using Mycorrhiza.Content.MycorrhizaBiome.MycoBoss.Boss.Attacks;
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

        private MycoBoss_Attack? CurrentAttack;

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
            NPC.Size = new Vector2(200, 200);
            NPC.boss = true;
            NPC.knockBackResist = 0;

            NPC.noGravity = true;




        }

        public override void OnSpawn(IEntitySource source)
        {
            PrepareTendrils();

        }
        #endregion
        public void SetState_State(MycoBoss_State newMycoBoss_State)
        {
            CurrentAttack?.Exit(this);

            CurrentState = newMycoBoss_State;
            Timer = 0;
            CurrentAttack = MycoBossAttackRegistry.Get(newMycoBoss_State);
            CurrentAttack.Enter(this);

            NPC.netUpdate = true;
        }

    }
}
