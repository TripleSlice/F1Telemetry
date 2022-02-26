using F1T.Structs;
using F1TMock.RandomUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace F1TMock.Mock
{
    class MockPacketMotionData
    {
        public static Vector3 carWorldPositionXVector = new Vector3(RandomGenerator.GenerateRandomInt(0, 1000), RandomGenerator.GenerateRandomInt(0, 1000), RandomGenerator.GenerateRandomInt(0, 1000));
        public static Vector3 carWorldPositionZVector = new Vector3(RandomGenerator.GenerateRandomInt(0, 1000), RandomGenerator.GenerateRandomInt(0, 1000), RandomGenerator.GenerateRandomInt(0, 1000));
        public static Vector3 carYawVector = new Vector3(RandomGenerator.GenerateRandomInt(0, 1000), RandomGenerator.GenerateRandomInt(0, 1000), RandomGenerator.GenerateRandomInt(0, 1000));

        private static void IncrementRandoms()
        {

            VectorUtil.Increment(ref carWorldPositionXVector, 0.01f, 0.02f, 0.01f);
            VectorUtil.Increment(ref carWorldPositionZVector, 0.01f, 0.02f, 0.01f);
            VectorUtil.Increment(ref carYawVector, 0.001f, 0.002f, 0.001f);

        }


        private static CarMotionData GetDummyCarMotionData()
        {
            CarMotionData carMotionData = new CarMotionData();
            carMotionData.m_worldPositionX = 100f;
            carMotionData.m_worldPositionY = 100f;
            carMotionData.m_worldPositionZ = 100f;
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
            carMotionData.m_yaw = 0;
            carMotionData.m_pitch = 0;
            carMotionData.m_roll = 0;
            return carMotionData;
        }
        private static CarMotionData GetPlayerCarMotionData()
        {
            CarMotionData carMotionData = new CarMotionData();
            carMotionData.m_worldPositionX = 0f;
            carMotionData.m_worldPositionY = 0f;
            carMotionData.m_worldPositionZ = 0f;
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
            carMotionData.m_yaw = 0;
            carMotionData.m_pitch = 0;
            carMotionData.m_roll = 0;
            return carMotionData;
        }
        private static CarMotionData GetRandomCarMotionData()
        {

            IncrementRandoms();

            CarMotionData carMotionData = new CarMotionData();
            carMotionData.m_worldPositionX = Perlin.Clamp(Perlin.Noise(carWorldPositionXVector), -20.0f, 20f);
            carMotionData.m_worldPositionY = 0f;
            carMotionData.m_worldPositionZ = Perlin.Clamp(Perlin.Noise(carWorldPositionZVector), -20.0f, 20f);
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
            carMotionData.m_yaw = Perlin.Clamp(Perlin.Noise(carWorldPositionZVector), -1f, 1f);
            carMotionData.m_pitch = 0;
            carMotionData.m_roll = 0;

            return carMotionData;
        }

        private static PacketMotionData GetRandomPacketMotionData()
        {
            PacketMotionData packetMotionData = new PacketMotionData();
            packetMotionData.m_header = MockPacketHeader.GetBytes(PacketType.Motion);
            CarMotionData[] carMotionDatas = new CarMotionData[22];
            for (int i = 0; i < carMotionDatas.Length; i++)
            {
                carMotionDatas[i] = GetDummyCarMotionData();
            }
            carMotionDatas[packetMotionData.m_header.m_playerCarIndex] = GetPlayerCarMotionData();

            carMotionDatas[8] = GetRandomCarMotionData();

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

        public static byte[] GetBytes()
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
