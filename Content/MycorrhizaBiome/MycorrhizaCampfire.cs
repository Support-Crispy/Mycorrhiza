using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Mycorrhiza.Content.MycorrhizaBiome
{
	public class MycorrhizaCampfire : ModItem, ILocalizedModType
	{
		public new string LocalizationCategory => "Items";

		public override void SetDefaults()
		{
			Item.DefaultToPlaceableTile(ModContent.TileType<MycorrhizaCampfirePlaced>());
			Item.width = 32;
			Item.height = 32;
            Item.ResearchUnlockCount = 1;
            ItemID.Sets.ShimmerTransformToItem[Type] = ItemID.ShimmerCampfire;
        }

        public override void AddRecipes()
        {
            CreateRecipe().
                AddRecipeGroup(RecipeGroupID.Wood, 10).
                AddIngredient<MycorrhizaTorch>(5).
                Register();
        }
    }
}