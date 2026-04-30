using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;

namespace Mycorrhiza.Content.MycorrhizaBiome.Enemies.Sporewalker
{
    public class Sporewalker : ModNPC
    {

        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.Skeleton);
            AIType = NPCID.Skeleton;
            AnimationType = NPCID.Zombie;
        }

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.Zombie];
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            bool inMycorrhiza = spawnInfo.Player.InModBiome(ModContent.GetInstance<MycorrhizaBiome>());

            if (inMycorrhiza)
            {
                return 1f;
            }

            return 0f;
        }

    }
}