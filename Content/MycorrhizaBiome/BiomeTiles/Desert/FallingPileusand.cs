using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

// This file contains ExampleSandBallProjectile, ExampleSandBallFallingProjectile, and ExampleSandBallGunProjectile.
// ExampleSandBallFallingProjectile and ExampleSandBallGunProjectile inherit from ExampleSandBallProjectile, allowing cleaner code and shared logic.
// ExampleSandBallFallingProjectile is the projectile that spawns when the ExampleSand tile falls.
// ExampleSandBallGunProjectile is the projectile that is shot by the Sandgun weapon.
// Both projectiles share the same aiStyle, ProjAIStyleID.FallingTile, but the AIType line in ExampleSandBallGunProjectile ensures that specific logic of the aiStyle is used for the sandgun projectile.
// It is possible to make a falling projectile not using ProjAIStyleID.FallingTile, but it is a lot of code.
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
            // The falling projectile when compared to the sandgun projectile is hostile.
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
            // The sandgun projectile when compared to the falling projectile has a ranged damage type, isn't hostile, and has extraupdates = 1.
            // Note that EbonsandBallGun has infinite penetration, unlike SandBallGun
            Projectile.CloneDefaults(ProjectileID.EbonsandBallGun);
            AIType = ProjectileID.EbonsandBallGun; // This is needed for some logic in the ProjAIStyleID.FallingTile code.
        }
    }
}