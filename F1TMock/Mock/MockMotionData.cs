using F1T.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace F1TMock.Mock
{
    class MockMotionData
    {
        private CarMotionData GetRandomCarMotionData()
        {
            CarMotionData carMotionData = new CarMotionData();
            carMotionData.m_worldPositionX = 100.0f;
            carMotionData.m_worldPositionY = 100.0f;
            carMotionData.m_worldPositionZ = 100.0f;
            carMotionData.m_worldVelocityX = 100.0f;
            carMotionData.m_worldVelocityY = 100.0f;
            carMotionData.m_worldVelocityZ = 100.0f;
            carMotionData.m_worldForwardDirX = 1;
            carMotionData.m_worldForwardDirY = 1;
            carMotionData.m_worldForwardDirZ = 1;
            carMotionData.m_worldRightDirX = 1;
            carMotionData.m_worldRightDirY = 1;
            carMotionData.m_worldRightDirZ = 1;
            carMotionData.m_gForceLateral = 1;
            carMotionData.m_gForceLongitudinal = 1;
            carMotionData.m_gForceVertical = 1;
            carMotionData.m_yaw = 100;
            carMotionData.m_pitch = 0;
            carMotionData.m_roll = 0;
            return carMotionData;
        }

        private PacketMotionData GetRandomPacketMotionData()
        {
            PacketMotionData packetMotionData = new PacketMotionData();
            packetMotionData.m_header = MockHeader.GetPacketHeader(PacketType.Motion);
            CarMotionData[] carMotionDatas = new CarMotionData[22];
            for (int i = 0; i < carMotionDatas.Length; i++)
            {
                carMotionDatas[i] = GetRandomCarMotionData();
            }
            packetMotionData.m_carMotionData = carMotionDatas;
            float[] testData = { 1.0f, 1.0f, 1.0f, 1.0f };
            packetMotionData.m_suspensionPosition = testData;
            packetMotionData.m_suspensionVelocity = testData;
            packetMotionData.m_suspensionAcceleration = testData;
            packetMotionData.m_wheelSpeed = testData;
            packetMotionData.m_wheelSlip = testData;
            packetMotionData.m_localVelocityX = 10.0f;
            packetMotionData.m_localVelocityY = 10.0f;
            packetMotionData.m_localVelocityZ = 10.0f;
            packetMotionData.m_angularAccelerationX = 10.0f;
            packetMotionData.m_angularAccelerationY = 10.0f;
            packetMotionData.m_angularAccelerationZ = 10.0f;
            packetMotionData.m_angularVelocityX = 10.0f;
            packetMotionData.m_angularVelocityY = 10.0f;
            packetMotionData.m_angularVelocityZ = 10.0f;
            packetMotionData.m_frontWheelsAngle = 0.5f;
            return packetMotionData;
        }

        public byte[] GetBytesMotionData()
        {
            var packet = GetRandomPacketMotionData();
            int size = Marshal.SizeOf(packet);
            byte[] arr = new byte[size];

            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(packet, ptr, true);
            Marshal.Copy(ptr, arr, 0, size);
            Marshal.FreeHGlobal(ptr);
            return arr;
        }
    }
}
