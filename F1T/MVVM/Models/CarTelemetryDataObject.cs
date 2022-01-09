using F1T.Core;
using F1T.Structs;

namespace F1T.MVVM.Models
{
    public class CarTelemetryDataObject : ObservableObject
    {

        private ushort _m_speed;
        public ushort m_speed // Speed of car in kilometres per hour
        {
            get { return _m_speed; }
            set { SetField(ref _m_speed, value, "m_speed"); }
        }

        private float _m_throttle;
        public float m_throttle // Amount of throttle applied (0.0 to 1.0)
        {
            get { return _m_throttle; }
            set { SetField(ref _m_throttle, value, "m_throttle"); }
        }

        private float _m_steer;
        public float m_steer // Steering (-1.0 (full lock left) to 1.0 (full lock right))
        {
            get { return _m_steer; }
            set { SetField(ref _m_steer, value, "m_steer"); }
        }                    


        private float _m_brake;
        public float m_brake // Amount of brake applied (0.0 to 1.0)
        {
            get { return _m_brake; }
            set { SetField(ref _m_brake, value, "m_brake"); }
        }

        public byte m_clutch;                   // Amount of clutch applied (0 to 100)

        private sbyte _m_gear;
        public sbyte m_gear // Gear selected (1-8, N=0, R=-1)
        {
            get { return _m_gear; }
            set { SetField(ref _m_gear, value, "m_gear"); }
        }               
        
        public ushort m_engineRPM;                // Engine RPM
        public byte m_drs;                      // 0 = off, 1 = on
        public byte m_revLightsPercent;         // Rev lights indicator (percentage)
        public ushort m_revLightsBitValue;       // Rev lights (bit 0 = leftmost LED, bit 14 = rightmost LED)
        public UInt16Quad m_brakesTemperature;     // Brakes temperature (celsius)
        public UInt8Quad m_tyresSurfaceTemperature; // Tyres surface temperature (celsius)
        public UInt8Quad m_tyresInnerTemperature; // Tyres inner temperature (celsius)
        public ushort m_engineTemperature;        // Engine temperature (celsius)
        public FloatQuad m_tyresPressure;         // Tyres pressure (PSI)
        public ByteQuad m_surfaceType;           // Driving surface, see appendices

        public CarTelemetryDataObject()
        {
            m_speed = 0;
            m_throttle = 0;
            m_steer = 0;
            m_brake = 0;
            m_clutch = 0;
            m_gear = 0;
            m_engineRPM = 0;
            m_drs = 0;
            m_revLightsPercent = 0;
            m_revLightsBitValue = 0;
            m_brakesTemperature = new UInt16Quad();
            m_tyresSurfaceTemperature = new UInt8Quad();
            m_tyresInnerTemperature = new UInt8Quad();
            m_engineTemperature = 0;
            m_tyresPressure = new FloatQuad();
            m_surfaceType = new ByteQuad();
        }
        public CarTelemetryDataObject(CarTelemetryData data)
        {
            m_speed = data.m_speed;
            m_throttle = data.m_throttle;
            m_steer = data.m_steer;
            m_brake = data.m_brake;
            m_clutch = data.m_clutch;
            m_gear = data.m_gear;
            m_engineRPM = data.m_engineRPM;
            m_drs = data.m_drs;
            m_revLightsPercent = data.m_revLightsPercent;
            m_revLightsBitValue = data.m_revLightsBitValue;
            m_brakesTemperature = data.m_brakesTemperature;
            m_tyresSurfaceTemperature = data.m_tyresSurfaceTemperature;
            m_tyresInnerTemperature = data.m_tyresInnerTemperature;
            m_engineTemperature = data.m_engineTemperature;
            m_tyresPressure = data.m_tyresPressure;
            m_surfaceType = data.m_surfaceType;
        }

        public static CarTelemetryDataObject[] FromArray(CarTelemetryData[] data)
        {
            CarTelemetryDataObject[] array = new CarTelemetryDataObject[22];

            for (int i = 0; i<data.Length; i++)
            {
                array[i] = new CarTelemetryDataObject(data[i]);
            }

            return array;
        }
    }
}
