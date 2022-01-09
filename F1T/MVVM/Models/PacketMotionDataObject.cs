using F1T.Core;
using F1T.Structs;

namespace F1T.MVVM.Models
{
    public class PacketMotionDataObject : ObservableObject
    {
        private CarMotionDataObject[] _m_carMotionData;
        public CarMotionDataObject[] m_carMotionData // Speed of car in kilometres per hour
        {
            get { return _m_carMotionData; }
            set { SetField(ref _m_carMotionData, value, "m_carMotionData"); }
        }

        // Extra player car ONLY data
        public FloatQuad m_suspensionPosition;       // Note: All wheel arrays have the following order:
        public FloatQuad m_suspensionVelocity;       // RL, RR, FL, FR
        public FloatQuad m_suspensionAcceleration;  // RL, RR, FL, FR
        public FloatQuad m_wheelSpeed;              // Speed of each wheel
        public FloatQuad m_wheelSlip;                // Slip ratio for each wheel
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

        public PacketMotionDataObject(PacketMotionData data)
        {
            m_carMotionData = CarMotionDataObject.FromArray(data.m_carMotionData);

            m_suspensionPosition = data.m_suspensionPosition;
            m_suspensionVelocity = data.m_suspensionVelocity;
            m_suspensionAcceleration = data.m_suspensionAcceleration;
            m_wheelSpeed = data.m_wheelSpeed;
            m_wheelSlip = data.m_wheelSlip;
            m_localVelocityX = data.m_localVelocityX;
            m_localVelocityY = data.m_localVelocityY;
            m_localVelocityZ = data.m_localVelocityZ;
            m_angularVelocityX = data.m_angularVelocityX;
            m_angularVelocityY = data.m_angularVelocityY;
            m_angularVelocityZ = data.m_angularVelocityZ;
            m_angularAccelerationX = data.m_angularAccelerationX;
            m_angularAccelerationY = data.m_angularAccelerationY;
            m_angularAccelerationZ = data.m_angularAccelerationZ;
            m_frontWheelsAngle = data.m_frontWheelsAngle;
        }
    }
}
