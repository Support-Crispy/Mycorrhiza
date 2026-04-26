using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace Mycorrhiza.Content.MycorrhizaBiome.MoldFurniture
{
    public class MoldBlock : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Placeables";
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 100;
        }

        public override void SetDefaults() => Item.DefaultToPlaceableTile(ModContent.TileType<MoldBlockPlaced>());

        public override void AddRecipes()
        {
            CreateRecipe().
                AddIngredient<SporewoodItems.Sporewood>(10).
                AddTile(TileID.MeatGrinder).
                Register();
        }
    }
}