using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Mycorrhiza.Content.MycorrhizaBiome.Plants
{
	public class DeathweedTweaks : GlobalTile
	{
		public override bool CanPlace(int i, int j, int type)
		{
			if (type == TileID.ImmatureHerbs && Main.LocalPlayer.HeldItem.type == ItemID.DeathweedSeeds)
			{
				Tile floorTile = Main.tile[i, j + 1];
				ushort customGrassType = (ushort)ModContent.TileType<MoldyGrassPlaced>();

				if (floorTile.HasTile && floorTile.TileType == customGrassType)
				{
					return true;
				}
			}

			return base.CanPlace(i, j, type);
		}
	}
}