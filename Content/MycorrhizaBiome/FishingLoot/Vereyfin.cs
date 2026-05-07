using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Mycorrhiza.Content.MycorrhizaBiome.FishingLoot
{
    public class Vereyfin : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.maxStack = 1;
            Item.value = Item.sellPrice(silver: 3);
            Item.rare = ItemRarityID.Green;
        }
    }
}