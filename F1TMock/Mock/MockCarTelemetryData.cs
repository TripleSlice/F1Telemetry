using F1T.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace F1TMock.Mock
{
    class MockCarTelemetryData
    {
        private CarTelemetryData GetRandomCarTelemetryData()
        {
            CarTelemetryData data = new CarTelemetryData();
            data.m_speed = 100;
            data.m_throttle = 0.2f;
            data.m_steer = 0;
            data.m_brake = 0;
            data.m_clutch = 0;
            data.m_gear = 2;
            data.m_engineRPM = 4000;
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

        private PacketCarTelemetryData GetRandomPacketCarTelemetryData()
        {
            PacketCarTelemetryData data = new PacketCarTelemetryData();
            data.m_header = MockHeader.GetPacketHeader(PacketType.CarTelemetry);
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

        public byte[] GetBytesCarTelemetryData()
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
