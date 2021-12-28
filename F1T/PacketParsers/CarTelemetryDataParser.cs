using F1T.Structs;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace F1T.PacketParsers
{
    public class CarTelemetryDataParser : BasePacketParser<CarTelemetryData>
    {
        public static CarTelemetryData[] Parse(ReadOnlySpan<byte> bytes)
        {
            CarTelemetryData[] packetCarTelementryDatas = new CarTelemetryData[22];
            int carIndex = 0;
            // we expect 22*CarTelemetryData, then the remaining data
            foreach (var telemetry in MemoryMarshal.Cast<byte, CarTelemetryData>(bytes).Slice(0, 22))
            {
                packetCarTelementryDatas[carIndex] = telemetry;
                carIndex++;
            }

            return packetCarTelementryDatas;
        }

        public static new ReadOnlySpan<byte> Slice(ReadOnlySpan<byte> bytes)
        {
            bytes = bytes.Slice(22 * Unsafe.SizeOf<CarTelemetryData>());
            return bytes;
        }
    }
}
