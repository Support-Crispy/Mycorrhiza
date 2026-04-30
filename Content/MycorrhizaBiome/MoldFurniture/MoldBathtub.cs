using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Mycorrhiza.Content.MycorrhizaBiome.MoldFurniture
{
    public class MoldBathtub : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items";
        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<MoldBathtubPlaced>());
            Item.value = Item.sellPrice(copper: 60);
        }

        public override void AddRecipes()
        {
            CreateRecipe().
                AddIngredient<MoldBlock>(14).
                AddTile(TileID.Sawmill).
                Register();
        }
    }
}