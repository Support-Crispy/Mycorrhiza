using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Mycorrhiza.Content.MycorrhizaBiome.HyphumEquipment
{
    public class HyphumOre : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Placeables";
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 100;
        }

        public override void SetDefaults() => Item.DefaultToPlaceableTile(ModContent.TileType<HyphumOrePlaced>());

        public override void AddRecipes()
        {
            Recipe.Create(ItemID.DeerThing, 1).AddIngredient(ItemID.FlinxFur, 3).AddIngredient<HyphumOre>(5).AddIngredient(ItemID.Lens, 1).AddTile(TileID.DemonAltar).Register();
        }

    }
}