using System.Runtime.InteropServices;

namespace F1T.Structs
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct LapHistoryData
    {
        public uint m_lapTimeInMS;           // Lap time in milliseconds
        public ushort m_sector1TimeInMS;       // Sector 1 time in milliseconds
        public byte m_sector1TimeMinutes;    // Sector 1 whole minute part
        public ushort m_sector2TimeInMS;       // Sector 2 time in milliseconds
        public byte m_sector2TimeMinutes;    // Sector 2 whole minute part
        public ushort m_sector3TimeInMS;       // Sector 3 time in milliseconds
        public byte m_sector3TimeMinutes;    // Sector 3 whole minute part
        public byte m_lapValidBitFlags;      // 0x01 bit set-lap valid,      0x02 bit set-sector 1 valid
                                       // 0x04 bit set-sector 2 valid, 0x08 bit set-sector 3 valid
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct TyreStintHistoryData
    {
        public byte m_endLap;                // Lap the tyre usage ends on (255 of current tyre)
        public ActualTyreCompund m_tyreActualCompound;    // Actual tyres used by this driver
        public VisualTyreCompound m_tyreVisualCompound;    // Visual tyres used by this driver
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PacketSessionHistoryData
    {
        public PacketHeader m_header;                   // Header

        public byte m_carIdx;                   // Index of the car this lap data relates to
        public byte m_numLaps;                  // Num laps in the data (including current partial lap)
        public byte m_numTyreStints;            // Number of tyre stints in the data

        public byte m_bestLapTimeLapNum;        // Lap the best lap time was achieved on
        public byte m_bestSector1LapNum;        // Lap the best Sector 1 time was achieved on
        public byte m_bestSector2LapNum;        // Lap the best Sector 2 time was achieved on
        public byte m_bestSector3LapNum;        // Lap the best Sector 3 time was achieved on

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 100)]
        public LapHistoryData[] m_lapHistoryData;   // 100 laps of data max
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public TyreStintHistoryData[] m_tyreStintsHistoryData;

    }
}
