using System.Runtime.InteropServices;
namespace F1T.Structs
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct FinalClassificationData
    {
        public byte m_position;              // Finishing position
        public byte m_numLaps;               // Number of laps completed
        public byte m_gridPosition;          // Grid position of the car
        public byte m_points;                // Number of points scored
        public byte m_numPitStops;           // Number of pit stops made
        public ResultStatus m_resultStatus;          // Result status - 0 = invalid, 1 = inactive, 2 = active
                                             // 3 = finished, 4 = didnotfinish, 5 = disqualified
                                             // 6 = not classified, 7 = retired
        public uint m_bestLapTimeInMS;       // Best lap time of the session in milliseconds
        public double m_totalRaceTime;         // Total race time in seconds without penalties
        public byte m_penaltiesTime;         // Total penalties accumulated in seconds
        public byte m_numPenalties;          // Number of penalties applied to this driver
        public byte m_numTyreStints;         // Number of tyres stints up to maximum
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public ActualTyreCompund[] m_tyreStintsActual;   // Actual tyres used by this driver
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public VisualTyreCompound[] m_tyreStintsVisual;   // Visual tyres used by this driver
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PacketFinalClassificationData
    {
        public PacketHeader m_header;                   // Header

        public byte m_numCars;          // Number of cars in the final classification
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 22)]
        public FinalClassificationData[] m_classificationData;
    }

}
