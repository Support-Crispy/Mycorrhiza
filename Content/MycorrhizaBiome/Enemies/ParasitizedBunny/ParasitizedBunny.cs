using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;

namespace Mycorrhiza.Content.MycorrhizaBiome.Enemies.ParasitizedBunny
{
    public class ParasitizedBunny : ModNPC
    {

        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.CorruptBunny);
            AIType = NPCID.CorruptBunny;
            AnimationType = NPCID.CorruptBunny;
        }

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.CorruptBunny];
        }

        /* public override float SpawnChance(NPCSpawnInfo spawnInfo)
         {
             bool inMycorrhiza = spawnInfo.Player.InModBiome(ModContent.GetInstance<MycorrhizaBiome>());

             if (inMycorrhiza)
             {
                 return 1f;
             }

             return 0f;
         } */

    }
}