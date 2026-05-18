using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.DataStructures;
using System;

namespace Mycorrhiza.Content.MycorrhizaBiome
{
    public class MycorrhizaWaterFountainPlaced : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileID.Sets.CountsAsWaterSource[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
            TileObjectData.newTile.Height = 4;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16, 16 };
            TileObjectData.newTile.DrawYOffset = 2;
            TileObjectData.addTile(Type);

            AddMapEntry(new Color(0, 100, 200));
            DustType = DustID.Water;
        }

        public override void NearbyEffects(int i, int j, bool closer)
        {
            if (closer)
            {
                Main.waterStyle = ModContent.GetInstance<MycorrhizaBiomeWaterStyle>().Slot;
            }
        }

        public override bool RightClick(int i, int j)
        {
            int left = i - Main.tile[i, j].TileFrameX / 18 % 2;
            int top = j - Main.tile[i, j].TileFrameY / 18 % 4;

            int state = Main.tile[left, top].TileFrameX >= 36 ? 0 : 36;
            for (int x = left; x < left + 2; x++)
            {
                for (int y = top; y < top + 4; y++)
                {
                    Main.tile[x, y].TileFrameX = (short)(Main.tile[x, y].TileFrameX % 36 + state);
                }
            }
            if (Main.netMode != NetmodeID.SinglePlayer)
                NetMessage.SendTileSquare(-1, left, top, 2, 4);
            return true;
        }

        public override void MouseOver(int i, int j)
        {
            Player player = Main.LocalPlayer;
            player.noThrow = 2;
            player.cursorItemIconEnabled = true;
            player.cursorItemIconID = ModContent.ItemType<MycorrhizaWaterFountain>();
        }
    }
}