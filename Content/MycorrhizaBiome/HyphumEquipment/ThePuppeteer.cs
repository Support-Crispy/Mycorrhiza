using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Mycorrhiza.Content.MycorrhizaBiome.HyphumEquipment;

namespace Mycorrhiza.Content.MycorrhizaBiome.HyphumEquipment
{
    public class ThePuppeteer : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 40;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 8;
            Item.useAnimation = 8;
            Item.fishingPole = 22;
            Item.shootSpeed = 13.5f;
            Item.shoot = ModContent.ProjectileType<ThePuppeteerBobber>();
            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(gold: 3, silver: 12);
            Item.UseSound = SoundID.Item1;
            Item.noMelee = true;
            Item.autoReuse = false;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<HyphumBar>(), 8)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}