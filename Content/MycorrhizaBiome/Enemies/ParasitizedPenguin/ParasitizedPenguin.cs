using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using BreadLibrary.Core.Utilities;
using Terraria.DataStructures;

namespace Mycorrhiza.Content.MycorrhizaBiome.Enemies.ParasitizedPenguin
{
    public class ParasitizedPenguin : ModNPC
    {
        private const int MaxVariants = 4; // Total number of variants (0 to 3)
        public int Variant
        {
            get => (int)NPC.localAI[0];
            set => NPC.localAI[0] = value;
        }
        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.CorruptPenguin);
            AIType = NPCID.CorruptPenguin;
            AnimationType = NPCID.CorruptPenguin;
        }

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.CorruptPenguin];
        }
        public override void OnSpawn(IEntitySource source)
        {
            Variant = Main.rand.Next(MaxVariants);
        }


        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D texture = TextureAssets.Npc[Type].Value;
            Vector2 DrawPos = NPC.Center - screenPos+ new Vector2(0, -2);


            Rectangle Frame = texture.Frame(4, 3, NPC.frame.X + Variant, NPC.frame.Y/NPC.frame.Height);

            Vector2 Origin = Frame.Size() / 2;


            SpriteEffects flip = (-NPC.spriteDirection).ToSpriteDirection();
            Main.EntitySpriteDraw(texture, DrawPos, Frame, drawColor, NPC.rotation, Origin, NPC.scale, flip);
            
            return false;
        }
    }
}