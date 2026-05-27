using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;

namespace Mycorrhiza.Content.MycorrhizaBiome.MycoBoss.Boss
{
    public class MycoBoss_NPC : ModNPC
    {
        public override void SetStaticDefaults()
        {
            NPCID.Sets.MustAlwaysDraw[Type] = true;
        }
        public override void SetDefaults()
        {
            NPC.lifeMax = 10_000;
            NPC.Size = new Microsoft.Xna.Framework.Vector2(200, 200);
        }
    }
}
