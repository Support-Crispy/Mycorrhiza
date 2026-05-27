using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace Mycorrhiza.Content.MycorrhizaBiome.Enemies.ParasitizedGoldfish
{
    public class ParasitizedGoldfish : ModNPC
    {

        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.CorruptGoldfish);
            AIType = NPCID.CorruptGoldfish;
            AnimationType = NPCID.CorruptGoldfish;
        }

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.CorruptGoldfish];
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            bool inMycorrhiza = spawnInfo.Player.InModBiome(ModContent.GetInstance<MycorrhizaBiome>());

            if (inMycorrhiza && spawnInfo.Water)
            {
                return 0.15f; 
            }

            if (inMycorrhiza && Main.bloodMoon && spawnInfo.Water)
            {
                return 0.3f; 
            }

            return 0f;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;
            Rectangle Frame = texture.Frame(1, 6, 0, NPC.frame.Y/(NPC.frame.Height+1));

            Vector2 drawPos = NPC.Center - screenPos;
            SpriteEffects flip = NPC.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            Main.EntitySpriteDraw(texture, drawPos, Frame, drawColor, NPC.rotation, Frame.Size() / 2, NPC.scale, flip);

            return false;
        }

    }
}