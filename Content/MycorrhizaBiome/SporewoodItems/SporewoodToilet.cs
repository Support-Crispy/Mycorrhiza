using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Mycorrhiza.Content.MycorrhizaBiome.SporewoodItems
{
    public class SporewoodToilet : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Placeables";
        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<SporewoodToiletPlaced>());
            Item.value = Item.sellPrice(silver: 4);
        }

        public override void AddRecipes()
        {
            CreateRecipe().
                AddIngredient<Sporewood>(6).
                AddTile(TileID.Sawmill).
                Register();
        }
    }
}