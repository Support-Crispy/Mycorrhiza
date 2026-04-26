using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Mycorrhiza.Content.MycorrhizaBiome.SporewoodItems
{
    public class SporewoodChest : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Placeables";
        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<SporewoodChestPlaced>());
            Item.value = Item.sellPrice(silver: 4);
        }

        public override void AddRecipes()
        {
            CreateRecipe().
                AddIngredient<Sporewood>(8).
                AddRecipeGroup("IronBars",2).
                AddTile(TileID.WorkBenches).
                Register();
        }
    }
}