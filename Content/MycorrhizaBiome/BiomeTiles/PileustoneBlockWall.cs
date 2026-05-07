using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace Mycorrhiza.Content.MycorrhizaBiome.BiomeTiles
{
    public class PileustoneBlockWall : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items";
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 400;
        }

        public override void SetDefaults() => Item.DefaultToPlaceableWall(ModContent.WallType<PileustoneBlockWallPlaced>());

        public override void AddRecipes()
        {
            CreateRecipe(4).
                AddIngredient<PileustoneBlock>().
                AddTile(TileID.WorkBenches).
                DisableDecraft().
                Register();
          }
    }
}