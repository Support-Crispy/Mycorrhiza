using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace Mycorrhiza.Content.MycorrhizaBiome.SporewoodItems
{
    public class Sporewood : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items";
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 100;
            ItemID.Sets.ShimmerTransformToItem[Type] = ItemID.Wood;
        }

        public override void SetDefaults() => Item.DefaultToPlaceableTile(ModContent.TileType<SporewoodPlaced>());

        public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
        {
            itemGroup = ContentSamples.CreativeHelper.ItemGroup.Wood;
        }

       // public override void AddRecipes()
       // {
          //  CreateRecipe().
          //      AddIngredient<SporewoodPlatform>(2).
          //      DisableDecraft().
          //      Register();
          //  CreateRecipe().
          //      AddIngredient<SporewoodWall>(4).
          //      AddTile(TileID.WorkBenches).
          //      DisableDecraft().
          //      Register();
        // }
    }
}