using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace Mycorrhiza.Content.MycorrhizaBiome.BiomeTiles
{
    public class PileustoneBrick : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items";
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 100;
        }

        public override void SetDefaults() => Item.DefaultToPlaceableTile(ModContent.TileType<PileustoneBrickPlaced>());

        public override void AddRecipes()
        {
            CreateRecipe().
                AddIngredient<PileustoneBlock>(2).
                AddTile(TileID.Furnaces).
                Register();

            CreateRecipe().
                AddIngredient<PileustoneBrickWall>(4).
                AddTile(TileID.WorkBenches).
                Register();
        }

    }
}