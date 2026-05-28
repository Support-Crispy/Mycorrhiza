using Mycorrhiza.Common.Networking;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace Mycorrhiza
{
	// Please read https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Modding-Guide#mod-skeleton-contents for more information about the various files in a mod.
	public partial class Mycorrhiza : Mod
	{
        public override void Load()
        {
            
        }
        public override void Unload()
        {
            PacketLoader.Unload();
        }


        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            NetLinker.HandlePacket(reader, whoAmI);
        }
	}
}
