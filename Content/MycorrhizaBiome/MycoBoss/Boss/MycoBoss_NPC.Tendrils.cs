using BreadLibrary.Core;
using BreadLibrary.Core.Utilities;
using BreadLibrary.Core.Verlet;
using Mycorrhiza.Content.MycorrhizaBiome.Enemies.Sporewalker; 
using Terraria;
using Terraria.GameContent;

namespace Mycorrhiza.Content.MycorrhizaBiome.MycoBoss.Boss
{
    public partial class MycoBoss_NPC :IMultiSegmentNPC
    {
        public List<MycoTendril> Tendrils = new();


        public List<ExtraNPCSegment> Segments;
        public ref List<ExtraNPCSegment> ExtraHitBoxes()
        {
            return ref Segments;
        }

        public void PrepareTendrils()
        {
            Tendrils.Clear();
            for (int i = 0; i < 5; i++)
            {
                Tendrils.Add(new MycoTendril(count: 20, segmentLength: 10, start: NPC.Center);
            }


            for (int x = 0; x < Tendrils.Count; x++)
            {
                for(int i = 0; i<  (Tendrils[x].Chain.Positions.Length-1); i++)
                {
                    Tendrils[x].Chain.Positions[i] = Vector2.Lerp(NPC.Center, NPC.Center + new Vector2(0, 200), (float)i / (Tendrils[x].Chain.Positions.Length - 1));
                }
            }
        }

        public void UpdateTendrils()
        {

            int i = 0;
            foreach (MycoTendril tendril in Tendrils)
            {
                Vector2 adjust = Vector2.UnitX.RotatedBy(MathHelper.TwoPi * i / (float)Tendrils.Count);
                tendril.Chain.Simulate(adjust, NPC.Center, 0.2f, 0.7f, collideWithTiles: false, playerInfluence: 0.01f);
                i++;
            }
        }
    }

    public class MycoTendril
    {
        public VerletChain Chain;

        public ExtraNPCSegment Segment;

        public bool Severed = false;

        public MycoTendril(int count, float segmentLength, Vector2 start)
        {
            Chain = new(count, segmentLength, start);
            
        }
        public MycoTendril(VerletChain chain)
        {
            Chain = chain;
        }


        public void Draw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor, Vector2 OwnerPos)
        {

            for (int i = 0; i < Chain.Positions.Length - 1; i++)
            {
                float interp = 1 - i / (float)(Chain.Positions.Length - 1);

                Utilities.DrawLineBetter(spriteBatch, Chain.Positions[i], Chain.Positions[i + 1], Color.White, 20 * interp);

                if (i == Chain.Positions.Length - 2)
                {
                    Vector2 Pos = Chain.Positions[i + 1];
                    var tex = TextureAssets.Npc[ModContent.NPCType<Sporewalker>()].Value;
                    var frame = tex.Frame(1, 10, 0, 0);
                    Vector2 Origin = frame.Size() / 2 + new Vector2(0, -10);

                    SpriteEffects effect = OwnerPos.DirectionFrom(Pos).X.NonZeroSign().ToSpriteDirection();
                    Main.EntitySpriteDraw(tex, Pos - screenPos, frame, Color.White, 0, Origin, 1, effect);
                }
            }
        }
    }
}
