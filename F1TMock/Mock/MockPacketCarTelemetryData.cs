using F1T.Structs;
using F1TMock.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace F1TMock.Mock
{
    public static class MockPacketCarTelemetryData
    {
        private static CarTelemetryData GetRandomCarTelemetryData()
        {
            CarTelemetryData data = new CarTelemetryData();
            data.m_speed = (ushort)PerlinGenerator.NoiseInRange("m_speed", 0, 300, Intensity.Low);
            data.m_throttle = PerlinGenerator.NoiseInRange("m_throttle", 0f, 1f, Intensity.Low);
            data.m_steer = PerlinGenerator.FloatNoise("m_steer", Intensity.Low);
            data.m_brake = PerlinGenerator.NoiseInRange("m_brake", 0f, 1f, Intensity.Low);
            data.m_clutch = (byte)PerlinGenerator.NoiseInRange("m_clutch", 0, 100, Intensity.Low);
            data.m_gear = (sbyte)PerlinGenerator.NoiseInRange("m_gear", 0, 8, Intensity.Low);
            data.m_engineRPM = (ushort)PerlinGenerator.NoiseInRange("m_engineRPM", 0, 12000, Intensity.Low); ;
            data.m_drs = (DRS)PerlinGenerator.NoiseInRange("m_drs", 0, 1, Intensity.Low);
            data.m_revLightsPercent = (byte)PerlinGenerator.NoiseInRange("m_revLightsPercent", 0, 100, Intensity.Low);
            data.m_revLightsBitValue = (ushort)PerlinGenerator.NoiseInRange("m_revLightsBitValue", 0, 14, Intensity.Low);
            data.m_brakesTemperature = PerlinGenerator.GenerateUShortArray("m_brakesTemperature", 100, 200, 4, Intensity.Low);
            data.m_tyresSurfaceTemperature = PerlinGenerator.GenerateByteArray("m_tyresSurfaceTemperature", 100, 200, 4, Intensity.Low);
            data.m_tyresInnerTemperature = PerlinGenerator.GenerateByteArray("m_tyresInnerTemperature", 100, 200, 4, Intensity.Low);
            data.m_engineTemperature = (ushort)PerlinGenerator.NoiseInRange("m_engineTemperature", 0, 100, Intensity.Low);
            data.m_tyresPressure = PerlinGenerator.GenerateFloatArray("m_tyresPressure", 0, 30, 4, Intensity.Low);
            data.m_surfaceType = new SurfaceType[] {SurfaceType.Tarmac, SurfaceType.Tarmac, SurfaceType.Tarmac, SurfaceType.Tarmac };

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
