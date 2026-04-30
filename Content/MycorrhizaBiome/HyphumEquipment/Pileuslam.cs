using Terraria.ID;
using Terraria.ModLoader;

namespace Mycorrhiza.Content.MycorrhizaBiome.HyphumEquipment
{
	public class Pileuslam : ModItem, ILocalizedModType
	{
		public new string LocalizationCategory => "Items.Tools";
		public override void SetDefaults()
		{
			Item.damage = 20;
			Item.knockBack = 8f;
			Item.useTime = 35;
			Item.useAnimation = 19;
			Item.hammer = 55;

			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTurn = true;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
		}
		public override void AddRecipes()
		{
			CreateRecipe().
				AddIngredient<HyphumBar>(10).
                AddIngredient<Materials.RottingStalk>(5).
                AddTile(TileID.Anvils).
				Register();
		}
	}
}