using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Mycorrhiza.Content.MycorrhizaBiome.SporewoodItems
{
    public class SporewoodBathtub : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items";
        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<SporewoodBathtubPlaced>());
            Item.value = Item.sellPrice(copper: 60);
        }

        public override void AddRecipes()
        {
            CreateRecipe().
                AddIngredient<Sporewood>(14).
                AddTile(TileID.Sawmill).
                Register();
        }
    }
}