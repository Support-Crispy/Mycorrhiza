using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.DataStructures;

namespace Mycorrhiza.Content.MycorrhizaBiome.Plants
{
    public class MycorrhizaLargePlants : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileCut[Type] = true;
            Main.tileNoFail[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileID.Sets.SwaysInWindBasic[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2);
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.StyleWrapLimit = 12;
            TileObjectData.newTile.RandomStyleRange = 12;
            TileObjectData.newTile.AnchorValidTiles = new int[] { ModContent.TileType<MoldyGrassPlaced>() };
            TileObjectData.addTile(Type);

            AddMapEntry(new Color(100, 150, 100));
            DustType = ModContent.DustType<Dusts.MycoPlantDust>();
        }
    }
}