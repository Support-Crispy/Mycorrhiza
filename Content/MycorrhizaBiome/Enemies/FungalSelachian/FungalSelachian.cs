using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Mycorrhiza.Content.MycorrhizaBiome;
using Mycorrhiza.Content.MycorrhizaBiome.BiomeTiles.Desert;
using System;

namespace Mycorrhiza.Content.MycorrhizaBiome.Enemies.FungalSelachian
{
    public class FungalSelachian : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.SandsharkCorrupt];
            NPCID.Sets.NPCBestiaryDrawOffset[Type] = NPCID.Sets.NPCBestiaryDrawOffset[NPCID.SandsharkCorrupt];
        }

        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.SandsharkCorrupt);
            AIType = NPCID.SandsharkCorrupt;
            AnimationType = NPCID.SandsharkCorrupt;
        }

        private bool IsSwimmableTile(int tileType)
        {
            return tileType == TileID.Sand ||
                   tileType == TileID.Ebonsand ||
                   tileType == TileID.Crimsand ||
                   tileType == TileID.Pearlsand ||
                   tileType == TileID.HardenedSand ||
                   tileType == 398 || // Hardened Ebonsand
                   tileType == 399 || // Hardened Crimsand
                   tileType == 402 || // Hardened Pearlsand
                   tileType == TileID.Sandstone ||
                   tileType == 400 || // Ebonsandstone
                   tileType == 401 || // Crimsandstone
                   tileType == ModContent.TileType<PileusandBlockPlaced>() ||
                   tileType == ModContent.TileType<HardenedPileusandBlockPlaced>() ||
                   tileType == ModContent.TileType<PileusandstoneBlockPlaced>();
        }

        public override void AI()
        {
            int tileX = (int)(NPC.Center.X / 16);
            int tileY = (int)(NPC.Center.Y / 16);
            int range = 4;

            // Store original tile types before swapping
            Dictionary<(int, int), ushort> originalTiles = new Dictionary<(int, int), ushort>();

            for (int x = tileX - range; x <= tileX + range; x++)
            {
                for (int y = tileY - range; y <= tileY + range; y++)
                {
                    if (x < 0 || x >= Main.maxTilesX || y < 0 || y >= Main.maxTilesY)
                        continue;

                    Tile tile = Main.tile[x, y];
                    if (tile.HasTile && IsSwimmableTile(tile.TileType))
                    {
                        originalTiles[(x, y)] = tile.TileType;
                        tile.TileType = TileID.Ebonsand;
                    }
                }
            }

            // Run vanilla Sand Shark AI
            NPC.VanillaAI();

            // Restore original tile types
            foreach (var entry in originalTiles)
            {
                int x = entry.Key.Item1;
                int y = entry.Key.Item2;
                Main.tile[x, y].TileType = entry.Value;
            }
        }

        //public override float SpawnChance(NPCSpawnInfo spawnInfo)
        //{
        //    return spawnInfo.Player.InModBiome<MycorrhizaBiome>() &&
        //           spawnInfo.SpawnTileY <= Main.worldSurface ? 0.2f : 0f;
        //}
    }
}