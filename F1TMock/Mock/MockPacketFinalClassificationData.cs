using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using F1T.Structs;

namespace F1TMock.Mock
{
    public static class MockPacketFinalClassificationData
    {

        public static FinalClassificationData GetRandomPacketFinalClassifcationData(int index)
        {
            FinalClassificationData data = new FinalClassificationData();

            data.m_position = (byte)(index + 1);
            data.m_numLaps = 20;
            data.m_gridPosition = (byte)(index + 1);
            data.m_points = (byte)(index + 1);
            data.m_numPitStops = 2;
            data.m_resultStatus = ResultStatus.Finished;
            data.m_bestLapTimeInMS = 20000;
            data.m_totalRaceTime = 5400;
            data.m_penaltiesTime = 5;
            data.m_numPenalties = 1;
            data.m_numTyreStints = 2;
            data.m_tyreStintsActual = new ActualTyreCompund[] { ActualTyreCompund.C1, ActualTyreCompund.C2, ActualTyreCompund.None, ActualTyreCompund.None, ActualTyreCompund.None, ActualTyreCompund.None, ActualTyreCompund.None, ActualTyreCompund.None, };
            data.m_tyreStintsVisual = new VisualTyreCompound[] { VisualTyreCompound.Soft, VisualTyreCompound.Medium, VisualTyreCompound.None, VisualTyreCompound.None, VisualTyreCompound.None, VisualTyreCompound.None, VisualTyreCompound.None, VisualTyreCompound.None, };

            return data;
        }

        public static PacketFinalClassificationData GetRandomPacketFinalClassifcationData()
        {
            PacketFinalClassificationData packet = new PacketFinalClassificationData();
            packet.m_header = MockPacketHeader.GetBytes(PacketType.FinalClassification);
            packet.m_numCars = 22;
            FinalClassificationData[] data = new FinalClassificationData[22];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = GetRandomPacketFinalClassifcationData(i);
            }
            packet.m_classificationData = data;
            return packet;
        }


        public static byte[] GetBytes()
        {
            var packet = GetRandomPacketFinalClassifcationData();
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
