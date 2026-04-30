using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;

namespace Mycorrhiza.Content.MycorrhizaBiome.Plants
{
    public class MoldyGrassPlaced : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileMergeDirt[Type] = true;
            TileID.Sets.Grass[Type] = true;
            TileID.Sets.NeedsGrassFraming[Type] = true;

            AddMapEntry(new Color(100, 150, 100));
        }

        private static bool IsExposedToAir(int x, int y)
        {
            if (!Main.tile[x, y - 1].HasTile) return true; 
            if (!Main.tile[x, y + 1].HasTile) return true;
            if (!Main.tile[x - 1, y].HasTile) return true; 
            if (!Main.tile[x + 1, y].HasTile) return true; 

            if (!Main.tile[x - 1, y - 1].HasTile) return true;
            if (!Main.tile[x + 1, y - 1].HasTile) return true;
            if (!Main.tile[x - 1, y + 1].HasTile) return true;
            if (!Main.tile[x + 1, y + 1].HasTile) return true;

            return false;
        }

        public override void RandomUpdate(int i, int j)
        {
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    int targetX = i + x;
                    int targetY = j + y;

                    if (WorldGen.InWorld(targetX, targetY) &&
                        Main.tile[targetX, targetY].TileType == TileID.Dirt &&
                        Main.tile[targetX, targetY].HasTile && IsExposedToAir(targetX, targetY))
                    {
                        Tile tile = Main.tile[targetX, targetY];
                        tile.TileType = (ushort)ModContent.TileType<MoldyGrassPlaced>();
                        WorldGen.TileFrame(targetX, targetY);
                    }
                }
            }
        }

        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if (!effectOnly && !fail)
            {
                WorldGen.PlaceTile(i, j, TileID.Dirt);
            }
        }

        public override bool IsTileBiomeSightable(int i, int j, ref Color sightColor)
        {
            sightColor = Color.Red;
            return true;
        }
    }

}