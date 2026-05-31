
using BreadLibrary.Core.Graphics.Pixelation;
using BreadLibrary.Core.Utilities;
using Mycorrhiza.Content.MycorrhizaBiome.MycoBoss.Boss.Attacks;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Mycorrhiza.Content.MycorrhizaBiome.MycoBoss.Boss
{
    public partial class MycoBoss_NPC : ModNPC, IDrawPixelated
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

        PixelLayer IDrawPixelated.PixelLayer => PixelLayer.BehindTiles;

        public void DrawCap(SpriteBatch spriteBatch, Vector2 screenPos, ref Color drawColor)
        {
            var tex = TextureAssets.Npc[Type].Value;

            var drawPos = NPC.Center - screenPos;


            Main.EntitySpriteDraw(tex, drawPos, null, drawColor, NPC.rotation, tex.Size() / 2, 1, 0);

        }
        public void DrawBody(SpriteBatch spriteBatch)
        {
           
            if(Body is not null)
            {
                for(int i = 0; i< Body.Positions.Length-1; i++)
                {
                    float interp = i / (float)(Body.Positions.Length - 1);
                    Utilities.DrawLineBetter(spriteBatch, Body.Positions[i], Body.Positions[i + 1], new Color(82, 75, 36), 93 * interp);
                }
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            #if DEBUG
            RenderHitboxes(spriteBatch, screenPos);


            if (AttackQueue is not null)
            {
                string msg = $"CurrentAtttack: {CurrentAttack?.ToString()}\nAttacks: ";
                foreach(var a in AttackQueue)
                {
                    msg += $"{a.ToString()}\n";
                }


                Utils.DrawBorderString(spriteBatch, msg, NPC.Center - screenPos, Color.White, anchory: -10);
            }

#endif
            RenderTendrils(spriteBatch, screenPos, ref drawColor);
            DrawCap(spriteBatch, screenPos, ref drawColor);

           
            return false;
        }

        void IDrawPixelated.DrawPixelated(SpriteBatch spriteBatch)
        {

            DrawBody(spriteBatch);
        }
    }
}
