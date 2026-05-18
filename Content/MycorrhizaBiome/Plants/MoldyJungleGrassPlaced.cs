using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using Microsoft.Xna.Framework;

namespace Mycorrhiza.Content.MycorrhizaBiome.Plants
{
    public class MoldyJungleGrassPlaced : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileMerge[TileID.Mud][Type] = true;
            Main.tileMerge[Type][TileID.Mud] = true;
            TileID.Sets.Grass[Type] = true;
            TileID.Sets.NeedsGrassFraming[Type] = true;
            AddMapEntry(new Color(100, 150, 100));
            DustType = ModContent.DustType<Dusts.MycoPlantDust>();
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
                        Main.tile[targetX, targetY].TileType == TileID.Mud &&
                        Main.tile[targetX, targetY].HasTile &&
                        IsExposedToAir(targetX, targetY))
                    {
                        Main.tile[targetX, targetY].TileType = (ushort)ModContent.TileType<MoldyJungleGrassPlaced>();
                        WorldGen.TileFrame(targetX, targetY);
                    }
                }
            }
        }

        public override void Convert(int i, int j, int conversionType)
        {
            switch (conversionType)
            {
                case BiomeConversionID.PurificationPowder:
                case BiomeConversionID.Purity:
                    WorldGen.ConvertTile(i, j, TileID.JungleGrass);
                    return;
                case BiomeConversionID.Corruption:
                    WorldGen.ConvertTile(i, j, TileID.CorruptJungleGrass);
                    return;
                case BiomeConversionID.Crimson:
                    WorldGen.ConvertTile(i, j, TileID.CrimsonJungleGrass);
                    return;
                case BiomeConversionID.Hallow:
                    WorldGen.ConvertTile(i, j, TileID.JungleGrass);
                    return;
            }
            WorldGen.SquareTileFrame(i, j);
            if (Main.netMode == NetmodeID.MultiplayerClient)
                NetMessage.SendTileSquare(-1, i, j);
        }

        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if (!effectOnly)
            {
                fail = true;
                Main.tile[i, j].TileType = TileID.Mud;
                WorldGen.TileFrame(i, j);
                if (Main.netMode != NetmodeID.SinglePlayer)
                    NetMessage.SendTileSquare(-1, i, j, 1);
            }
        }

        public override bool IsTileBiomeSightable(int i, int j, ref Color sightColor)
        {
            sightColor = Color.Red;
            return true;
        }
    }
}