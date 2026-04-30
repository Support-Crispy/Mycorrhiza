using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;

namespace Mycorrhiza.Content.MycorrhizaBiome.Enemies.Moldslime
{
    public class Moldslime : ModNPC
    {

        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.DungeonSlime);
            AIType = NPCID.DungeonSlime;
            AnimationType = NPCID.DungeonSlime;
        }

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.DungeonSlime];
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