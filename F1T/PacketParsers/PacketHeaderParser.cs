using F1T.Structs;
using System;
using System.Runtime.InteropServices;

namespace F1T.PacketParsers
{
    public class PacketHeaderParser: BasePacketParser<PacketHeader>
    {
        public static PacketHeader Parse(ReadOnlySpan<byte> bytes)
        {
            PacketHeader packetheader = MemoryMarshal.Cast<byte, PacketHeader>(bytes)[0];
            return packetheader;
        }
    }
}
