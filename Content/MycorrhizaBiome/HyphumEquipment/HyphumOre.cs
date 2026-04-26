using Terraria.ID;
using Terraria.ModLoader;
using System;

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

    }
}