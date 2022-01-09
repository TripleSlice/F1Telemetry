using F1T.Structs;
using System;
using System.Runtime.InteropServices;

namespace F1T.PacketParsers
{
    public class PacketMotionDataParser : BasePacketParser<PacketMotionData>
    {
        public static PacketMotionData Parse(ReadOnlySpan<byte> bytes)
        {

            PacketMotionData data = new PacketMotionData();

            CarMotionData[] carMotionData = CarMotionDataParser.Parse(bytes);
            bytes = CarMotionDataParser.Slice(bytes);
            data.m_carMotionData = carMotionData;

            data.m_suspensionPosition = MemoryMarshal.Cast<byte, FloatQuad>(bytes)[0];
            bytes = bytes.Slice(16);

            data.m_suspensionVelocity = MemoryMarshal.Cast<byte, FloatQuad>(bytes)[0];
            bytes = bytes.Slice(16);

            data.m_suspensionPosition = MemoryMarshal.Cast<byte, FloatQuad>(bytes)[0];
            bytes = bytes.Slice(16);

            data.m_wheelSpeed = MemoryMarshal.Cast<byte, FloatQuad>(bytes)[0];
            bytes = bytes.Slice(16);

            data.m_wheelSlip = MemoryMarshal.Cast<byte, FloatQuad>(bytes)[0];
            bytes = bytes.Slice(16);

            data.m_localVelocityX = MemoryMarshal.Cast<byte, float>(bytes)[0];
            bytes = bytes.Slice(sizeof(float));

            data.m_localVelocityY = MemoryMarshal.Cast<byte, float>(bytes)[0];
            bytes = bytes.Slice(sizeof(float));

            data.m_localVelocityZ = MemoryMarshal.Cast<byte, float>(bytes)[0];
            bytes = bytes.Slice(sizeof(float));

            data.m_angularVelocityX = MemoryMarshal.Cast<byte, float>(bytes)[0];
            bytes = bytes.Slice(sizeof(float));

            data.m_angularVelocityY = MemoryMarshal.Cast<byte, float>(bytes)[0];
            bytes = bytes.Slice(sizeof(float));

            data.m_angularVelocityZ = MemoryMarshal.Cast<byte, float>(bytes)[0];
            bytes = bytes.Slice(sizeof(float));

            data.m_angularAccelerationX = MemoryMarshal.Cast<byte, float>(bytes)[0];
            bytes = bytes.Slice(sizeof(float));

            data.m_angularAccelerationY = MemoryMarshal.Cast<byte, float>(bytes)[0];
            bytes = bytes.Slice(sizeof(float));

            data.m_angularAccelerationZ = MemoryMarshal.Cast<byte, float>(bytes)[0];
            bytes = bytes.Slice(sizeof(float));

            data.m_frontWheelsAngle = MemoryMarshal.Cast<byte, float>(bytes)[0];
            //bytes = bytes.Slice(sizeof(float));

            return data;
        }
    }
}
