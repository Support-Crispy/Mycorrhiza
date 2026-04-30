using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Mycorrhiza.Content.MycorrhizaBiome.SporewoodItems
{
	public class SporewoodBreastplate : ModItem, ILocalizedModType
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
                AddIngredient<Sporewood>(30).
                AddTile(TileID.WorkBenches).
                Register();
        }
    }
}