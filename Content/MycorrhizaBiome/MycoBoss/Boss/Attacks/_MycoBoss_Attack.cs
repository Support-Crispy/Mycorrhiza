
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mycorrhiza.Content.MycorrhizaBiome.MycoBoss.Boss.Attacks
{

    public abstract class _MycoBoss_Attack
    {
        public sealed override string ToString()
        {
            return this.ID.ToString();
        }
        public abstract MycoBoss_State ID { get; }
        public virtual void Enter(MycoBoss_NPC boss) { }
        public abstract void Update(MycoBoss_NPC boss);
        public virtual void Exit(MycoBoss_NPC boss) { }

        public virtual void Draw(MycoBoss_NPC boss, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor) { }
        protected void Finish(MycoBoss_NPC boss) => boss.MoveToNextState();

    }
}