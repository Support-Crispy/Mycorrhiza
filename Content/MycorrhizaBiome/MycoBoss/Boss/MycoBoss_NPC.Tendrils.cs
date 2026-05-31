using BreadLibrary.Core;
using BreadLibrary.Core.Graphics.Spritebatch;
using BreadLibrary.Core.Utilities;
using BreadLibrary.Core.Verlet;
using Mycorrhiza.Content.MycorrhizaBiome.MycoBoss.Boss.Attacks;
using Mycorrhiza.Content.MycorrhizaBiome.MycoBoss.Boss.Attacks.Mycorrhiza.Content.MycorrhizaBiome.MycoBoss.Boss.Attacks;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;

namespace Mycorrhiza.Content.MycorrhizaBiome.MycoBoss.Boss
{
    public partial class MycoBoss_NPC : IMultiSegmentNPC
    {
        private static int _Dangling
        {
            get => ModContent.NPCType<DanglingSporeWalker>();
        }

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

        public Dictionary<int, int> DanglingSporewalkers = new();

        public List<MycoTendril> Tendrils = new();


        public List<ExtraNPCSegment> Segments;
        public ref List<ExtraNPCSegment> ExtraHitBoxes()
        {
            return ref Segments;
        }


        public static int MaxTendrils
        {
            get
            {
                if (Main.masterMode)
                {
                    return 12;
                }
                else if (Main.expertMode)
                    return 8;
                else
                    return 6;

            }
        }
        public void PrepareTendrils()
        {
            Tendrils.Clear();
            DanglingSporewalkers = new();
            Segments = new();
            for (int i = 0; i < MaxTendrils; i++)
            {
                var segment = new ExtraNPCSegment(new Rectangle(0, 0, 40, 40), false, itemCollide: true, projectileCollide: true, uniqueIframes: true, immunity: 30)
                {
                    ImmuneTime = 30
                };
                Tendrils.Add(new MycoTendril(this, i, count: 26, segmentLength: 10, start: NPC.Center, segment));
                Segments.Add(Tendrils[i].Segment);


            }
            int x = 0;
            foreach (MycoTendril tendril in Tendrils)
            {
                for (int i = 0; i < (tendril.Chain.Positions.Length - 1); i++)
                {
                    tendril.Chain.Positions[i] = Vector2.Lerp(NPC.Center, NPC.Center + new Vector2(0, 200), (float)i / (tendril.Chain.Positions.Length - 1));
                }

                var a = NPC.NewNPCDirect(new EntitySource_Parent(Entity), tendril.Chain.Positions[^1], ModContent.NPCType<DanglingSporeWalker>());

                DanglingSporewalkers.Add(x, a.whoAmI);
                    x++;
            }


        }
        //todo: let _MycoBossAttacks override this behavior if necessary
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
                {
                    Vector2 adjust;

                    // Determine side and distribution index
                    // First `half` tendrils go on the left, the rest on the right.
                    bool isLeftSide = i < half;
                    int indexInSide = isLeftSide ? i : i - half;
                    int totalPerSide = isLeftSide ? half : (totalTendrils - half);

                    float span = MathHelper.Pi * 0.2f; Vector2.UnitX.RotatedBy(MathHelper.TwoPi * i / (float)Tendrils.Count); // 90 degrees spread across each side
                    float baseAngle = isLeftSide ? MathHelper.Pi : 0f;

                    // Avoid division by zero when there's only one tendril on a side
                    float t = totalPerSide > 1 ? indexInSide / (float)(totalPerSide - 1) : 0.5f;

                    float angle = baseAngle - span / 2f + t * span;

                    adjust = Vector2.UnitX.RotatedBy(angle);

                    // Simulate chain with computed adjust
                    tendril.Chain.Simulate(adjust, NPC.Center, -0.2f, 0.9f, 6, collideWithTiles: false, collideWithPlayers: false);


                    for(int x = 0; x< tendril.Chain.Positions.Length-3; x++)
                    {
                        tendril.Chain.OldPositions[x] += Vector2.UnitY * MathF.Sin(x + Main.GameUpdateCount * 0.1f)*4;
                    }
                }

             




                if (!tendril.Severed)
                {
                    NPC nPC = Main.npc[DanglingSporewalkers[i]];
                    nPC.Center = tendril.Chain.Positions[^1];
                    nPC.As<DanglingSporeWalker>().TentacleID = i;
                    AddWeightToSegment(ref tendril.Chain, tendril.Chain.Positions.Length - 3, 1);

                    AddWeightToSegment(ref tendril.Chain, tendril.Chain.Positions.Length - 2, 2);
                    AddWeightToSegment(ref tendril.Chain, tendril.Chain.Positions.Length - 1, 4);
                }
                else
                {
                    tendril.Segment.Active = false;
                    severed++;
                }
                tendril.Segment.Hitbox.Location = (tendril.Chain.Positions[tendril.Chain.Positions.Length / 2] - tendril.Segment.Hitbox.Size() / 2).ToPoint();

                Segments[i] = tendril.Segment;

                i++;
            }

            if (severed == Tendrils.Count)
            {
                var a = _MycoBossAttackRegistry.Create(MycoBoss_State.Replenish_Sporewalkers);
                if (!AttackQueue.Contains(MycoBoss_State.Replenish_Sporewalkers))
                    AttackQueue.Enqueue(MycoBoss_State.Replenish_Sporewalkers);
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
                    if (Segments[i].Active)
                    if (Segments[i].Hitbox.Intersects(origin.Hitbox) && Main.rand.NextFloat(0, 1) < SeverChance)
                    {
                        Segments[i].ImmuneTime = 6000;
                        Segments[i].Active = false;

                        Tendrils[i].Sever();
                        break;
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
                if (a.Active)
                    Utils.DrawRect(spriteBatch, a.Hitbox, Color.Green);
                else
                    Utils.DrawRect(spriteBatch, a.Hitbox, Color.Red * 0.5f);
            }
        }
#endif


        public static BasicEffect TendrilBasicEffect;

        public BasicEffect GetTendrilEffect()
        {
            if (TendrilBasicEffect is null || TendrilBasicEffect.IsDisposed)
            {
                TendrilBasicEffect = new BasicEffect(Main.instance.GraphicsDevice)
                {
                    VertexColorEnabled = true,
                    TextureEnabled = false,
                    LightingEnabled = false,
                    FogEnabled = false
                };
            }

            TendrilBasicEffect.World = Matrix.Identity;
            TendrilBasicEffect.View = Main.GameViewMatrix.TransformationMatrix;

            // 2D screen-space projection.
            TendrilBasicEffect.Projection = Matrix.CreateOrthographicOffCenter(
                0,
                Main.screenWidth,
                Main.screenHeight,
                0,
                0,
                1
            );

            return TendrilBasicEffect;
        }

    }

    public class MycoTendril
    {
        public Action? OnSever;
        public MycoBoss_NPC Owner;
        public VerletChain Chain;

        public ExtraNPCSegment Segment;

        public bool Severed = false;

        public int Index;

        public float VisibleLengthInterpolant = 1f;
        public float SeverShrinkSpeed = 0.18f;

        private DanglingSporeWalker dangling
        {
            get
            {
                if (Owner.DanglingSporewalkers is null)
                    return null;


                int index = Owner.DanglingSporewalkers[Index];

                NPC npc = Main.npc[index];
                if (npc is not null && npc.active && npc.type == ModContent.NPCType<DanglingSporeWalker>())
                {
                    return npc.As<DanglingSporeWalker>();
                }
                return null;
            }
        }


        public void Sever()
        {
            if (Severed)
                return;

            OnSever?.Invoke();

            Severed = true;

            Segment.Active = false;

            Owner.DanglingSporewalkers.Remove(Index);
        }

        public void Regrow()
        {
            Severed = false;
            Segment.Active = true;

            VisibleLengthInterpolant = 1f;
            if (!Owner.DanglingSporewalkers.ContainsKey(Index))
            {
                var a = NPC.NewNPCDirect(new EntitySource_Parent(Owner.Entity), Chain.Positions[^1], ModContent.NPCType<DanglingSporeWalker>());

                Owner.DanglingSporewalkers.Add(Index, a.whoAmI);
            }
          
            //todo: send packet
        }

        private void TryDropSporewalker()
        {
            SoundStyle sever = Assets.Sounds.Enemies.TendrilSever.Asset;
            SoundEngine.PlaySound(sever with { PitchVariance = 0.2f, Type = SoundType.Sound, MaxInstances = 0}, Chain.Positions[^1]);
            dangling?.Drop();
            //todo: send packet

        }
        #region Constructors
        public MycoTendril(MycoBoss_NPC Owner, int Index, int count, float segmentLength, Vector2 start, ExtraNPCSegment segment)
        {
            this.Owner = Owner;
            this.Index = Index;
            Chain = new(count, segmentLength, start);
            Segment = segment;
            this.OnSever = () =>
            {
                TryDropSporewalker();
            };
        }
        public MycoTendril(MycoBoss_NPC Owner, int Index, VerletChain chain, ExtraNPCSegment segment)
        {
            this.Owner = Owner;
            this.Index = Index;
            Chain = chain;
            Segment = segment;
            this.OnSever = () =>
            {
                TryDropSporewalker();
            };
        }

        public MycoTendril(MycoBoss_NPC Owner, int Index, VerletChain chain, Rectangle Hitbox, bool UniqueIframes, bool DealsDamage = false, int Immunity = 60, bool ItemCollide = true, bool ProjectileCollide = true, int ImmuneTime = 0)
        {
            this.Owner = Owner;
            this.Index = Index;
            Chain = chain;
            Segment = new ExtraNPCSegment(Hitbox, DealsDamage, ItemCollide, ProjectileCollide, UniqueIframes, Immunity)
            {
                ImmuneTime = ImmuneTime
            };
            this.OnSever = () =>
            {
                TryDropSporewalker();
            };
        }
        public MycoTendril(MycoBoss_NPC Owner, int Index, int count, float segmentLength, Vector2 start, Rectangle Hitbox, bool UniqueIframes, bool DealsDamage = false, int Immunity = 60, bool ItemCollide = true, bool ProjectileCollide = true, int ImmuneTime = 0)
        {
            this.Owner = Owner;
            this.Index = Index;
            Chain = new(count, segmentLength, start);
            Segment = new ExtraNPCSegment(Hitbox, DealsDamage, ItemCollide, ProjectileCollide, UniqueIframes, Immunity)
            {
                ImmuneTime = ImmuneTime
            };

            this.OnSever = () =>
            {
                TryDropSporewalker();
            };
        }


      
        #endregion


        public void Draw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor, Vector2 OwnerPos)
        {
            int maxLength = (int)((Chain.Positions.Length - 1) * VisibleLengthInterpolant);

            if (maxLength < 1)
                return;
            if (maxLength < 1)
                return;

            if (Main.netMode == NetmodeID.Server)
                return;

            GraphicsDevice graphicsDevice = Main.instance.GraphicsDevice;

            VertexPositionColorTexture[] vertices = BuildTendrilStripVertices(
                screenPos,
                maxLength,
                baseWidth: 10f,
                color: Color.Lime
            );

            if (vertices.Length < 4)
                return;

            var cap = spriteBatch.Capture();
            BasicEffect effect = Owner.GetTendrilEffect();

            graphicsDevice.BlendState = BlendState.AlphaBlend;
            graphicsDevice.DepthStencilState = DepthStencilState.None;
            graphicsDevice.RasterizerState = RasterizerState.CullNone;
            graphicsDevice.SamplerStates[0] = SamplerState.LinearClamp;

            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                graphicsDevice.DrawUserPrimitives(
                    PrimitiveType.TriangleStrip,
                    vertices,
                    0,
                    vertices.Length - 2
                );
            }


          


        }


        private VertexPositionColorTexture[] BuildTendrilStripVertices(
        Vector2 screenPos,
        int maxLength,
        float baseWidth,
        Color color)
        { 
            int pointCount = maxLength + 1;

            VertexPositionColorTexture[] vertices = new VertexPositionColorTexture[pointCount * 2];

            for (int i = 0; i < pointCount; i++)
            {
                Vector2 current = Chain.Positions[i];

                Vector2 previous = i > 0
                    ? Chain.Positions[i - 1]
                    : Chain.Positions[i];

                Vector2 next = i < pointCount - 1
                    ? Chain.Positions[i + 1]
                    : Chain.Positions[i];

                Vector2 tangent = next - previous;

                if (tangent.LengthSquared() <= 0.001f)
                    tangent = Vector2.UnitX;

                tangent.Normalize();

                Vector2 normal = new Vector2(-tangent.Y, tangent.X);

                float progress = i / (float)(pointCount - 1);
                float taper = 1f - progress;

                float width = MathHelper.Lerp(baseWidth * 0.35f, baseWidth, taper);

                Vector2 left = current + normal * width * 0.5f;
                Vector2 right = current - normal * width * 0.5f;

                Vector2 leftScreen = left - screenPos;
                Vector2 rightScreen = right - screenPos;

                float texV = progress;

                vertices[i * 2] = new VertexPositionColorTexture(
                    new Vector3(leftScreen, 0f),
                    color,
                    new Vector2(0f, texV)
                );

                vertices[i * 2 + 1] = new VertexPositionColorTexture(
                    new Vector3(rightScreen, 0f),
                    color,
                    new Vector2(1f, texV)
                );
            }

            return vertices;
        }
    }


}
