using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;

namespace Mycorrhiza.Content.MycorrhizaBiome.Enemies.ParasitizedPenguin
{
    public class ParasitizedPenguin : ModNPC
    {

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

    }
}