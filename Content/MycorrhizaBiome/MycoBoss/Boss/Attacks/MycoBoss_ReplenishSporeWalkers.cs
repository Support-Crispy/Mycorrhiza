using BreadLibrary.Core.Graphics.Particles;
using BreadLibrary.Core.ScreenShake;
using BreadLibrary.Core.Utilities;
using Mycorrhiza.Content.Particles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mycorrhiza.Content.MycorrhizaBiome.MycoBoss.Boss.Attacks
{
    internal class MycoBoss_ReplenishSporeWalkers : _MycoBoss_Attack
    {

        private static int WindupToBurryTime
        {
            get
            {
                if (Main.zenithWorld)
                    return 40;

                else
                    return 50;
            }
        }
        private static int FullyBurriedTime
        {
            get
            {
                return 70;
            }
        }

        private static int BeginRipOutTime
        {
            get => FullyBurriedTime + 10;
        }

        private static int RipOutTime
        {
            get
            {
                return BeginRipOutTime + 120;
            }
        }

        public override MycoBoss_State ID => MycoBoss_State.Replenish_Sporewalkers;

        public Dictionary<int, Vector2> HitCoords = new();
        public override void Update(MycoBoss_NPC boss)
        {
            HitCoords ??= new();


            NPC NPC = boss.NPC;
            int Timer = boss.Timer;
            NPC.velocity.X *= 0;
            //find Points Nearby The NPC.


            if (boss.DanglingSporewalkers.Count > 0)
            {
                this.Exit(boss);
                return;

            }

            if (boss.DesiredHeight > MycoBoss_NPC.BaseDesiredHeight)
            {
                boss.DesiredHeight = MycoBoss_NPC.BaseDesiredHeight;
            }

            int i = 0;
            
            foreach(var tendril in boss.Tendrils)
            {
                Vector2 midway = Vector2.Lerp(tendril.Chain.Positions[0], tendril.Chain.Positions[^1], 0.5f);

                bool flip = i >= MycoBoss_NPC.MaxTendrils / 2;
                float interp = flip ? -30 : 30;

                interp *= (1 - boss.DesiredHeight / (float)MycoBoss_NPC.BaseDesiredHeight);
                Vector2 AimedEnd = Vector2.UnitY.RotatedBy(MathHelper.ToRadians(interp)) * 1000;

                if (!HitCoords.ContainsKey(i))
                {
                    Point? Hit = Utilities.RaycastTo(midway, midway + AimedEnd, debug: true);

                    if (Hit.HasValue)
                    {
                        HitCoords.TryAdd(i, Hit.Value.ToWorldCoordinates());
                    }
                }
               



                i++;
            }

            if (boss.Timer < WindupToBurryTime)
                return;



            i = 0;
            //plunge tendrils into the ground
            if (Timer<RipOutTime)
                if (HitCoords.Count == MycoBoss_NPC.MaxTendrils)
                {
                    float interp = Math.Clamp(Timer / (int)FullyBurriedTime, 0, 1);

                    boss.DesiredHeight = float.Lerp(boss.DesiredHeight, 140, interp);

                    foreach (MycoTendril tendril in boss.Tendrils)
                    {

                        ref Vector2 Chain = ref tendril.Chain.Positions[tendril.Chain.Positions.Length / 2];
                        Chain = Vector2.Lerp(Chain, HitCoords[i], interp);
                        i++;
                    }
                }
            //shake screen and such
            if(Timer> BeginRipOutTime && Timer < RipOutTime)
            {

                ScreenShakeSystem.ShakeAt(boss.NPC.Center, 10, 60);
            }


            i = 0;
            if (Timer > RipOutTime)
            {
                boss.DesiredHeight = float.Lerp(boss.DesiredHeight, MycoBoss_NPC.BaseDesiredHeight + 100, 0.2f);

                if(Timer == RipOutTime + 1)
                {
                    foreach (MycoTendril tendril in boss.Tendrils)
                    {
                        ref Vector2 Chain = ref tendril.Chain.Positions[tendril.Chain.Positions.Length / 2];

                        for(int x = 0; x< 10; x++)
                        {

                            MushBoom Particle = new();
                            Particle.Prepare(Chain, Vector2.UnitY * -1 * Main.rand.NextFloat(), Main.rand.NextFloat(), 50);

                            ParticleEngine.ShaderParticles.Add(Particle);
                            tendril.Regrow();


                        }

                    }
                }
              

                if(Timer>RipOutTime + 40)
                {
                    this.Exit(boss);
                }
            }



        }

        public override void Exit(MycoBoss_NPC boss)
        {
            if(boss.AttackQueue.Count>1)
            boss.AttackQueue.Dequeue();
            boss.MoveToNextState();
        }
    }
}
