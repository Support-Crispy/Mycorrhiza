using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

//Placeholder, finish me!

namespace Mycorrhiza.Content.MycorrhizaBiome
{
	public class RottingAltarReplica : ModItem, ILocalizedModType
	{
		public new string LocalizationCategory => "Items";

		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
		}

		public override void AddRecipes()
		{
			CreateRecipe().
				AddIngredient<BiomeTiles.PileustoneBlock>(15).
				AddIngredient<HyphumEquipment.HyphumBar>(3).
				AddIngredient<Materials.RottingStalk>(3).
				Register();
		}
	}
}