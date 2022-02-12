using F1T.Settings;
using F1T.Structs;
using System;

namespace F1T.MVVM.ViewModels
{
    public class InputTelemetryViewModel : BaseModuleViewModel<InputTelemetrySettings>
    {
        // === BEGINING OF MODULE SETUP ===
        // === Singleton Instance with Thread Saftey ===
        private static InputTelemetryViewModel _instance = null;
        private static object _singletonLock = new object();
        public static InputTelemetryViewModel GetInstance()
        {
            lock (_singletonLock)
            {
                if (_instance == null) { _instance = new InputTelemetryViewModel(); }
                return _instance;
            }
        }

        private InputTelemetrySettings _settings = new InputTelemetrySettings().Read<InputTelemetrySettings>();
        public override InputTelemetrySettings Settings { get => _settings; }
        // === END OF MODULE SETUP ===

        public PacketCarTelemetryData CarTelemetryData;
        public CarTelemetryData PlayerCarTelemetryData;
        public int PlayerIndex = -1;


        private float _brakeValue;
        public float BrakeValue 
        {
            get { return _brakeValue; }
            set { SetField(ref _brakeValue, value, "BrakeValue"); }
        }

        private float _throttleValue;
        public float ThrottleValue
        {
            get { return _throttleValue; }
            set { SetField(ref _throttleValue, value, "ThrottleValue"); }
        }

        private float _steerValue;
        public float SteerValue
        {
            get { return _steerValue; }
            set { SetField(ref _steerValue, value, "SteerValue"); }
        }



        private void TelemetryUpdate(PacketCarTelemetryData packet)
        {
            CarTelemetryData = packet;
            PlayerIndex = packet.m_header.m_playerCarIndex;
            PlayerCarTelemetryData = packet.m_carTelemetryData[PlayerIndex];

            BrakeValue = PlayerCarTelemetryData.m_brake;
            ThrottleValue = PlayerCarTelemetryData.m_throttle;
            SteerValue = PlayerCarTelemetryData.m_steer;
        }

        // TODO
        // Load/Set Defaults of this Module
        private InputTelemetryViewModel() : base()
        {
            udpConnection.OnCarTelemetryDataReceive += TelemetryUpdate;
        }
    }
}
