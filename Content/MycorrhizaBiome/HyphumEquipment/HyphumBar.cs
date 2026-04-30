using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Mycorrhiza.Content.MycorrhizaBiome.HyphumEquipment
{
    public class HyphumBar : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items";
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 25;
        }

        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<HyphumEquipment.HyphumBarPlaced>());
            Item.value = Item.sellPrice(silver: 30);
            Item.rare = ItemRarityID.Blue;
        }
        public override void AddRecipes()
        {
            CreateRecipe().
                AddIngredient<HyphumOre>(3).
                AddTile(TileID.Furnaces).
                Register();

            Recipe.Create(ItemID.Magiluminescence, 1).AddIngredient<HyphumBar>(12).AddIngredient(ItemID.Topaz, 5).AddTile(TileID.Anvils).Register();
            Recipe.Create(ItemID.ShadowCandle, 1).AddIngredient<HyphumBar>(3).AddIngredient(ItemID.Torch, 1).AddTile(TileID.WorkBenches).AddCondition(Condition.InGraveyard).Register();

        }
    }
}