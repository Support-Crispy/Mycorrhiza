using Terraria.ID;
using Terraria.ModLoader;

namespace Mycorrhiza.Content.MycorrhizaBiome.HyphumEquipment
{
    public class Saprophyte : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Melee";
        public override void SetDefaults()
        {
            Item.damage = 30;
            Item.width = 36;
            Item.height = 40;
            Item.DamageType = DamageClass.Melee;
            Item.useAnimation = Item.useTime = 25;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 6f;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.rare = ItemRarityID.White;
        }
        public override void AddRecipes()
        {
            CreateRecipe().
                AddIngredient<HyphumBar>(10).
                AddTile(TileID.Anvils).
                Register();
        }

    }
}