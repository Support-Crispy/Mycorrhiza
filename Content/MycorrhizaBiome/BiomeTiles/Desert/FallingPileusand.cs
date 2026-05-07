using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

//This still needs a sprite

namespace Mycorrhiza.Content.MycorrhizaBiome.BiomeTiles.Desert
{
    public abstract class FallingPileusand : ModProjectile
    {
        public override string Texture => "Mycorrhiza/Content/MycorrhizaBiome/BiomeTiles/Desert/FallingPileusand";

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.FallingBlockDoesNotFallThroughPlatforms[Type] = true;
            ProjectileID.Sets.ForcePlateDetection[Type] = true;
        }
    }

    public class FallingPileusandBall : FallingPileusand
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            ProjectileID.Sets.FallingBlockTileItem[Type] = new(ModContent.TileType<PileusandBlockPlaced>(), ModContent.ItemType<PileusandBlock>());
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.EbonsandBallFalling);
        }
    }

    public class Pileusandball : FallingPileusand
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            ProjectileID.Sets.FallingBlockTileItem[Type] = new(ModContent.TileType<PileusandBlockPlaced>());
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.EbonsandBallGun);
            AIType = ProjectileID.EbonsandBallGun;
        }
    }
}