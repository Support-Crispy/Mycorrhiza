using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Mycorrhiza.Content.MycorrhizaBiome.Materials

{
	public class RottingStalk : ModItem, ILocalizedModType
	{
		public new string LocalizationCategory => "Items";

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 25;
        }

        public override void SetDefaults()
		{
            Item.value = Item.sellPrice(silver: 1);
            Item.maxStack = Item.CommonMaxStack;
            Item.rare = ItemRarityID.Blue;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Swing;
        }

        public override void AddRecipes()
        {
            Recipe.Create(ItemID.ObsidianShirt, 1).AddIngredient(ItemID.Silk, 10).AddIngredient(ItemID.Obsidian, 20).AddIngredient<RottingStalk>(10).AddTile(TileID.Hellforge).Register();
            Recipe.Create(ItemID.ObsidianHelm, 1).AddIngredient(ItemID.Silk, 10).AddIngredient(ItemID.Obsidian, 20).AddIngredient<RottingStalk>(5).AddTile(TileID.Hellforge).Register();
            Recipe.Create(ItemID.ObsidianPants, 1).AddIngredient(ItemID.Silk, 10).AddIngredient(ItemID.Obsidian, 20).AddIngredient<RottingStalk>(5).AddTile(TileID.Hellforge).Register();
            Recipe.Create(ItemID.VoidLens, 1).AddIngredient(ItemID.Bone, 30).AddIngredient(ItemID.JungleSpores, 15).AddIngredient<RottingStalk>(30).AddTile(TileID.DemonAltar).Register();
            Recipe.Create(ItemID.VoidVault, 1).AddIngredient(ItemID.Bone, 15).AddIngredient(ItemID.JungleSpores, 8).AddIngredient<RottingStalk>(15).AddTile(TileID.DemonAltar).Register();
        }

    }
}