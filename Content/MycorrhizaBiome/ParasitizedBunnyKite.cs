using Terraria;
using Terraria.ModLoader;

namespace Mycorrhiza.Content.MycorrhizaBiome
{
	public class ParasitizedBunnyKite : ModItem, ILocalizedModType
	{
		public new string LocalizationCategory => "Items";

		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
		}
	}
}