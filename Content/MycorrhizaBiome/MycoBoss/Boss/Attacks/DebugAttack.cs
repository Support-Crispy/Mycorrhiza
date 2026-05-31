using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mycorrhiza.Content.MycorrhizaBiome.MycoBoss.Boss.Attacks
{
    internal class DebugAttack : _MycoBoss_Attack
    {
        public override MycoBoss_State ID => MycoBoss_State.Debug;

        public override void Update(MycoBoss_NPC boss)
        {
            boss.ShouldHoldHeadStill = false;

            boss.DesiredHeight = MycoBoss_NPC.BaseDesiredHeight;
            if (boss.Timer > 10)
                boss.MoveToNextState();
        }
    }
}
