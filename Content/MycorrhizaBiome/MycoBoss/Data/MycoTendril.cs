using BreadLibrary.Core.Verlet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mycorrhiza.Content.MycorrhizaBiome.MycoBoss.Data
{
    internal class MycoTendril
    {
        public VerletChain Chain;

        public int CorpseTextureIndex;


        public MycoTendril(int count, float segmentLength, Vector2 start, int corpseTextureIndex)
        {
            Chain = new(count, segmentLength, start);
            CorpseTextureIndex = corpseTextureIndex;
        }
        public MycoTendril(VerletChain chain, int corpseTextureIndex)
        {
            Chain = chain;
            CorpseTextureIndex = corpseTextureIndex;
        }
    }
}
