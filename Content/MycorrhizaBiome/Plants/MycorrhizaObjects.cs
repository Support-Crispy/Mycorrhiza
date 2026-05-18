using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Mycorrhiza.Content.MycorrhizaBiome.Plants
{
    public class MycorrhizaObjects : ModTile
    {
        public override void SetStaticDefaults()
        {

            Main.tileFrameImportant[Type] = true;
            Main.tileNoFail[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileObjectData.newTile.Width = 3;
            TileObjectData.newTile.Height = 2;
            TileObjectData.newTile.Origin = new Terraria.DataStructures.Point16(1, 1);
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16 };
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.newTile.StyleHorizontal = true;

            TileObjectData.newTile.StyleWrapLimit = 15;
            TileObjectData.newTile.RandomStyleRange = 5;

            TileObjectData.newTile.Direction = Terraria.Enums.TileObjectDirection.None;

            TileObjectData.newTile.AnchorBottom = new Terraria.DataStructures.AnchorData(
                Terraria.Enums.AnchorType.SolidTile | Terraria.Enums.AnchorType.SolidWithTop | Terraria.Enums.AnchorType.SolidSide,
                TileObjectData.newTile.Width,
                0
            );
            TileObjectData.newTile.UsesCustomCanPlace = true;
            TileObjectData.newTile.AnchorValidTiles = new int[] { ModContent.TileType<MoldyGrassPlaced>() };

            TileObjectData.addTile(Type);

            AddMapEntry(new Color(100, 150, 100));
            DustType = ModContent.DustType<Dusts.MycoPlantDust>();
        }

        public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
        {
            offsetY = 2; 
        }
    }
}