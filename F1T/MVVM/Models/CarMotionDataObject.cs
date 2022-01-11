using F1T.Core;
using F1T.Structs;

namespace F1T.MVVM.Models
{
    public class CarMotionDataObject : ObservableObject
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

        public CarMotionDataObject()
        {
            m_worldPositionX = 0;
            m_worldPositionY = 0;
            m_worldPositionZ = 0;
            m_worldVelocityX = 0;
            m_worldVelocityY = 0;
            m_worldVelocityZ = 0;
            m_worldForwardDirX = 0;
            m_worldForwardDirY = 0;
            m_worldForwardDirZ = 0;
            m_worldRightDirX = 0;
            m_worldRightDirY = 0;
            m_worldRightDirZ = 0;
            m_gForceLateral = 0;
            m_gForceLongitudinal = 0;
            m_gForceVertical = 0;
            m_yaw = 0;
            m_pitch = 0;
            m_roll = 0;
        }

        public CarMotionDataObject(CarMotionData data)
        {
            m_worldPositionX = data.m_worldPositionX;
            m_worldPositionY = data.m_worldPositionY;
            m_worldPositionZ = data.m_worldPositionZ;
            m_worldVelocityX = data.m_worldVelocityX;
            m_worldVelocityY = data.m_worldVelocityY;
            m_worldVelocityZ = data.m_worldVelocityZ;
            m_worldForwardDirX = data.m_worldForwardDirX;
            m_worldForwardDirY = data.m_worldForwardDirY;
            m_worldForwardDirZ = data.m_worldForwardDirZ;
            m_worldRightDirX = data.m_worldRightDirX;
            m_worldRightDirY = data.m_worldRightDirY;
            m_worldRightDirZ = data.m_worldRightDirZ;
            m_gForceLateral = data.m_gForceLateral;
            m_gForceLongitudinal = data.m_gForceLongitudinal;
            m_gForceVertical = data.m_gForceVertical;
            m_yaw = data.m_yaw;
            m_pitch = data.m_pitch;
            m_roll = data.m_roll;
        }

        public static CarMotionDataObject[] FromArray(CarMotionData[] data)
        {
            CarMotionDataObject[] array = new CarMotionDataObject[22];

            for (int i = 0; i < data.Length; i++)
            {
                array[i] = new CarMotionDataObject(data[i]);
            }

            return array;
        }

    }
}
