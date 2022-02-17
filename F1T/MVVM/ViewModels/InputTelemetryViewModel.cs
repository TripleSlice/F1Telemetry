using F1T.Settings;
using F1T.Structs;
using System;

namespace F1T.MVVM.ViewModels
{
    /// <summary>
    /// ViewModel for the InputTelemetryOverlayView and InputTelemetrySettingView
    /// </summary>
    public class InputTelemetryViewModel : BaseModuleViewModel<InputTelemetrySettings>
    {
        // === BEGINING OF MODULE SETUP ===
        // Singleton Instance with Thread Safety
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
        // Settings
        private InputTelemetrySettings _settings = new InputTelemetrySettings().Read<InputTelemetrySettings>();
        public override InputTelemetrySettings Settings { get => _settings; }
        // === END OF MODULE SETUP ===

        // Required packets for module to function
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

        private string _currentGearValue;
        public string CurrentGearValue
        {
            get { return _currentGearValue; }
            set { SetField(ref _currentGearValue, value, "CurrentGearValue"); }
        }

        private string _suggestedGearValue;
        public string SuggestedGearValue
        {
            get { return _suggestedGearValue; }
            set { SetField(ref _suggestedGearValue, value, "SuggestedGearValue"); }
        }

        private string _arrowValue;
        public string ArrowValue
        {
            get { return _arrowValue; }
            set { SetField(ref _arrowValue, value, "ArrowValue"); }
        }

        private void TelemetryUpdate(PacketCarTelemetryData packet)
        {
            CarTelemetryData = packet;
            PlayerIndex = packet.m_header.m_playerCarIndex;
            PlayerCarTelemetryData = packet.m_carTelemetryData[PlayerIndex];

            sbyte currentGear = PlayerCarTelemetryData.m_gear;
            sbyte suggestedGear = packet.m_suggestedGear;

            BrakeValue = PlayerCarTelemetryData.m_brake;
            ThrottleValue = PlayerCarTelemetryData.m_throttle;
            SteerValue = PlayerCarTelemetryData.m_steer;
            CurrentGearValue = currentGear.ToString();
            ArrowValue = suggestedGear > currentGear ? "5" : "6";
            ArrowValue = suggestedGear != currentGear ? ArrowValue : "";
            SuggestedGearValue = suggestedGear != currentGear ? suggestedGear.ToString() : "";
            SuggestedGearValue = suggestedGear == 0 || suggestedGear == -1 ? "" : SuggestedGearValue;
            ArrowValue = suggestedGear == 0 || suggestedGear == -1 ? "" : ArrowValue;
        }

        private InputTelemetryViewModel() : base()
        {
            udpConnection.OnCarTelemetryDataReceive += TelemetryUpdate;
        }
    }
}
