using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;

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

    }
}