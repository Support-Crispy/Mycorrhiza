
using Mycorrhiza.Content.MycorrhizaBiome.MycoBoss.Boss.Attacks;
using ReLogic.Content;
using Terraria.ID;
using Terraria.ModLoader;

namespace Mycorrhiza.Content.MycorrhizaBiome.MycoBoss.Boss
{
    public partial class MycoBoss_NPC : ModNPC
    {
        public override void BossHeadSlot(ref int index)
        {

        }

        private static void LoadAssets()
        {
            Corpses = new List<Asset<Texture2D>>();
        }
        /// <summary>
        /// To Contain all the textures used on the tendrils.
        /// </summary>
        private static List<Asset<Texture2D>> Corpses;



        public void RenderTendrils(SpriteBatch spriteBatch, Vector2 screenPos, ref Color drawColor)
        {
            if(Tendrils is null)
            {
                return;
            }
            foreach (var tendril in Tendrils)
            {
                tendril.Draw(spriteBatch, screenPos, drawColor, NPC.Center);
            }

        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            RenderTendrils(spriteBatch, screenPos, ref drawColor);
            return base.PreDraw(spriteBatch, screenPos, drawColor);
        }
    }
}
