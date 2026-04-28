using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Mycorrhiza.Content.MycorrhizaBiome.MoldFurniture
{
    public class MoldSofa : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items";
        public override void SetDefaults()
        {
            //Item.DefaultToPlaceableTile(ModContent.TileType<MoldSofaPlaced>());
            Item.value = Item.sellPrice(copper: 60);
        }

        public override void AddRecipes()
        {
            CreateRecipe().
                AddIngredient<MoldBlock>(5).
                AddIngredient(ItemID.Silk, 2).
                AddTile(TileID.Sawmill).
                Register();
        }
    }
}