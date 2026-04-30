using System;
using Terraria.ModLoader;

namespace Mycorrhiza.Content.MycorrhizaBiome
{
    public class MycorrhizaBiomeTileCount : ModSystem
    {
        public int mycorrhizaBlockCount;

        public override void TileCountsAvailable(ReadOnlySpan<int> tileCounts)
        {
            mycorrhizaBlockCount = tileCounts[ModContent.TileType<BiomeTiles.PileustoneBlockPlaced>()] + tileCounts[ModContent.TileType<Plants.MoldyGrassPlaced>()];
        }
    }
}