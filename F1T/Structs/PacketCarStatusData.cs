using System.Runtime.InteropServices;

namespace F1T.Structs
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CarStatusData
    {
        public byte m_tractionControl;          // Traction control - 0 = off, 1 = medium, 2 = full
        public byte m_antiLockBrakes;           // 0 (off) - 1 (on)
        public byte m_fuelMix;                  // Fuel mix - 0 = lean, 1 = standard, 2 = rich, 3 = max
        public byte m_frontBrakeBias;           // Front brake bias (percentage)
        public byte m_pitLimiterStatus;         // Pit limiter status - 0 = off, 1 = on
        public float m_fuelInTank;               // Current fuel mass
        public float m_fuelCapacity;             // Fuel capacity
        public float m_fuelRemainingLaps;        // Fuel remaining in terms of laps (value on MFD)
        public ushort m_maxRPM;                   // Cars max RPM, point of rev limiter
        public ushort m_idleRPM;                  // Cars idle RPM
        public byte m_maxGears;                 // Maximum number of gears
        public byte m_drsAllowed;               // 0 = not allowed, 1 = allowed
        public ushort m_drsActivationDistance;    // 0 = DRS not available, non-zero - DRS will be available
                                                  // in [X] metres
        public byte m_actualTyreCompound;    // F1 Modern - 16 = C5, 17 = C4, 18 = C3, 19 = C2, 20 = C1
                                             // 7 = inter, 8 = wet
                                             // F1 Classic - 9 = dry, 10 = wet
                                             // F2 – 11 = super soft, 12 = soft, 13 = medium, 14 = hard
                                             // 15 = wet
        public byte m_visualTyreCompound;       // F1 visual (can be different from actual compound)
                                                // 16 = soft, 17 = medium, 18 = hard, 7 = inter, 8 = wet
                                                // F1 Classic – same as above
                                                // F2 ‘19, 15 = wet, 19 – super soft, 20 = soft
                                                // 21 = medium , 22 = hard
        public byte m_tyresAgeLaps;             // Age in laps of the current set of tyres
        public sbyte m_vehicleFiaFlags;    // -1 = invalid/unknown, 0 = none, 1 = green
                                           // 2 = blue, 3 = yellow, 4 = red
        public float m_ersStoreEnergy;           // ERS energy store in Joules
        public byte m_ersDeployMode;            // ERS deployment mode, 0 = none, 1 = medium
                                                // 2 = hotlap, 3 = overtake
        public float m_ersHarvestedThisLapMGUK;  // ERS energy harvested this lap by MGU-K
        public float m_ersHarvestedThisLapMGUH;  // ERS energy harvested this lap by MGU-H
        public float m_ersDeployedThisLap;       // ERS energy deployed this lap
        public byte m_networkPaused;            // Whether the car is paused in a network game

    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PacketCarStatusData
    {
        public PacketHeader m_header;        // Header
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 22)]
        public CarStatusData[] m_carStatusData;
    }
}
