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
    class MockPacketCarTelemetryData
    {

        public static Vector3 throttleVector = new Vector3(RandomGenerator.GenerateRandomInt(0, 1000), RandomGenerator.GenerateRandomInt(0, 1000), RandomGenerator.GenerateRandomInt(0, 1000));
        public static Vector3 brakeVector = new Vector3(RandomGenerator.GenerateRandomInt(0, 1000), RandomGenerator.GenerateRandomInt(0, 1000), RandomGenerator.GenerateRandomInt(0, 1000));
        public static Vector3 steerVector = new Vector3(RandomGenerator.GenerateRandomInt(0, 1000), RandomGenerator.GenerateRandomInt(0, 1000), RandomGenerator.GenerateRandomInt(0, 1000));
        public static Vector3 gearVector = new Vector3(RandomGenerator.GenerateRandomInt(0, 1000), RandomGenerator.GenerateRandomInt(0, 1000), RandomGenerator.GenerateRandomInt(0, 1000));
        public static Vector3 rpmVector = new Vector3(RandomGenerator.GenerateRandomInt(0, 1000), RandomGenerator.GenerateRandomInt(0, 1000), RandomGenerator.GenerateRandomInt(0, 1000));

        private static void IncrementRandoms()
        {
            VectorUtil.Increment(ref throttleVector, 0.001f, 0.002f, 0.001f);
            VectorUtil.Increment(ref brakeVector, 0.001f, 0.002f, 0.001f);
            VectorUtil.Increment(ref steerVector, 0.001f, 0.002f, 0.001f);
            VectorUtil.Increment(ref gearVector, 0.001f, 0.002f, 0.001f);
            VectorUtil.Increment(ref rpmVector, 0.001f, 0.002f, 0.001f);
        }

        private static CarTelemetryData GetRandomCarTelemetryData()
        {
            IncrementRandoms();

            CarTelemetryData data = new CarTelemetryData();
            data.m_speed = (ushort)Perlin.Clamp(Perlin.Noise(rpmVector), 0, 300);
            data.m_throttle = Perlin.Clamp(Perlin.Noise(throttleVector), 0f, 1f);
            data.m_steer = Perlin.Noise(steerVector);
            data.m_brake = Perlin.Clamp(Perlin.Noise(brakeVector), 0f, 1f);
            data.m_clutch = 0;
            data.m_gear = (sbyte)Perlin.Clamp(Perlin.Noise(gearVector), 0, 8);
            data.m_engineRPM = (ushort)Perlin.Clamp(Perlin.Noise(rpmVector), 0, 12000);
            data.m_drs = DRS.Off;
            data.m_revLightsPercent = 20;
            data.m_revLightsBitValue = 4;
            ushort[] testData = { 200, 200, 200, 200 };
            data.m_brakesTemperature = testData;
            byte[] testData2 = { 200, 200, 200, 200 };
            data.m_tyresSurfaceTemperature = testData2;
            data.m_tyresInnerTemperature = testData2;
            data.m_engineTemperature = 200;
            float[] testData3 = { 20.0f, 20.0f, 20.0f, 20.0f };
            data.m_tyresPressure = testData3;
            byte[] testData4 = { 1, 1, 1, 1 };
            data.m_surfaceType = testData4;
            return data;
        }

        private static PacketCarTelemetryData GetRandomPacketCarTelemetryData()
        {
            PacketCarTelemetryData data = new PacketCarTelemetryData();
            data.m_header = MockPacketHeader.GetBytes(PacketType.CarTelemetry);
            CarTelemetryData[] cars = new CarTelemetryData[22];
            for (int i = 0; i < cars.Length; i++)
            {
                cars[i] = GetRandomCarTelemetryData();
            }
            data.m_carTelemetryData = cars;
            data.m_mfdPanelIndex = MFDPanelIndex.Closed;
            data.m_mfdPanelIndexSecondaryPlayer = 255;
            data.m_suggestedGear = 0;
            return data;
        }

        public static byte[] GetBytes()
        {
            var packet = GetRandomPacketCarTelemetryData();
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
