using AltLibrary;
using AltLibrary.Common.AltBiomes;
using Terraria.ModLoader;
using Terraria.ID;
using Mycorrhiza.Content.MycorrhizaBiome.Plants;
using Mycorrhiza.Content.MycorrhizaBiome.BiomeTiles;
using Mycorrhiza.Content.MycorrhizaBiome.BiomeTiles.Desert;

namespace Mycorrhiza.Content.MycorrhizaBiome
{
    public class MycorrhizaBiomeType : AltBiome
    {
        public override string IconSmall => "Mycorrhiza/Content/MycorrhizaBiome/Icons/MycorrhizaIcon_Small";
        public override string WorldIcon => "Mycorrhiza/Content/MycorrhizaBiome/Icons/MycorrhizaWorldIcon";

        public override void SetStaticDefaults()
        {
            BiomeType = AltLibrary.BiomeType.Evil;

            BiomeGrass = ModContent.TileType<MoldyGrassPlaced>();

            AddTileConversion(ModContent.TileType<PileustoneBlockPlaced>(), TileID.Stone);
            AddTileConversion(ModContent.TileType<PileusandBlockPlaced>(), TileID.Sand);
            AddTileConversion(ModContent.TileType<HardenedPileusandBlockPlaced>(), TileID.HardenedSand);
            AddTileConversion(ModContent.TileType<PileusandstoneBlockPlaced>(), TileID.Sandstone);

            AddWallConversions(ModContent.WallType<PileustoneBlockWallPlaced>(),
                WallID.EbonstoneUnsafe, WallID.CrimstoneUnsafe,
                WallID.CorruptionUnsafe1, WallID.CorruptionUnsafe2,
                WallID.CorruptionUnsafe3, WallID.CorruptionUnsafe4,
                WallID.CrimsonUnsafe1, WallID.CrimsonUnsafe2,
                WallID.CrimsonUnsafe3, WallID.CrimsonUnsafe4);

            EvilBiomeGenerationPass = new MycorrhizaEvilBiomeGenerationPass();
        }
    }
}