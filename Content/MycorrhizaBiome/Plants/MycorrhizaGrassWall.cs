using Terraria;
using Terraria.ModLoader;

namespace Mycorrhiza.Content.MycorrhizaBiome.Plants

{
	public class MycorrhizaGrassWall : ModItem, ILocalizedModType
	{
		public new string LocalizationCategory => "Items";

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 400;
        }

        public override void SetDefaults() => Item.DefaultToPlaceableWall(ModContent.WallType<MycorrhizaGrassWallPlaced>());
    }
}