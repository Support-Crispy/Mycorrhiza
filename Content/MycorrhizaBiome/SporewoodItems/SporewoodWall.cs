using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace Mycorrhiza.Content.MycorrhizaBiome.SporewoodItems
{
    public class SporewoodWall : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Placeables";
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 100;
        }

        public override void SetDefaults() => Item.DefaultToPlaceableWall(ModContent.WallType<SporewoodWallPlaced>());

        public override void AddRecipes()
        {
            CreateRecipe(4).
                AddIngredient<Sporewood>().
                DisableDecraft().
                Register();
          }
    }
}