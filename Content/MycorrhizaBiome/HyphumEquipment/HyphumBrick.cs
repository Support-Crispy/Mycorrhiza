using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace Mycorrhiza.Content.MycorrhizaBiome.HyphumEquipment
{
    public class HyphumBrick : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items";
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 100;
        }

        public override void SetDefaults() => Item.DefaultToPlaceableTile(ModContent.TileType<HyphumBrickPlaced>());


        public override void AddRecipes()
        {
            CreateRecipe(5).
                AddIngredient<HyphumOre>(1).
                AddIngredient<BiomeTiles.PileustoneBlock>(5).
                AddTile(TileID.Furnaces).
                Register();

            CreateRecipe().
                AddIngredient<HyphumBrickWall>(4).
                DisableDecraft().
                Register();
       
        }
    }
}