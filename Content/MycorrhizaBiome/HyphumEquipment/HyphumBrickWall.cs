using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace Mycorrhiza.Content.MycorrhizaBiome.HyphumEquipment
{
    public class HyphumBrickWall : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items";
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 400;
        }

        public override void SetDefaults() => Item.DefaultToPlaceableWall(ModContent.WallType<HyphumBrickWallPlaced>());

        public override void AddRecipes()
        {
            CreateRecipe(4).
                AddIngredient<HyphumBrick>().
                DisableDecraft().
                Register();
          }
    }
}