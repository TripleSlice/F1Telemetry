using System.Runtime.InteropServices;

namespace F1T.Structs
{

    [StructLayout(LayoutKind.Explicit, Pack = 0, Size = 60)]
    public struct CarTelemetryData
    {
        [FieldOffset(0)]
        public ushort m_speed;                    // Speed of car in kilometres per hour
        [FieldOffset(2)]
        public float m_throttle;                 // Amount of throttle applied (0.0 to 1.0)
        [FieldOffset(6)]
        public float m_steer;                    // Steering (-1.0 (full lock left) to 1.0 (full lock right))
        [FieldOffset(10)]
        public float m_brake;                    // Amount of brake applied (0.0 to 1.0)
        [FieldOffset(14)]
        public byte m_clutch;                   // Amount of clutch applied (0 to 100)
        [FieldOffset(15)]
        public sbyte m_gear;                     // Gear selected (1-8, N=0, R=-1)
        [FieldOffset(16)]
        public ushort m_engineRPM;                // Engine RPM
        [FieldOffset(18)]
        public byte m_drs;                      // 0 = off, 1 = on
        [FieldOffset(19)]
        public byte m_revLightsPercent;         // Rev lights indicator (percentage)
        [FieldOffset(20)]
        public ushort m_revLightsBitValue;       // Rev lights (bit 0 = leftmost LED, bit 14 = rightmost LED)
        [FieldOffset(22)]
        public UInt16Quad m_brakesTemperature;     // Brakes temperature (celsius)
        [FieldOffset(30)]
        public UInt8Quad m_tyresSurfaceTemperature; // Tyres surface temperature (celsius)
        [FieldOffset(34)]
        public UInt8Quad m_tyresInnerTemperature; // Tyres inner temperature (celsius)
        [FieldOffset(38)]
        public ushort m_engineTemperature;        // Engine temperature (celsius)
        [FieldOffset(40)]
        public SingleQuad m_tyresPressure;         // Tyres pressure (PSI)
        [FieldOffset(56)]
        public ByteQuad m_surfaceType;           // Driving surface, see appendices
    };
}
