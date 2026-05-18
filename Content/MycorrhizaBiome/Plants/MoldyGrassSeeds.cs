using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.GameContent;

namespace Mycorrhiza.Content.MycorrhizaBiome.Plants
{
    public class MoldyGrassSeeds : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 25;

            FlexibleTileWand.RubblePlacementLarge.AddVariations(Type, ModContent.TileType<MycorrhizaObjects>(), 0, 1, 2, 3, 4);
        }

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
            int x = Player.tileTargetX;
            int y = Player.tileTargetY;

            if (!WorldGen.InWorld(x, y))
                return false;

            Tile tile = Main.tile[x, y];
            return tile.HasTile && (tile.TileType == TileID.Dirt || tile.TileType == TileID.Mud);
        }

        public override bool? UseItem(Player player)
        {
            int x = Player.tileTargetX;
            int y = Player.tileTargetY;

            if (WorldGen.InWorld(x, y))
            {
                Tile tile = Main.tile[x, y];

                if (tile.HasTile && tile.TileType == TileID.Dirt)
                {
                    tile.TileType = (ushort)ModContent.TileType<Plants.MoldyGrassPlaced>();
                    WorldGen.TileFrame(x, y);

                    if (Main.netMode == NetmodeID.MultiplayerClient)
                        NetMessage.SendTileSquare(-1, x, y, 1);

                    for (int i = 0; i < 10; i++)
                    {
                        Dust.NewDust(new Vector2(x * 16, y * 16), 16, 16, DustID.Grass, 0f, 0f, 100, default, 1f);
                    }
                    return true;
                }

                if (tile.HasTile && tile.TileType == TileID.Mud)
                {
                    tile.TileType = (ushort)ModContent.TileType<Plants.MoldyJungleGrassPlaced>();
                    WorldGen.TileFrame(x, y);

                    if (Main.netMode == NetmodeID.MultiplayerClient)
                        NetMessage.SendTileSquare(-1, x, y, 1);

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