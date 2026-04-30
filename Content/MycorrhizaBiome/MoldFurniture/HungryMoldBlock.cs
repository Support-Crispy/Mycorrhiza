using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace Mycorrhiza.Content.MycorrhizaBiome.MoldFurniture
{
    public class HungryMoldBlock : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items";
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 100;
        }

        public override void SetDefaults() => Item.DefaultToPlaceableTile(ModContent.TileType<HungryMoldBlockPlaced>());

        public override void AddRecipes()
        {
            CreateRecipe().
                AddIngredient<MoldBlock>(1).
                AddTile(TileID.DemonAltar).
                Register();
        }
    }
}