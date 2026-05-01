using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace Mycorrhiza.Content.MycorrhizaBiome.MoldFurniture
{
    public class MoldBlock : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items";
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 100;
        }

        public override void SetDefaults() => Item.DefaultToPlaceableTile(ModContent.TileType<MoldBlockPlaced>());

        public override void AddRecipes()
        {
            CreateRecipe().
                AddIngredient<BiomeTiles.PileustoneBlock>(2).
                AddTile(TileID.MeatGrinder).
                Register();

            CreateRecipe().
                AddIngredient<MoldBlockWall>(4).
                AddTile(TileID.WorkBenches).
                Register();

            CreateRecipe().
                AddIngredient<PaleMoldBlockWall>(4).
                AddTile(TileID.WorkBenches).
                Register();

            CreateRecipe().
                AddIngredient<MoldPlatform>(2).
                Register();
        }
    }
}