using System;
using System.Runtime.CompilerServices;

namespace F1T.PacketParsers
{
    public abstract class BasePacketParser<T>
    {
        public static ReadOnlySpan<byte> Slice(ReadOnlySpan<byte> bytes)
        {
            bytes = bytes.Slice(Unsafe.SizeOf<T>());
            return bytes;
        }
    }
}
