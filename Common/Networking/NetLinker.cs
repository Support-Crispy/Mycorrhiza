using Mycorrhiza.Common.Networking;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mycorrhiza.Common.Networking;

public static class NetLinker
{
    public static void HandlePacket(BinaryReader reader, int whoAmI)
    {
        PacketLoader.HandlePacket(reader, whoAmI);
    }
    
    
}
