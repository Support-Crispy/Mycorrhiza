using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Mycorrhiza.Content.MycorrhizaBiome.HyphumEquipment
{
	public class ThePuppeteer : ModItem, ILocalizedModType
	{
		public new string LocalizationCategory => "Items";

		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
		}

        public override void AddRecipes()
        {
            CreateRecipe().
                AddIngredient<HyphumBar>(8).
                AddTile(TileID.Anvils).
                Register();
        }
    }
}