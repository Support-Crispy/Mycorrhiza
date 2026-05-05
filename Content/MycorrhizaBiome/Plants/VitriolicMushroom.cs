using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Mycorrhiza.Content.MycorrhizaBiome.Plants

{
	public class VitriolicMushroom : ModItem, ILocalizedModType
	{
		public new string LocalizationCategory => "Items";

		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
            Item.ResearchUnlockCount = 25;
            ItemID.Sets.ShimmerTransformToItem[Type] = ItemID.Mushroom;
        }
	}
}