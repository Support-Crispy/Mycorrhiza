
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mycorrhiza.Content.MycorrhizaBiome.MycoBoss.Boss.Attacks
{

    internal abstract class MycoBoss_Attack
    {
        public abstract MycoBoss_State ID { get; }
        public virtual void Enter(MycoBoss_NPC boss) { }
        public abstract void Update(MycoBoss_NPC boss);
        public virtual void Exit(MycoBoss_NPC boss) { }

        public virtual void Draw(MycoBoss_NPC boss, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor) { }

    }
}