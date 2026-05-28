
using Mycorrhiza.Content.MycorrhizaBiome.MycoBoss.Boss.Attacks;
using ReLogic.Content;
using Terraria.ID;
using Terraria.ModLoader;

namespace Mycorrhiza.Content.MycorrhizaBiome.MycoBoss.Boss
{
    public partial class MycoBoss_NPC : ModNPC
    {
        private static void LoadAssets()
        {
            //Corpses = ModContent.Request<Texture2D>("Mycorrhiza/Content/MycorrhizaBiome/MycoBoss/Boss/MycoBoss_Corpse");
        }

        private static Asset<Texture2D> Corpses;


        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            return base.PreDraw(spriteBatch, screenPos, drawColor);
        }
    }
}
