using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Mycorrhiza.Content.MycorrhizaBiome.MoldFurniture
{
    public class MoldSink : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items";
        public override void SetDefaults()
        {
            //Item.DefaultToPlaceableTile(ModContent.TileType<MoldSinkPlaced>());
            Item.value = Item.sellPrice(copper: 60);
        }

        public override void AddRecipes()
        {
            CreateRecipe().
                AddIngredient<MoldBlock>(6).
                AddIngredient(ItemID.WaterBucket, 1).
                AddTile(TileID.WorkBenches).
                Register();
        }
    }
}