using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Mycorrhiza.Content.MycorrhizaBiome.Plants
{
    public class MoldyGrassSeeds : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 14;
            Item.height = 14;
            Item.maxStack = Item.CommonMaxStack;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.value = Item.buyPrice(copper: 10);
            Item.rare = ItemRarityID.Blue;
        }

        public override bool CanUseItem(Player player)
        {
            int x = (int)(Main.MouseWorld.X / 16);
            int y = (int)(Main.MouseWorld.Y / 16);

            Tile tile = Main.tile[x, y];

            return tile.HasTile && tile.TileType == TileID.Dirt;
        }

        public override bool? UseItem(Player player)
        {
            int x = (int)(Main.MouseWorld.X / 16);
            int y = (int)(Main.MouseWorld.Y / 16);

            if (WorldGen.InWorld(x, y))
            {
                Tile tile = Main.tile[x, y];

                if (tile.HasTile && tile.TileType == TileID.Dirt)
                {
                    tile.TileType = (ushort)ModContent.TileType<Plants.MoldyGrassPlaced>();

                    WorldGen.TileFrame(x, y);
                    for (int i = 0; i < 10; i++)
                    {
                        Dust.NewDust(new Vector2(x * 16, y * 16), 16, 16, DustID.Grass, 0f, 0f, 100, default, 1f);
                    }

                    return true;
                }
            }

            return false;
        }
    }
}