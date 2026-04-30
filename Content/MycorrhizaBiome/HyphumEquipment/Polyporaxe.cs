using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Mycorrhiza.Content.MycorrhizaBiome.HyphumEquipment
{
    public class Polyporaxe : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items";

        public override void SetStaticDefaults()
        {
            ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.damage = 21;
            Item.knockBack = 6f;
            Item.useTime = 31;
            Item.useAnimation = 31;
            Item.axe = 15;
            Item.DamageType = DamageClass.Melee;
            Item.useTurn = true;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.value = Item.sellPrice(silver: 27);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
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