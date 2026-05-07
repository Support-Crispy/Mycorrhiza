using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Mycorrhiza.Content.MycorrhizaBiome.HyphumEquipment
{
	public class Mycosis : ModItem, ILocalizedModType
	{
		public new string LocalizationCategory => "Items";

        public override void SetStaticDefaults()
        {
            ItemID.Sets.Yoyo[Item.type] = true; 
            ItemID.Sets.GamepadExtraRange[Item.type] = 10; 
            ItemID.Sets.GamepadSmartQuickReach[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 24; 
            Item.height = 24; 

            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useTime = 25; 
            Item.useAnimation = 25; 
            Item.noMelee = true; 
            Item.noUseGraphic = true;
            Item.UseSound = SoundID.Item1; 

            Item.damage = 12; 
            Item.DamageType = DamageClass.MeleeNoSpeed; 
            Item.knockBack = 2.5f; 
            Item.crit = 20;
            Item.channel = true;
            Item.rare = ItemRarityID.Blue;

            Item.shoot = ModContent.ProjectileType<MycosisYoyo>(); 
            Item.shootSpeed = 16f;			
        }

        private static readonly int[] unwantedPrefixes = [PrefixID.Legendary];

        public override bool AllowPrefix(int pre)
        {
            if (Array.IndexOf(unwantedPrefixes, pre) > -1)
            {
                return false;
            }

            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe().
                AddIngredient<HyphumBar>(12).
                AddTile(TileID.Anvils).
                Register();
        }
    }
}