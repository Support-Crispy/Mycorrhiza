using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Mycorrhiza.Content.MycorrhizaBiome.HyphumEquipment
{
	public class Fusarium : ModItem, ILocalizedModType
	{
		public new string LocalizationCategory => "Items";

        public override void SetDefaults()
        {

            Item.DefaultToWhip(ModContent.ProjectileType<FusariumWhip>(), 20, 2, 4);
            Item.rare = ItemRarityID.Green;
            Item.channel = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe().
                AddIngredient<HyphumBar>(12).
                AddTile(TileID.Anvils).
                Register();
        }

        public override bool MeleePrefix()
        {
            return true;
        }
    }
}