using F1T.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace F1TMock.Mock
{
    internal class MockPacketSessionHistoryData
    {

        private static LapHistoryData GetRandomLapHistoryData()
        {
            LapHistoryData data = new LapHistoryData();
            data.m_lapTimeInMS = 3000;
            data.m_sector1TimeInMS = 1000;
            data.m_sector2TimeInMS = 1000;
            data.m_sector3TimeInMS = 1000;
            data.m_lapValidBitFlags = 0;
            return data;
        }


        private static TyreStintHistoryData GetRandomTyreStintHistoryData()
        {
            TyreStintHistoryData data = new TyreStintHistoryData();
            data.m_endLap = 1;
            data.m_tyreActualCompound = ActualTyreCompund.C3;
            data.m_tyreVisualCompound = VisualTyreCompound.Soft;
            return data;
        }


        private static int counter = 0;
        private static PacketSessionHistoryData GetRandomPacketSessionHistory()
        {
            PacketSessionHistoryData packetSessionHistoryData = new PacketSessionHistoryData();
            packetSessionHistoryData.m_header = MockPacketHeader.GetBytes(PacketType.SessionHistory);



            packetSessionHistoryData.m_carIdx = 0 % 22;        
            packetSessionHistoryData.m_numLaps = 5;           
            packetSessionHistoryData.m_numTyreStints = 1;  

            packetSessionHistoryData.m_bestLapTimeLapNum = 1;  
            packetSessionHistoryData.m_bestSector1LapNum = 1;
            packetSessionHistoryData.m_bestSector2LapNum = 1; 
            packetSessionHistoryData.m_bestSector3LapNum = 1;  

        LapHistoryData[] lapHistoryDatas = new LapHistoryData[22];
            for (int i = 0; i < lapHistoryDatas.Length; i++)
            {
                lapHistoryDatas[i] = GetRandomLapHistoryData();
            }
            packetSessionHistoryData.m_lapHistoryData = lapHistoryDatas;

            TyreStintHistoryData[] tyreStintHistoryDatas = new TyreStintHistoryData[22];
            for (int i = 0; i < tyreStintHistoryDatas.Length; i++)
            {
                tyreStintHistoryDatas[i] = GetRandomTyreStintHistoryData();
            }
            packetSessionHistoryData.m_tyreStintsHistoryData = tyreStintHistoryDatas;


            counter++;

            return packetSessionHistoryData;
        }

        public static byte[] GetBytes()
        {
            PacketSessionHistoryData packet = GetRandomPacketSessionHistory();
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
