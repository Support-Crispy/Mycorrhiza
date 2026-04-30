using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace Mycorrhiza.Content
{
    public class ModdedGlobalNPC : GlobalNPC
    {
        public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo)
        {
            bool inMycorrhiza = spawnInfo.Player.InModBiome(ModContent.GetInstance<MycorrhizaBiome.MycorrhizaBiome>());

            if (inMycorrhiza)
            {
                int[] npcsToRemove = new int[]
                {
                    NPCID.Zombie,
                    NPCID.DemonEye,
                    NPCID.BlueSlime,
                };

                foreach (int npcType in npcsToRemove)
                {
                    if (pool.ContainsKey(npcType))
                    {
                        pool.Remove(npcType);
                    }
                }
            }
        }
    }
}