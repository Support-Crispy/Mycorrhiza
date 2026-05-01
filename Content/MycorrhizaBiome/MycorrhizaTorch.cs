using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Mycorrhiza.Content.MycorrhizaBiome
{
	public class MycorrhizaTorch : ModItem, ILocalizedModType
	{
		public new string LocalizationCategory => "Items";

		public override void SetDefaults()
		{
			Item.DefaultToPlaceableTile(ModContent.TileType<MycorrhizaTorchPlaced>());
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
            CreateRecipe().
                AddIngredient(ItemID.Torch,3).
                AddIngredient<BiomeTiles.PileustoneBlock>(1).
                Register();
        }
    }
}