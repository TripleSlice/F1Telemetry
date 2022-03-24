using F1T.Structs;
using F1TMock.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace F1TMock.Mock
{
    public static class MockPacketCarStatusData
    {
        private static CarStatusData GetRandomCarStatusData(int index)
        {
            CarStatusData data = new CarStatusData();
            data.m_tractionControl = (TractionControl)PerlinGenerator.NoiseInRange("m_tractionControl" + index, 0, 2, Intensity.Low);
            data.m_antiLockBrakes = (AntiLockBrakes)PerlinGenerator.NoiseInRange("m_antiLockBrakes" + index, 0, 1, Intensity.Low);
            data.m_fuelMix = (FuelMix)PerlinGenerator.NoiseInRange("m_fuelMix" + index, 0, 3, Intensity.Low);
            data.m_frontBrakeBias = (byte)IncrementalGenerator.GetIntNumber("m_frontBrakeBias" + index, 0, 100, 1);
            data.m_pitLimiterStatus = (PitLimiterStatus)PerlinGenerator.NoiseInRange("m_pitLimiterStatus" + index, 0, 1, Intensity.Low);
            data.m_fuelInTank = IncrementalGenerator.GetFloatNumber("m_fuelInTank" + index, 0, 100, 1);
            data.m_fuelCapacity = 40.0f;
            data.m_fuelRemainingLaps = IncrementalGenerator.GetFloatNumber("m_fuelRemainingLaps" + index, 0, 10, 0.1f);
            data.m_maxRPM = 12000;
            data.m_idleRPM = 2000;
            data.m_maxGears = 8;
            data.m_drsAllowed = (DRSAllowed)PerlinGenerator.NoiseInRange("m_drsAllowed" + index, 0, 1, Intensity.Low);
            data.m_drsActivationDistance = (ushort)IncrementalGenerator.GetIntNumber("m_drsActivationDistance" + index, 0, 100, 1);
            data.m_actualTyreCompound = (ActualTyreCompund)IncrementalGenerator.GetFloatNumber("m_actualTyreCompound" + index, 16, 20, 0.01f);
            data.m_visualTyreCompound = (VisualTyreCompound)IncrementalGenerator.GetFloatNumber("m_visualTyreCompound" + index, 16, 18, 0.01f);
            data.m_tyresAgeLaps = (byte)IncrementalGenerator.GetIntNumber("m_tyresAgeLaps" + index, 0, 20, 1); ;
            data.m_vehicleFiaFlags = (VehicleFiaFlags)IncrementalGenerator.GetFloatNumber("m_vehicleFiaFlags" + index, 0, 4, 0.01f);
            data.m_ersStoreEnergy = IncrementalGenerator.GetFloatNumber("m_ersStoreEnergy" + index, 0, 100, 1);
            data.m_ersDeployMode = (ERSDeployMode)IncrementalGenerator.GetFloatNumber("m_ersDeployMode" + index, 0, 4, 0.01f);
            data.m_ersHarvestedThisLapMGUK = IncrementalGenerator.GetFloatNumber("m_ersHarvestedThisLapMGUK" + index, 0, 4, 0.01f);
            data.m_ersHarvestedThisLapMGUH = IncrementalGenerator.GetFloatNumber("m_ersHarvestedThisLapMGUH" + index, 0, 4, 0.01f);
            data.m_ersDeployedThisLap = IncrementalGenerator.GetFloatNumber("m_ersDeployedThisLap" + index, 0, 4, 0.01f);
            data.m_networkPaused = (byte)PerlinGenerator.NoiseInRange("m_networkPaused" + index, 0, 1, Intensity.Low);
            return data;
        }

        private static PacketCarStatusData GetRandomPacketCarStatusData()
        {
            PacketCarStatusData data = new PacketCarStatusData();
            data.m_header = MockPacketHeader.GetBytes(PacketType.CarStatus);
            CarStatusData[] cars = new CarStatusData[22];
            for (int i = 0; i < cars.Length; i++)
            {
                cars[i] = GetRandomCarStatusData(i);
            }
            data.m_carStatusData = cars;
            return data;
        }

        public static byte[] GetBytes()
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
