using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Mycorrhiza.Content.MycorrhizaBiome.HyphumEquipment
{
    public class MycosisYoyo : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.YoyosLifeTimeMultiplier[Projectile.type] = 6f;

            ProjectileID.Sets.YoyosMaximumRange[Projectile.type] = 200;

            ProjectileID.Sets.YoyosTopSpeed[Projectile.type] = 12.5f;
        }

        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;

            Projectile.aiStyle = ProjAIStyleID.Yoyo; 

            Projectile.friendly = true; 
            Projectile.DamageType = DamageClass.MeleeNoSpeed; 
            Projectile.penetrate = -1;  
        }

        public override void PostAI()
        {
            if (Main.rand.NextBool(5))
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<Dusts.MycoMoldDust>()); // Makes the projectile emit dust.
            }
        }
    }
}