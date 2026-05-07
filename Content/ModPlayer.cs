using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.DataStructures; 
using Mycorrhiza.Content.MycorrhizaBiome.BiomeTiles;
using Mycorrhiza.Content.MycorrhizaBiome.Plants;
using Mycorrhiza.Content.MycorrhizaBiome.BiomeTiles.Desert;
using Mycorrhiza.Content.MycorrhizaBiome.FishingLoot;

namespace Mycorrhiza.Players
{
	public class MycorrhizaPlayer : ModPlayer
	{
		public override void CatchFish(FishingAttempt attempt, ref int itemDrop, ref int npcSpawn, ref AdvancedPopupRequest sonar, ref Vector2 sonarPosition)
		{
			bool inMycorrhizaBiome = IsInMycorrhizaBiome(attempt.X, attempt.Y);

			if (!inMycorrhizaBiome) return;

			if (attempt.rare && Main.hardMode && Main.rand.NextFloat() < 0.005f)
			{
				itemDrop = ModContent.ItemType<Vereyfin>();
				sonar.Text = "Vereyfin";
				sonar.Color = Color.Cyan;
				sonar.Velocity = Vector2.Zero;
				sonar.DurationInFrames = 300;
				return;
			}

			if (attempt.crate)
			{
				if (Main.hardMode)
				{
					itemDrop = ModContent.ItemType<RottingCrate>();
					sonar.Text = "Rotting Crate";
					sonar.Color = Color.DarkRed;
				}
				else
				{
					itemDrop = ModContent.ItemType<MycorrhizalCrate>();
					sonar.Text = "Mycorrhizal Crate";
					sonar.Color = Color.Orange;
				}

				sonar.Velocity = Vector2.Zero;
				sonar.DurationInFrames = 300;
				return;
			}

			if (attempt.common)
			{
				if (Main.rand.NextBool(3))
				{
					itemDrop = ModContent.ItemType<HollowWoodfin>();
					sonar.Text = "Hollow Woodfin";
					sonar.Color = Color.PaleGoldenrod;
				}
				else
				{
					itemDrop = ModContent.ItemType<Sporeflopper>();
					sonar.Text = "Sporeflopper";
					sonar.Color = Color.PaleGreen;
				}

				sonar.Velocity = Vector2.Zero;
				sonar.DurationInFrames = 300;
				return;
			}
		}

		private bool IsInMycorrhizaBiome(int tileX, int tileY)
		{
			int radius = 40;
			int mycorrhizaTileCount = 0;
			int totalChecked = 0;

			for (int i = tileX - radius; i <= tileX + radius; i++)
			{
				for (int j = tileY - radius; j <= tileY + radius; j++)
				{
					if (!WorldGen.InWorld(i, j)) continue;

					Tile tile = Main.tile[i, j];
					if (tile == null || !tile.HasTile) continue;

					totalChecked++;

					if (tile.TileType == ModContent.TileType<PileustoneBlockPlaced>() ||
						tile.TileType == ModContent.TileType<MoldyGrassPlaced>() ||
						tile.TileType == ModContent.TileType<PileusandBlockPlaced>() ||
						tile.TileType == ModContent.TileType<PileusandstoneBlockPlaced>())
					{
						mycorrhizaTileCount++;
					}
				}
			}

			return totalChecked > 0 && (mycorrhizaTileCount > 50);
		}
	}
}