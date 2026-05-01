using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Mycorrhiza.Content.MycorrhizaBiome.HyphumEquipment
{
    [AutoloadEquip(EquipType.Head)]
    public class RhyzalHat : ModItem, ILocalizedModType
	{
		public new string LocalizationCategory => "Items";

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 20;
            Item.value = Item.sellPrice(silver: 75);
            Item.defense = 1;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
            => (head.type, body.type, legs.type) == (Type, ModContent.ItemType<RhyzalBreastplate>(), ModContent.ItemType<RhyzalGreaves>());

        public override void AddRecipes()
        {
            CreateRecipe().
                AddIngredient<HyphumBar>(15).
                AddIngredient<Materials.RottingStalk>(10).
                AddTile(TileID.Anvils).
                Register();
        }
    }
}