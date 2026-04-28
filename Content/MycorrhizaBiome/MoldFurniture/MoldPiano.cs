using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Mycorrhiza.Content.MycorrhizaBiome.MoldFurniture
{
    public class MoldPiano : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items";
        public override void SetDefaults()
        {
            //Item.DefaultToPlaceableTile(ModContent.TileType<MoldPianoPlaced>());
            Item.value = Item.sellPrice(copper: 60);
        }

        public override void AddRecipes()
        {
            CreateRecipe().
                AddIngredient<MoldBlock>(15).
                AddIngredient(ItemID.Bone, 4).
                AddIngredient(ItemID.Book, 1).
                AddTile(TileID.Sawmill).
                Register();
        }
    }
}