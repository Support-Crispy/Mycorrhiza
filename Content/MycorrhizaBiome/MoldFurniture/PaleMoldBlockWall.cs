using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace Mycorrhiza.Content.MycorrhizaBiome.MoldFurniture
{
    public class PaleMoldBlockWall : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items";
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 100;
        }

        public override void SetDefaults() => Item.DefaultToPlaceableWall(ModContent.WallType<PaleMoldBlockWallPlaced>());

        public override void AddRecipes()
        {
            CreateRecipe(4).
                AddIngredient<MoldBlock>().
                AddTile(TileID.HeavyWorkBench).
                DisableDecraft().
                Register();
          }
    }
}