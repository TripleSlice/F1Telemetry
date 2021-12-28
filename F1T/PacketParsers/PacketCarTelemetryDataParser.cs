using F1T.Structs;
using System;
using System.Runtime.InteropServices;

namespace F1T.PacketParsers
{
    public class PacketCarTelemetryDataParser : BasePacketParser<PacketCarTelemetryData>
    {
        public static PacketCarTelemetryData Parse(ReadOnlySpan<byte> bytes)
        {

            PacketCarTelemetryData packetCarTelemetryData = new PacketCarTelemetryData();

            CarTelemetryData[] carTelemetryData = CarTelemetryDataParser.Parse(bytes);
            bytes = CarTelemetryDataParser.Slice(bytes);

            packetCarTelemetryData.m_carTelemetryData = carTelemetryData;

            packetCarTelemetryData.m_mfdPanelIndex = MemoryMarshal.Cast<byte, byte>(bytes)[0];
            bytes = bytes.Slice(sizeof(byte));

            packetCarTelemetryData.m_mfdPanelIndexSecondaryPlayer = MemoryMarshal.Cast<byte, byte>(bytes)[0];
            bytes = bytes.Slice(sizeof(byte));

            packetCarTelemetryData.m_suggestedGear = MemoryMarshal.Cast<byte, sbyte>(bytes)[0];
            // bytes = bytes.Slice(sizeof(sbyte));

            return packetCarTelemetryData;

        }
    }
}
