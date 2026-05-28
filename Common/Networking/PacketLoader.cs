using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mycorrhiza.Common.Networking
{
    public static class PacketLoader
    {
        private static readonly List<NetPacket> Packets = new();
        private static readonly Dictionary<Type, ushort> IDsByType = new();

        public static ushort Register(NetPacket packet)
        {
            ushort id = (ushort)Packets.Count;

            packet.PacketID = (byte)id;

            Packets.Add(packet);
            IDsByType[packet.GetType()] = id;

            return id;
        }

        public static ushort GetID<T>() where T : NetPacket
        {
            return GetID(typeof(T));
        }

        public static ushort GetID(Type type)
        {
            if (!IDsByType.TryGetValue(type, out ushort id))
                throw new Exception($"Packet type {type.FullName} was not registered.");

            return id;
        }

        public static void HandlePacket(BinaryReader reader, int whoAmI)
        {
            ushort packetID = reader.ReadUInt16();

            if (packetID >= Packets.Count)
            {
                ModContent.GetInstance<Mycorrhiza>().Logger.Warn($"Unknown packet ID: {packetID}");
                return;
            }

            NetPacket packetTemplate = Packets[packetID];

            NetPacket packetInstance = CreateFreshInstance(packetTemplate);

            packetInstance.Read(reader);
            packetInstance.Handle(whoAmI);
        }

        private static NetPacket CreateFreshInstance(NetPacket template)
        {
            Type type = template.GetType();

            if (Activator.CreateInstance(type) is not NetPacket packet)
                throw new Exception($"Could not create packet instance for {type.FullName}.");

            packet.PacketID = template.PacketID;
            packet.Mod = template.Mod;

            return packet;
        }

        public static void Unload()
        {
            Packets.Clear();
            IDsByType.Clear();
        }
    }
}
