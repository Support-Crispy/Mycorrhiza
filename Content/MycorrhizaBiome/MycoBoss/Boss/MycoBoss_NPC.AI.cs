using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }

        public override void PostAI()
        {
            base.PostAI();
        }

    }
}
