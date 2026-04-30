using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace Mycorrhiza.Content.MycorrhizaBiome
{
	public class MycorrhizaBiomeWaterStyle : ModWaterStyle
	{
		//private Asset<Texture2D> rainTexture;
		public override void Load()
		{
			//rainTexture = Mod.Assets.Request<Texture2D>("Content/Biomes/ExampleRain");
		}

		public override int ChooseWaterfallStyle()
		{
			return ModContent.GetInstance<MycorrhizaWaterfallStyle>().Slot;
		}

		public override int GetSplashDust()
		{
			return ModContent.DustType<MycorrhizaWaterSplashDust>();
		}

		public override int GetDropletGore()
		{
			return ModContent.GoreType<MycorrhizaWaterDroplet>();
		}

		public override void LightColorMultiplier(ref float r, ref float g, ref float b)
		{
			r = 1f;
			g = 1f;
			b = 1f;
		}

		public override Color BiomeHairColor()
		{
			return Color.White;
		}

		//public override byte GetRainVariant()
		//{
		//	return (byte)Main.rand.Next(3);
		//}

		//public override Asset<Texture2D> GetRainTexture() => rainTexture;
	}
}