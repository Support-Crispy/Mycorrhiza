using BreadLibrary.Core;
using BreadLibrary.Core.Utilities;
using BreadLibrary.Core.Verlet;
using Mycorrhiza.Content.MycorrhizaBiome.Enemies.Sporewalker; 
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;

namespace Mycorrhiza.Content.MycorrhizaBiome.MycoBoss.Boss
{
    public partial class MycoBoss_NPC : IMultiSegmentNPC
    {

        public static float SeverChance
        {
            get
            {
                if (Main.masterMode)
                {
                    return 0.25f;
                }
                else if (Main.expertMode)
                    return 0.50f;
                else
                    return 0.75f;
            }
        }


        public List<MycoTendril> Tendrils = new();


        public List<ExtraNPCSegment> Segments;
        public ref List<ExtraNPCSegment> ExtraHitBoxes()
        {
            return ref Segments;
        }

        public void PrepareTendrils()
        {
            Tendrils.Clear();
            Segments = new();
            for (int i = 0; i < 6; i++)
            {
                var segment = new ExtraNPCSegment(new Rectangle(0, 0, 40, 40), false, itemCollide: true, projectileCollide: true, uniqueIframes: true, immunity: 30)
                {
                    ImmuneTime = 30
                };
                Tendrils.Add(new MycoTendril(i,count: 20, segmentLength: 10, start: NPC.Center, segment));
                Segments.Add(Tendrils[i].Segment);

           
            }

            foreach (MycoTendril tendril in Tendrils)
            {
                for (int i = 0; i < (tendril.Chain.Positions.Length - 1); i++)
                {
                    tendril.Chain.Positions[i] = Vector2.Lerp(NPC.Center, NPC.Center + new Vector2(0, 200), (float)i / (tendril.Chain.Positions.Length - 1));
                }

                NPC.NewNPCDirect(new EntitySource_Parent(Entity), tendril.Chain.Positions[^1], ModContent.NPCType<DanglingSporeWalker>());
            }


        }

        public void UpdateTendrils()
        {

            int severed = 0;
            int i = 0;

            if (Tendrils is null || Tendrils.Count == 0)
                return;

            int totalTendrils = Tendrils.Count;
            int half = totalTendrils / 2;

            foreach (MycoTendril tendril in Tendrils)
            {
                Vector2 adjust;

                // Determine side and distribution index
                // First `half` tendrils go on the left, the rest on the right.
                bool isLeftSide = i < half;
                int indexInSide = isLeftSide ? i : i - half;
                int totalPerSide = isLeftSide ? half : (totalTendrils - half);

                float span = MathHelper.Pi * 0.7f; Vector2.UnitX.RotatedBy(MathHelper.TwoPi * i / (float)Tendrils.Count); // 90 degrees spread across each side
                float baseAngle = isLeftSide ? MathHelper.Pi : 0f;

                // Avoid division by zero when there's only one tendril on a side
                float t = totalPerSide > 1 ? indexInSide / (float)(totalPerSide - 1) : 0.5f;

                float angle = baseAngle - span / 2f + t * span;

                adjust = Vector2.UnitX.RotatedBy(angle);

                // Simulate chain with computed adjust
                tendril.Chain.Simulate(adjust, NPC.Center, 0.2f, 0.7f, 6, collideWithTiles: false, playerInfluence: 0.01f);

                if (!tendril.Severed)
                {
                    AddWeightToSegment(ref tendril.Chain, tendril.Chain.Positions.Length - 3, 2);

                    AddWeightToSegment(ref tendril.Chain, tendril.Chain.Positions.Length - 2, 3);
                    AddWeightToSegment(ref tendril.Chain, tendril.Chain.Positions.Length - 1, 4);
                }
                else severed++;

                tendril.Segment.Hitbox.Location = (tendril.Chain.Positions[tendril.Chain.Positions.Length / 2] - tendril.Segment.Hitbox.Size() / 2).ToPoint();
                if (tendril.Segment.ImmuneTime > 0)
                {
                    tendril.Segment.ImmuneTime--;
                }
                Segments[i] = tendril.Segment;

                i++;
            }

            if (severed == Tendrils.Count)
            {
                PrepareTendrils();
            }
        }

        private static void AddWeightToSegment(ref VerletChain chain, int index, float weight)
        {
            if (index < -0 || index >= chain.Positions.Length)
                return;

            chain.Positions[index].Y += weight;
        }



        void IMultiSegmentNPC.OnHitBoxCollide(int WhoAmI, Projectile origin)
        {


            if (origin.owner == Main.myPlayer && origin.damage > 0)
            {
                for (int i = 0; i < Segments.Count; i++)
                {
                    if (Segments[i].Hitbox.Intersects(origin.Hitbox))
                    {
                        Segments[i].ImmuneTime = 6000;
                        Segments[i].Active = false;
                        Tendrils[i].Severed = true;
                    }
                }
            }
        }




        public void RenderTendrils(SpriteBatch spriteBatch, Vector2 screenPos, ref Color drawColor)
        {
            if (Tendrils is null)
            {
                return;
            }
            foreach (var tendril in Tendrils)
            {
                tendril.Draw(spriteBatch, screenPos, drawColor, NPC.Center);
            }

        }

#if DEBUG 
        public void RenderHitboxes(SpriteBatch spriteBatch, Vector2 screenPos)
        {
            if (Segments is null)
            {
                return;
            }

            foreach (var a in Segments)
            {
                Utils.DrawRect(spriteBatch, a.Hitbox, Color.White);
            }
        }
#endif
    }

    public class MycoTendril
    {
        public VerletChain Chain;

        public ExtraNPCSegment Segment;

        public bool Severed = false;

        public bool HasNPC = false;

        public int Index;

        #region Constructors
        public MycoTendril(int Index, int count, float segmentLength, Vector2 start, ExtraNPCSegment segment)
        {
            this.Index = Index;
            Chain = new(count, segmentLength, start);
            Segment = segment;
        }
        public MycoTendril(int Index, VerletChain chain, ExtraNPCSegment segment)
        {
            this.Index = Index;
            Chain = chain;
            Segment = segment;
        }

        public MycoTendril(int Index, VerletChain chain, Rectangle Hitbox, bool UniqueIframes, bool DealsDamage = false, int Immunity = 60, bool ItemCollide = true, bool ProjectileCollide = true, int ImmuneTime = 0)
        {
            this.Index = Index;
            Chain = chain;
            Segment = new ExtraNPCSegment(Hitbox, DealsDamage, ItemCollide, ProjectileCollide, UniqueIframes, Immunity)
            {
                ImmuneTime = ImmuneTime
            };
        }
        public MycoTendril(int Index, int count, float segmentLength, Vector2 start, Rectangle Hitbox, bool UniqueIframes, bool DealsDamage = false, int Immunity = 60, bool ItemCollide = true, bool ProjectileCollide = true, int ImmuneTime = 0)
        {
            this.Index = Index;
            Chain = new(count, segmentLength, start);
            Segment = new ExtraNPCSegment(Hitbox, DealsDamage, ItemCollide, ProjectileCollide, UniqueIframes, Immunity)
            {
                ImmuneTime = ImmuneTime
            };
        }
        #endregion


        public void Draw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor, Vector2 OwnerPos)
        {

            int MaxLength = !Severed? Chain.Positions.Length - 1 : Chain.Positions.Length/2;
            for (int i = 0; i < MaxLength; i++)
            {
                float interp = 1 - i / (float)MaxLength;

                Utilities.DrawLineBetter(spriteBatch, Chain.Positions[i], Chain.Positions[i + 1], Color.Lime, 10);

                
            }
        }
    }

    
}
