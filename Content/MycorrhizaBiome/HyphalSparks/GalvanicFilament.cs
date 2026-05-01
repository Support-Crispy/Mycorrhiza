using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Mycorrhiza.Content.MycorrhizaBiome.HyphalSparks
{
	public class GalvanicFilament : ModItem, ILocalizedModType
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
                AddIngredient(ItemID.SpellTome, 1).
                AddIngredient<HyphalSparks>(20).
                AddIngredient(ItemID.SoulofNight, 15).
                AddTile(TileID.Bookcases).
                Register();
        }
    }
}