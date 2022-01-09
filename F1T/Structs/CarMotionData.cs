using System.Runtime.InteropServices;

namespace F1T.Structs
{
    [StructLayout(LayoutKind.Explicit, Pack = 0, Size = 60)]
    public struct CarMotionData
    {
        [FieldOffset(0)]
        public float m_worldPositionX;           // World space X position
        [FieldOffset(4)]
        public float m_worldPositionY;           // World space Y position
        [FieldOffset(8)]
        public float m_worldPositionZ;           // World space Z position
        [FieldOffset(12)]
        public float m_worldVelocityX;           // Velocity in world space X
        [FieldOffset(16)]
        public float m_worldVelocityY;           // Velocity in world space Y
        [FieldOffset(20)]
        public float m_worldVelocityZ;           // Velocity in world space Z
        [FieldOffset(22)]
        public short m_worldForwardDirX;         // World space forward X direction (normalised)
        [FieldOffset(24)]
        public short m_worldForwardDirY;         // World space forward Y direction (normalised)
        [FieldOffset(26)]
        public short m_worldForwardDirZ;         // World space forward Z direction (normalised)
        [FieldOffset(28)]
        public short m_worldRightDirX;           // World space right X direction (normalised)
        [FieldOffset(30)]
        public short m_worldRightDirY;           // World space right Y direction (normalised)
        [FieldOffset(32)]
        public short m_worldRightDirZ;           // World space right Z direction (normalised)
        [FieldOffset(36)]
        public float m_gForceLateral;            // Lateral G-Force component
        [FieldOffset(40)]
        public float m_gForceLongitudinal;       // Longitudinal G-Force component
        [FieldOffset(44)]
        public float m_gForceVertical;           // Vertical G-Force component
        [FieldOffset(48)]
        public float m_yaw;                      // Yaw angle in radians
        [FieldOffset(52)]
        public float m_pitch;                    // Pitch angle in radians
        [FieldOffset(56)]
        public float m_roll;                     // Roll angle in radians

    };
}
