using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Mycorrhiza.Content.MycorrhizaBiome.SporewoodItems
{
    public class SporewoodLamp : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items";
        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<SporewoodLampPlaced>());
            Item.value = Item.sellPrice(copper: 60);
        }

        public override void AddRecipes()
        {
            CreateRecipe().
                AddIngredient(ItemID.Torch, 1).
                AddIngredient<Sporewood>(3).
                AddTile(TileID.WorkBenches).
                Register();
        }
    }
}