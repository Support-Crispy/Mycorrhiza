using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Mycorrhiza.Content.MycorrhizaBiome.MoldFurniture
{
	public class MoldDresser : ModItem, ILocalizedModType
	{
		public new string LocalizationCategory => "Items.Placeables";
		public override void SetDefaults()
		{
			Item.DefaultToPlaceableTile(ModContent.TileType<MoldDresserPlaced>());
			Item.value = Item.sellPrice(copper: 60);
		}

		public override void AddRecipes()
		{
			CreateRecipe().
				AddIngredient<MoldBlock>(16).
				AddTile(TileID.Sawmill).
				Register();
		}
	}
}