using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Mycorrhiza.Content.MycorrhizaBiome.HyphalSparks
{
	public class HyphalTorch : ModItem, ILocalizedModType
	{
		public new string LocalizationCategory => "Items";

		public override void SetDefaults()
		{
			Item.DefaultToPlaceableTile(ModContent.TileType<HyphalTorchPlaced>());
			Item.width = 32;
			Item.height = 32;
            Item.ResearchUnlockCount = 100;
            ItemID.Sets.ShimmerTransformToItem[Type] = ItemID.ShimmerTorch;
        }

        public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
        {
            itemGroup = ContentSamples.CreativeHelper.ItemGroup.Torches;
        }

        public override void AddRecipes()
        {
            CreateRecipe(33).
                AddIngredient(ItemID.Torch, 33).
                AddIngredient<HyphalSparks>(1).
                Register();
        }
    }
}