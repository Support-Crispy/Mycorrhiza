using Terraria.ID;
using Terraria.ModLoader;

namespace Mycorrhiza.Content.MycorrhizaBiome
{
    public class MycorrhizaWaterSplashDust : ModDust
    {
        public override void SetStaticDefaults()
        {
            UpdateType = DustID.PureSpray;
        }
    }
}