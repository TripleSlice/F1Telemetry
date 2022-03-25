using F1T.Structs;
using F1TMock.Utils;
using System.Runtime.InteropServices;

namespace F1TMock.Mock
{
    public static class MockPacketCarDamageData
    {
        private static CarDamageData GetRandomCarDamageData(int index)
        {
            CarDamageData carDamageData = new CarDamageData();
            carDamageData.m_tyresWear = IncrementalGenerator.GenerateFloatArray("m_tyresWear" + index, 0, 100, 1, 4);
            carDamageData.m_tyresDamage = IncrementalGenerator.GenerateByteArray("m_tyresDamage" + index, 0, 100, 1, 4);
            carDamageData.m_brakesDamage = IncrementalGenerator.GenerateByteArray("m_brakesDamage" + index, 0, 100, 1, 4);
            carDamageData.m_frontLeftWingDamage = (byte)IncrementalGenerator.GetIntNumber("m_frontLeftWingDamage" + index, 0, 100, 1);
            carDamageData.m_frontRightWingDamage = (byte)IncrementalGenerator.GetIntNumber("m_frontRightWingDamage" + index, 0, 100, 1);
            carDamageData.m_rearWingDamage = (byte)IncrementalGenerator.GetIntNumber("m_rearWingDamage" + index, 0, 100, 1);
            carDamageData.m_floorDamage = (byte)IncrementalGenerator.GetIntNumber("m_frontm_floorDamageLeftWingDamage" + index, 0, 100, 1);
            carDamageData.m_diffuserDamage = (byte)IncrementalGenerator.GetIntNumber("m_diffuserDamage" + index, 0, 100, 1);
            carDamageData.m_sidepodDamage = (byte)IncrementalGenerator.GetIntNumber("m_sidepodDamage" + index, 0, 100, 1);
            carDamageData.m_drsFault = (DRSFault)PerlinGenerator.NoiseInRange("m_drsFault" + index, 0, 1, Intensity.Low);
            carDamageData.m_gearBoxDamage = (byte)IncrementalGenerator.GetIntNumber("m_gearBoxDamage" + index, 0, 100, 1);
            carDamageData.m_engineDamage = (byte)IncrementalGenerator.GetIntNumber("m_engineDamage" + index, 0, 100, 1);
            carDamageData.m_engineMGUHWear = (byte)IncrementalGenerator.GetIntNumber("m_engineMGUHWear" + index, 0, 100, 1);
            carDamageData.m_engineESWear = (byte)IncrementalGenerator.GetIntNumber("m_engineESWear" + index, 0, 100, 1);
            carDamageData.m_engineCEWear = (byte)IncrementalGenerator.GetIntNumber("m_engineCEWear" + index, 0, 100, 1);
            carDamageData.m_engineICEWear = (byte)IncrementalGenerator.GetIntNumber("m_engineICEWear" + index, 0, 100, 1);
            carDamageData.m_engineMGUKWear = (byte)IncrementalGenerator.GetIntNumber("m_engineMGUKWear" + index, 0, 100, 1);
            carDamageData.m_engineTCWear = (byte)IncrementalGenerator.GetIntNumber("m_engineTCWear" + index, 0, 100, 1);
            return carDamageData;
        }

        private static PacketCarDamageData GetRandomPacketCarDamageData()
        {
            PacketCarDamageData packet = new PacketCarDamageData();
            packet.m_header = MockPacketHeader.GetBytes(PacketType.CarDamage);
            CarDamageData[] carDamageDatas = new CarDamageData[22];
            for (int i = 0; i < carDamageDatas.Length; i++)
            {
                carDamageDatas[i] = GetRandomCarDamageData(i);
            }
            packet.m_carDamageData = carDamageDatas;
            return packet;
        }

        public static byte[] GetBytes()
        {
            var packet = GetRandomPacketCarDamageData();
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
