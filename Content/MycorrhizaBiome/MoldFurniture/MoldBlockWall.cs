using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace Mycorrhiza.Content.MycorrhizaBiome.MoldFurniture
{
    public class MoldBlockWall : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items";
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 100;
        }

        public override void SetDefaults() => Item.DefaultToPlaceableWall(ModContent.WallType<MoldBlockWallPlaced>());

        public override void AddRecipes()
        {
            CreateRecipe(4).
                AddIngredient<MoldBlock>().
                DisableDecraft().
                Register();
          }
    }
}