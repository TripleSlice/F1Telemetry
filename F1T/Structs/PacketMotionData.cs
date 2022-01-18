using System.Runtime.InteropServices;

namespace F1T.Structs
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CarMotionData
    {
        public float m_worldPositionX;           // World space X position
        public float m_worldPositionY;           // World space Y position
        public float m_worldPositionZ;           // World space Z position
        public float m_worldVelocityX;           // Velocity in world space X
        public float m_worldVelocityY;           // Velocity in world space Y
        public float m_worldVelocityZ;           // Velocity in world space Z
        public short m_worldForwardDirX;         // World space forward X direction (normalised)
        public short m_worldForwardDirY;         // World space forward Y direction (normalised)
        public short m_worldForwardDirZ;         // World space forward Z direction (normalised)
        public short m_worldRightDirX;           // World space right X direction (normalised)
        public short m_worldRightDirY;           // World space right Y direction (normalised)
        public short m_worldRightDirZ;           // World space right Z direction (normalised)
        public float m_gForceLateral;            // Lateral G-Force component
        public float m_gForceLongitudinal;       // Longitudinal G-Force component
        public float m_gForceVertical;           // Vertical G-Force component
        public float m_yaw;                      // Yaw angle in radians
        public float m_pitch;                    // Pitch angle in radians
        public float m_roll;                     // Roll angle in radians

    }
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PacketMotionData
    {
        public PacketHeader m_header;                  // Header
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 22)]
        public CarMotionData[] m_carMotionData;      // Data for all cars on track

        // Extra player car ONLY data
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public float[] m_suspensionPosition;       // Note: All wheel arrays have the following order:
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public float[] m_suspensionVelocity;       // RL, RR, FL, FR
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public float[] m_suspensionAcceleration;  // RL, RR, FL, FR
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public float[] m_wheelSpeed;              // Speed of each wheel
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public float[] m_wheelSlip;                // Slip ratio for each wheel
        public float m_localVelocityX;             // Velocity in local space
        public float m_localVelocityY;             // Velocity in local space
        public float m_localVelocityZ;             // Velocity in local space
        public float m_angularVelocityX;       // Angular velocity x-component
        public float m_angularVelocityY;            // Angular velocity y-component
        public float m_angularVelocityZ;            // Angular velocity z-component
        public float m_angularAccelerationX;        // Angular velocity x-component
        public float m_angularAccelerationY;   // Angular velocity y-component
        public float m_angularAccelerationZ;        // Angular velocity z-component
        public float m_frontWheelsAngle;            // Current front wheels angle in radians

    }
}
