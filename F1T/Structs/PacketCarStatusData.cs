using System.Runtime.InteropServices;

namespace F1T.Structs
{

    public enum TractionControl : byte
    {
        Off,
        Medium,
        Full
    }
    public enum AntiLockBrakes : byte
    {
        Off,
        On
    }
    public enum FuelMix : byte
    {
        Lean,
        Standard,
        Rich,
        Max
    }
    public enum PitLimiterStatus : byte
    {
        Off,
        On
    }
    public enum ActualTyreCompund : byte
    {
        None = 0,
        C5 = 16,
        C4 = 17,
        C3 = 18,
        C2 = 19,
        C1 = 20,
        Inter = 7,
        Wet = 8,
        ClassicDry = 9,
        ClassicWet = 10,
        F2SuperSoft = 11,
        F2Soft = 12,
        F2Medium = 13,
        F2Hard = 14,
        F2Wet = 15,
        Unknown = 255,
    }
    public enum VisualTyreCompound : byte
    {
        None = 0,
        Soft = 16,
        Medium = 17,
        Hard = 18,
        Inter = 7,
        Wet = 8,
        ClassicDry = 9,
        ClassicWet = 10,
        F2Wet = 15,
        F2SuperSoft = 19,
        F2Soft = 20,
        F2Medium = 21,
        F2Hard = 22,
        Unknown = 255
    }
    public enum VehicleFiaFlags : sbyte
    {
        Unknown = -1,
        None,
        Green ,
        Blue,
        Yellow
    }
    public enum ERSDeployMode : byte
    {
        None,
        Medium,
        Hotlap,
        Overtake
    }

    public enum DRSAllowed : byte
    {
        NotAllowed,
        Allowed
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CarStatusData
    {
        public TractionControl m_tractionControl;          // Traction control - 0 = off, 1 = medium, 2 = full
        public AntiLockBrakes m_antiLockBrakes;           // 0 (off) - 1 (on)
        public FuelMix m_fuelMix;                  // Fuel mix - 0 = lean, 1 = standard, 2 = rich, 3 = max
        public byte m_frontBrakeBias;           // Front brake bias (percentage)
        public PitLimiterStatus m_pitLimiterStatus;         // Pit limiter status - 0 = off, 1 = on
        public float m_fuelInTank;               // Current fuel mass
        public float m_fuelCapacity;             // Fuel capacity
        public float m_fuelRemainingLaps;        // Fuel remaining in terms of laps (value on MFD)
        public ushort m_maxRPM;                   // Cars max RPM, point of rev limiter
        public ushort m_idleRPM;                  // Cars idle RPM
        public byte m_maxGears;                 // Maximum number of gears
        public DRSAllowed m_drsAllowed;               // 0 = not allowed, 1 = allowed
        public ushort m_drsActivationDistance;    // 0 = DRS not available, non-zero - DRS will be available
                                                  // in [X] metres
        public ActualTyreCompund m_actualTyreCompound;    // F1 Modern - 16 = C5, 17 = C4, 18 = C3, 19 = C2, 20 = C1
                                             // 21 = C0, 7 = inter, 8 = wet
                                             // F1 Classic - 9 = dry, 10 = wet
                                             // F2 – 11 = super soft, 12 = soft, 13 = medium, 14 = hard
                                             // 15 = wet
        public VisualTyreCompound m_visualTyreCompound;       // F1 visual (can be different from actual compound)
                                                // 16 = soft, 17 = medium, 18 = hard, 7 = inter, 8 = wet
                                                // F1 Classic – same as above
                                                // F2 ‘19, 15 = wet, 19 – super soft, 20 = soft
                                                // 21 = medium , 22 = hard
        public byte m_tyresAgeLaps;             // Age in laps of the current set of tyres
        public VehicleFiaFlags m_vehicleFiaFlags;    // -1 = invalid/unknown, 0 = none, 1 = green
                                           // 2 = blue, 3 = yellow
        public float m_enginePowerICE;           // Engine power output of ICE (W)
        public float m_enginePowerMGUK;          // Engine power output of MGU-K (W)
        public float m_ersStoreEnergy;           // ERS energy store in Joules
        public ERSDeployMode m_ersDeployMode;            // ERS deployment mode, 0 = none, 1 = medium
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
