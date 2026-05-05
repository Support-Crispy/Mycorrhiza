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
            Item.noMelee = true; // This makes it so the item doesn't do damage to enemies (the projectile does that).
            Item.noUseGraphic = true;
            Item.UseSound = SoundID.Item1; 

            Item.damage = 12; // The amount of damage the item does to an enemy or player.
            Item.DamageType = DamageClass.MeleeNoSpeed; 
            Item.knockBack = 2.5f; 
            Item.crit = 20; // The percent chance for the weapon to deal a critical strike. Defaults to 4.
            Item.channel = true;
            Item.rare = ItemRarityID.Blue;

            Item.shoot = ModContent.ProjectileType<MycosisYoyo>(); // Which projectile this item will shoot. We set this to our corresponding projectile.
            Item.shootSpeed = 16f; // The velocity of the shot projectile.			
        }

        // Here is an example of blacklisting certain modifiers. Remove this section for standard vanilla behavior.
        // In this example, we are blacklisting the ones that reduce damage of a melee weapon.
        // Make sure that your item can even receive these prefixes (check the vanilla wiki on prefixes).
        private static readonly int[] unwantedPrefixes = [PrefixID.Legendary];

        public override bool AllowPrefix(int pre)
        {
            // return false to make the game reroll the prefix.

            // DON'T DO THIS BY ITSELF:
            // return false;
            // This will get the game stuck because it will try to reroll every time. Instead, make it have a chance to return true.

            if (Array.IndexOf(unwantedPrefixes, pre) > -1)
            {
                // IndexOf returns a positive index of the element you search for. If not found, it's less than 0.
                // Here we check if the selected prefix is positive (it was found).
                // If so, we found a prefix that we don't want. Reroll.
                return false;
            }

            // Don't reroll
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