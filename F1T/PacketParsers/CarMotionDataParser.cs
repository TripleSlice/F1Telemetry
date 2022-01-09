using F1T.Structs;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
namespace F1T.PacketParsers
{
    public class CarMotionDataParser : BasePacketParser<CarMotionData>
    {
        public static CarMotionData[] Parse(ReadOnlySpan<byte> bytes)
        {
            CarMotionData[] data = new CarMotionData[22];
            int carIndex = 0;
            // we expect 22*CarMotionData, then the remaining data
            foreach (var telemetry in MemoryMarshal.Cast<byte, CarMotionData>(bytes).Slice(0, 22))
            {
                data[carIndex] = telemetry;
                carIndex++;
            }

            return data;
        }

        public static new ReadOnlySpan<byte> Slice(ReadOnlySpan<byte> bytes)
        {
            bytes = bytes.Slice(22 * Unsafe.SizeOf<CarMotionData>());
            return bytes;
        }
    }
}
