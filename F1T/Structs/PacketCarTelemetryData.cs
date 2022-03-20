using System.Runtime.InteropServices;

namespace F1T.Structs
{
    public enum DRS : byte
    {
        Off,
        On
    }
    
    public enum SurfaceType : byte
    {
        Tarmac,
        RumbleStrip,
        Concrete,
        Rock,
        Gravel,
        Mud,
        Sand,
        Grass,
        Water,
        Cobblestone,
        Metal,
        Ridget
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CarTelemetryData
    {
        public ushort m_speed;                    // Speed of car in kilometres per hour
        public float m_throttle;                 // Amount of throttle applied (0.0 to 1.0)
        public float m_steer;                    // Steering (-1.0 (full lock left) to 1.0 (full lock right))
        public float m_brake;                    // Amount of brake applied (0.0 to 1.0)
        public byte m_clutch;                   // Amount of clutch applied (0 to 100)
        public sbyte m_gear;                     // Gear selected (1-8, N=0, R=-1)
        public ushort m_engineRPM;                // Engine RPM
        public DRS m_drs;                      // 0 = off, 1 = on
        public byte m_revLightsPercent;         // Rev lights indicator (percentage)
        public ushort m_revLightsBitValue;        // Rev lights (bit 0 = leftmost LED, bit 14 = rightmost LED)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public ushort[] m_brakesTemperature;     // Brakes temperature (celsius)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] m_tyresSurfaceTemperature; // Tyres surface temperature (celsius)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] m_tyresInnerTemperature; // Tyres inner temperature (celsius)
        public ushort m_engineTemperature;        // Engine temperature (celsius)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public float[] m_tyresPressure;         // Tyres pressure (PSI)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public SurfaceType[] m_surfaceType;           // Driving surface, see appendices

    }

    public enum MFDPanelIndex : byte
    {
        CarSetup,
        Pits,
        Damage,
        Engine,
        Temeratures,
        Closed = 255
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PacketCarTelemetryData
    {
        public PacketHeader m_header;        // Header
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 22)]
        public CarTelemetryData[] m_carTelemetryData;

        public MFDPanelIndex m_mfdPanelIndex;       // Index of MFD panel open - 255 = MFD closed
                                           // Single player, race – 0 = Car setup, 1 = Pits
                                           // 2 = Damage, 3 =  Engine, 4 = Temperatures
                                           // May vary depending on game mode
        public byte m_mfdPanelIndexSecondaryPlayer;   // See above
        public sbyte m_suggestedGear;       // Suggested gear for the player (1-8)
                                    // 0 if no gear suggested

    }
}
