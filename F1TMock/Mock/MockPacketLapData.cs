using F1T.Structs;
using System.Runtime.InteropServices;

namespace F1TMock.Mock
{
    public static class MockPacketLapData
    {
        private static LapData GetRandomLapData()
        {
            LapData data = new LapData();
            data.m_lastLapTimeInMS = 100000;
            data.m_currentLapTimeInMS = 2000;
            data.m_sector1TimeInMS = 1999;
            data.m_sector2TimeInMS = 5000;
            data.m_lapDistance = 5000;
            data.m_totalDistance = 25000;
            data.m_safetyCarDelta = 1000;
            data.m_carPosition = 1;
            data.m_currentLapNum = 20;
            data.m_pitStatus = PitStatus.None;
            data.m_numPitStops = 0;
            data.m_sector = Sector.SectorOne;
            data.m_currentLapInvalid = 0;
            data.m_penalties = 0;
            data.m_warnings = 0;
            data.m_numUnservedDriveThroughPens = 0;
            data.m_numUnservedStopGoPens = 0;
            data.m_gridPosition = 1;
            data.m_driverStatus = DriverStatus.OnTrack;
            data.m_resultStatus = ResultStatus.Inactive;
            data.m_pitLaneTimerActive = PitLaneTimerActive.InActive;
            data.m_pitLaneTimeInLaneInMS = 0;
            data.m_pitStopTimerInMS = 0;
            data.m_pitStopShouldServePen = 0;
            return data;
        }

        private static PacketLapData GetRandomPacketLapData()
        {
            F1T.Structs.PacketLapData data = new F1T.Structs.PacketLapData();
            data.m_header = MockPacketHeader.GetBytes(PacketType.LapData);
            LapData[] laps = new LapData[22];
            for (int i = 0; i < laps.Length; i++)
            {
                laps[i] = GetRandomLapData();
            }
            data.m_lapData = laps;
            return data;
        }

        public static byte[] GetBytes()
        {
            var packet = GetRandomPacketLapData();
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
