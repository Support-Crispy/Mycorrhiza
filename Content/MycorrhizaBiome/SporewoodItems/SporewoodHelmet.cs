using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Mycorrhiza.Content.MycorrhizaBiome.SporewoodItems
{
    [AutoloadEquip(EquipType.Head)]
    public class SporewoodHelmet : ModItem, ILocalizedModType
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
            => (head.type, body.type, legs.type) == (Type, ModContent.ItemType<SporewoodBreastplate>(), ModContent.ItemType<SporewoodGreaves>());

        public override void AddRecipes()
        {
            CreateRecipe().
                AddIngredient<Sporewood>(20).
                AddTile(TileID.WorkBenches).
                Register();
        }
    }
}