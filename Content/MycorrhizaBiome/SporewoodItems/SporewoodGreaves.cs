using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Mycorrhiza.Content.MycorrhizaBiome.SporewoodItems
{
    [AutoloadEquip(EquipType.Legs)]
    public class SporewoodGreaves : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items";

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 20;
            Item.defense = 1;
        }

        public override void AddRecipes()
        {
            CreateRecipe().
                AddIngredient<Sporewood>(25).
                AddTile(TileID.WorkBenches).
                Register();
        }
    }
}