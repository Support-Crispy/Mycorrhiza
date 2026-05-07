using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Mycorrhiza.Content.MycorrhizaBiome.HyphalSparks
{
	public class LivingHyphalFireBlock : ModItem, ILocalizedModType
	{
		public new string LocalizationCategory => "Items";

        public static Vector3 LightColor = new Vector3(1.0f, 0.549f, 0.918f);

        public override void SetStaticDefaults()
        {
            ItemID.Sets.IsLavaImmuneRegardlessOfRarity[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<LivingHyphalFireBlockPlaced>());
            Item.width = 12;
            Item.height = 12;
        }

        public override void PostUpdate()
        {
            // Curiously, only the regular Living Fire Block creates light.
            Lighting.AddLight(Item.Center, LightColor);
        }

        public override void AddRecipes()
        {
            CreateRecipe(20).
                AddIngredient(ItemID.LivingFireBlock, 20).
                AddIngredient<HyphalSparks>(1).
                AddTile(TileID.CrystalBall).
                Register();
        }
    }
}