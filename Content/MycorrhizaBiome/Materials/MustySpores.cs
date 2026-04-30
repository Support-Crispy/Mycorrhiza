using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Mycorrhiza.Content.MycorrhizaBiome.Materials

{
	public class MustySpores : ModItem, ILocalizedModType
	{
		public new string LocalizationCategory => "Items";

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 25;
        }

        public override void SetDefaults()
		{
            Item.value = Item.sellPrice(copper: 2);
            Item.maxStack = Item.CommonMaxStack;
            Item.rare = ItemRarityID.White;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Swing;
        }

        public override void AddRecipes()
        {
            Recipe.Create(ItemID.BattlePotion, 1).AddIngredient(ItemID.BottledWater, 1).AddIngredient(ItemID.Deathweed, 1).AddIngredient<MustySpores>(1).AddTile(TileID.Bottles).Register();
            Recipe.Create(ItemID.CoffinMinecart, 1).AddRecipeGroup(RecipeGroupID.IronBar, 5).AddRecipeGroup(RecipeGroupID.Wood, 2).AddIngredient<MustySpores>(10).AddTile(TileID.Bottles).Register();
            Recipe.Create(ItemID.MonsterLasagna, 1).AddIngredient<MustySpores>(8).AddTile(TileID.CookingPots).Register();
            Recipe.Create(ItemID.MechanicalWorm, 1).AddIngredient<MustySpores>(6).AddRecipeGroup(RecipeGroupID.IronBar, 5).AddIngredient(ItemID.SoulofNight, 6).AddTile(TileID.MythrilAnvil).Register();
        }

    }
}