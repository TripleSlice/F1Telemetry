using F1T.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace F1TMock.Mock
{
    class MockCarStatusData
    {
        private CarStatusData GetRandomCarStatusData()
        {
            CarStatusData data = new CarStatusData();
            data.m_tractionControl = TractionControl.Off;
            data.m_antiLockBrakes = AntiLockBrakes.On;
            data.m_fuelMix = FuelMix.Rich;
            data.m_frontBrakeBias = 50;
            data.m_pitLimiterStatus = PitLimiterStatus.On;
            data.m_fuelInTank = 20.0f;
            data.m_fuelCapacity = 40.0f;
            data.m_fuelRemainingLaps = 10.0f;
            data.m_maxRPM = 12000;
            data.m_idleRPM = 2000;
            data.m_maxGears = 8;
            data.m_drsAllowed = 0;
            data.m_drsActivationDistance = 0;
            data.m_actualTyreCompound = ActualTyreCompund.C4;
            data.m_visualTyreCompound = VisualTyreCompound.Medium;
            data.m_tyresAgeLaps = 2;
            data.m_vehicleFiaFlags = VehicleFiaFlags.None;
            data.m_ersStoreEnergy = 10.0f;
            data.m_ersDeployMode = ERSDeployMode.None;
            data.m_ersHarvestedThisLapMGUK = 1.0f;
            data.m_ersHarvestedThisLapMGUH = 1.0f;
            data.m_ersDeployedThisLap = 0.0f;
            data.m_networkPaused = 0;
            return data;
        }

        private PacketCarStatusData GetRandomPacketCarStatusData()
        {
            PacketCarStatusData data = new PacketCarStatusData();
            data.m_header = MockHeader.GetPacketHeader(PacketType.CarStatus);
            CarStatusData[] cars = new CarStatusData[22];
            for (int i = 0; i < cars.Length; i++)
            {
                cars[i] = GetRandomCarStatusData();
            }
            data.m_carStatusData = cars;
            return data;
        }

        public byte[] GetBytesCarStatusData()
        {
            var packet = GetRandomPacketCarStatusData();
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
