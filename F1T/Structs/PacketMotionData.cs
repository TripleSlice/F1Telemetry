using System.Runtime.InteropServices;

namespace F1T.Structs
{

    [StructLayout(LayoutKind.Explicit, Pack = 0, Size = 1464)]
    public struct PacketMotionData
    {
        [FieldOffset(0)]
        public PacketHeader m_header;                  // Header
        [FieldOffset(24)]
        public CarMotionData[] m_carMotionData;      // Data for all cars on track

        // Extra player car ONLY data
        [FieldOffset(1344)]
        public FloatQuad m_suspensionPosition;       // Note: All wheel arrays have the following order:
        [FieldOffset(1360)]
        public FloatQuad m_suspensionVelocity;       // RL, RR, FL, FR
        [FieldOffset(1376)]
        public FloatQuad m_suspensionAcceleration;  // RL, RR, FL, FR
        [FieldOffset(1392)]
        public FloatQuad m_wheelSpeed;              // Speed of each wheel
        [FieldOffset(1408)]
        public FloatQuad m_wheelSlip;                // Slip ratio for each wheel
        [FieldOffset(1424)]
        public float m_localVelocityX;             // Velocity in local space
        [FieldOffset(1428)]
        public float m_localVelocityY;             // Velocity in local space
        [FieldOffset(1432)]
        public float m_localVelocityZ;             // Velocity in local space
        [FieldOffset(1436)]
        public float m_angularVelocityX;       // Angular velocity x-component
        [FieldOffset(1440)]
        public float m_angularVelocityY;            // Angular velocity y-component
        [FieldOffset(1444)]
        public float m_angularVelocityZ;            // Angular velocity z-component
        [FieldOffset(1448)]
        public float m_angularAccelerationX;        // Angular velocity x-component
        [FieldOffset(1452)]
        public float m_angularAccelerationY;   // Angular velocity y-component
        [FieldOffset(1456)]
        public float m_angularAccelerationZ;        // Angular velocity z-component
        [FieldOffset(1460)]
        public float m_frontWheelsAngle;            // Current front wheels angle in radians

    };
}
