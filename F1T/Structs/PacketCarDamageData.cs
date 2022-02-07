using System.Runtime.InteropServices;

namespace F1T.Structs
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CarDamageData
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public float[] m_tyresWear;                     // Tyre wear (percentage)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] m_tyresDamage;                   // Tyre damage (percentage)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] m_brakesDamage;                  // Brakes damage (percentage)
        public byte m_frontLeftWingDamage;              // Front left wing damage (percentage)
        public byte m_frontRightWingDamage;             // Front right wing damage (percentage)
        public byte m_rearWingDamage;                   // Rear wing damage (percentage)
        public byte m_floorDamage;                      // Floor damage (percentage)
        public byte m_diffuserDamage;                   // Diffuser damage (percentage)
        public byte m_sidepodDamage;                    // Sidepod damage (percentage)
        public byte m_drsFault;                         // Indicator for DRS fault, 0 = OK, 1 = fault
        public byte m_gearBoxDamage;                    // Gear box damage (percentage)
        public byte m_engineDamage;                     // Engine damage (percentage)
        public byte m_engineMGUHWear;                   // Engine wear MGU-H (percentage)
        public byte m_engineESWear;                     // Engine wear ES (percentage)
        public byte m_engineCEWear;                     // Engine wear CE (percentage)
        public byte m_engineICEWear;                    // Engine wear ICE (percentage)
        public byte m_engineMGUKWear;                   // Engine wear MGU-K (percentage)
        public byte m_engineTCWear;                     // Engine wear TC (percentage)
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PacketCarDamageData
    {
        public PacketHeader m_header;        // Header
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 22)]
        public CarDamageData[] m_carDamageData;
    }
}
