using System.Runtime.InteropServices;
namespace F1T.Structs
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CarSetupData
    {
        public byte m_frontWing;                // Front wing aero
        public byte m_rearWing;                 // Rear wing aero
        public byte m_onThrottle;               // Differential adjustment on throttle (percentage)
        public byte m_offThrottle;              // Differential adjustment off throttle (percentage)
        public float m_frontCamber;              // Front camber angle (suspension geometry)
        public float m_rearCamber;               // Rear camber angle (suspension geometry)
        public float m_frontToe;                 // Front toe angle (suspension geometry)
        public float m_rearToe;                  // Rear toe angle (suspension geometry)
        public byte m_frontSuspension;          // Front suspension
        public byte m_rearSuspension;           // Rear suspension
        public byte m_frontAntiRollBar;         // Front anti-roll bar
        public byte m_rearAntiRollBar;          // Front anti-roll bar
        public byte m_frontSuspensionHeight;    // Front ride height
        public byte m_rearSuspensionHeight;     // Rear ride height
        public byte m_brakePressure;            // Brake pressure (percentage)
        public byte m_brakeBias;                // Brake bias (percentage)
        public float m_rearLeftTyrePressure;     // Rear left tyre pressure (PSI)
        public float m_rearRightTyrePressure;    // Rear right tyre pressure (PSI)
        public float m_frontLeftTyrePressure;    // Front left tyre pressure (PSI)
        public float m_frontRightTyrePressure;   // Front right tyre pressure (PSI)
        public byte m_ballast;                  // Ballast
        public float m_fuelLoad;                 // Fuel load
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PacketCarSetupData
    {
        public PacketHeader m_header;                   // Header

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 22)]
        public CarSetupData[] m_carSetups;
    }
}
