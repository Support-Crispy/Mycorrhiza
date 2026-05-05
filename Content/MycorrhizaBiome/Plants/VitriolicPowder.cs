using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Mycorrhiza.Content.MycorrhizaBiome.Plants

{
	public class VitriolicPowder : ModItem, ILocalizedModType
	{
		public new string LocalizationCategory => "Items";

		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
            Item.ResearchUnlockCount = 99;
            ItemID.Sets.ShimmerTransformToItem[Type] = ItemID.PurificationPowder;
        }

        public override void AddRecipes()
        {
            CreateRecipe(5).
                AddIngredient<VitriolicMushroom>(1).
                AddTile(TileID.Bottles).
                Register();

            Recipe.Create(ItemID.PoisonedKnife, 50).AddIngredient(ItemID.ThrowingKnife, 50).AddIngredient<VitriolicPowder>(1).Register();
        }
    }
}