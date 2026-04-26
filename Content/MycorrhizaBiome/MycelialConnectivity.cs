using Terraria;
using Terraria.ModLoader;

namespace Mycorrhiza.Content.MycorrhizaBiome
{
	public class MycelialConnectivity : ModItem, ILocalizedModType
	{
		public new string LocalizationCategory => "Items.Placeables";

		public override void SetDefaults()
		{
			Item.DefaultToPlaceableTile(ModContent.TileType<MycelialConnectivityPlaced>());
			Item.width = 32;
			Item.height = 32;
			Item.value = Item.buyPrice(gold: 1);
		}
	}
}