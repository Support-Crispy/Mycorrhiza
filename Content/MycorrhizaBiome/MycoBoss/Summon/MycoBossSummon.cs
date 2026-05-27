using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

//Placeholder, finish me!

namespace Mycorrhiza.Content.MycorrhizaBiome.MycoBoss.Summon

{
	public class MycoBossSummon : ModItem, ILocalizedModType
	{
		public new string LocalizationCategory => "Items";

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 25;
        }

        public override void SetDefaults()
		{
            Item.maxStack = Item.CommonMaxStack;
            Item.rare = ItemRarityID.Blue;
            Item.useTime = 45;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Swing;
        }

        public override void AddRecipes()
        {
            CreateRecipe().
                AddIngredient<Plants.VitriolicPowder>(30).
                AddIngredient<Materials.MustySpores>(15).
                AddTile(TileID.DemonAltar).
                Register();
        }

    }
}