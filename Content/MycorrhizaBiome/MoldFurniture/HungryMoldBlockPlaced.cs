using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;

namespace Mycorrhiza.Content.MycorrhizaBiome.MoldFurniture
{
    public class HungryMoldBlockPlaced : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;

            AddMapEntry(new Color(100, 150, 100));
        }


        public override void RandomUpdate(int i, int j)
        {
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    int targetX = i + x;
                    int targetY = j + y;

                    if (WorldGen.InWorld(targetX, targetY))
                    {
                        Tile tile = Main.tile[targetX, targetY];
                        tile.TileType = (ushort)ModContent.TileType<HungryMoldBlockPlaced>();
                        WorldGen.TileFrame(targetX, targetY);
                    }
                }
            }
        }

        public override bool IsTileBiomeSightable(int i, int j, ref Color sightColor)
        {
            sightColor = Color.Black;
            return true;
        }
    }

}