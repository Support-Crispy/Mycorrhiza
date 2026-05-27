
using BreadLibrary.Core.Utilities;
using Mycorrhiza.Content.MycorrhizaBiome.MycoBoss.Boss.Attacks;
using Terraria.ID;
using Terraria.ModLoader;

namespace Mycorrhiza.Content.MycorrhizaBiome.MycoBoss.Boss
{
    public partial class MycoBoss_NPC : ModNPC
    {
        #region Fields and Properties
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
        public override void SetStaticDefaults()
        {
            NPCID.Sets.MustAlwaysDraw[Type] = true;
            Path = this.GetPath();
        }
        public override void SetDefaults()
        {
            NPC.lifeMax = 10_000;
            NPC.Size = new Vector2(200, 200);
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
