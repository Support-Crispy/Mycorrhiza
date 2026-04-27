using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Mycorrhiza.Content.MycorrhizaBiome.SporewoodItems
{
	public class SporewoodDresser : ModItem, ILocalizedModType
	{
		public new string LocalizationCategory => "Items.Placeables";
		public override void SetDefaults()
		{
			Item.DefaultToPlaceableTile(ModContent.TileType<SporewoodDresserPlaced>());
			Item.value = Item.sellPrice(copper: 60);
		}

		public override void AddRecipes()
		{
			CreateRecipe().
				AddIngredient<Sporewood>(16).
				AddTile(TileID.Sawmill).
				Register();
		}
	}
}