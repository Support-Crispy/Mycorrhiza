using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace Mycorrhiza.Common.Networking
{
    public abstract class NetPacket : ModType
    {
        protected sealed override void Register()
        {
            PacketLoader.Register(this);
        }
        public abstract byte PacketID { get; internal set; }

        public virtual void Write(BinaryWriter writer) { }

        public virtual void Read(BinaryReader reader) { }

        public abstract void Handle(int whoAmI);

        public void Send(int toClient = -1, int ignoreClient = -1)
        {
            if (Main.netMode == Terraria.ID.NetmodeID.SinglePlayer)
                return;

            ModPacket packet = ModContent.GetInstance<Mycorrhiza>().GetPacket();

            packet.Write(PacketID);
            Write(packet);

            packet.Send(toClient, ignoreClient);
        }
    }
}
