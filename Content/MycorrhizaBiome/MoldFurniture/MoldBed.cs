using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Mycorrhiza.Content.MycorrhizaBiome.MoldFurniture
{
    public class MoldBed : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items";
        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<MoldBedPlaced>());
            Item.value = Item.sellPrice(silver: 4);
        }

        public override void AddRecipes()
        {
            CreateRecipe().
                AddIngredient<MoldBlock>(15).
                AddIngredient(ItemID.Silk, 5).
                AddTile(TileID.Sawmill).
                Register();
        }
    }
}