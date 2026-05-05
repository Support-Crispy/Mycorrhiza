using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.ModLoader;

namespace Mycorrhiza.Content.MycorrhizaBiome.HyphumEquipment
{
	public class StonefeederPickaxe : ModItem, ILocalizedModType
	{
		public new string LocalizationCategory => "Items";

        public override void SetDefaults()
        {
            Item.damage = 24;
            Item.DamageType = DamageClass.Melee;
            Item.width = 40;
            Item.height = 40;
            // On the official wiki, https://terraria.wiki.gg/wiki/Pickaxes, the "Use time" column corresponds to Item.useAnimation and the "Mining speed" column corresponds to Item.useTime.
            Item.useTime = 14;
            Item.useAnimation = 22;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 6;
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;

            Item.pick = 70; // How strong the pickaxe is, see https://terraria.wiki.gg/wiki/Pickaxe_power for a list of common values
            Item.attackSpeedOnlyAffectsWeaponAnimation = true; // Melee speed affects how fast the tool swings for damage purposes, but not how fast it can dig
        }

        public override void AddRecipes()
        {
            CreateRecipe().
                AddIngredient<HyphumBar>(12).
                AddIngredient<Materials.RottingStalk>(6).
                AddTile(TileID.Anvils).
                Register();
        }
    }
}