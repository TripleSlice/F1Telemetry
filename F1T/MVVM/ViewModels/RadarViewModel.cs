using F1T.MVVM.Views.Radar;
using F1T.Settings;
using F1T.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace F1T.MVVM.ViewModels
{
    /// <summary>
    /// ViewModel for the RadarOverlayView and RadarSettingView
    /// </summary>
    public class RadarViewModel : BaseModuleViewModel<RadarSettings>
    {

        // === BEGINING OF MODULE SETUP ===
        // === Singleton Instance with Thread Saftey ===
        private static RadarViewModel _instance = null;
        private static object _singletonLock = new object();
        public static RadarViewModel GetInstance()
        {
            lock (_singletonLock)
            {
                if (_instance == null) { _instance = new RadarViewModel(); }
                return _instance;
            }
        }
        // Settings
        private RadarSettings _settings = new RadarSettings().Read<RadarSettings>();
        public override RadarSettings Settings { get => _settings; }
        // === END OF MODULE SETUP ===

        private int _scale;
        public int Scale
        {
            get { return _scale; }
            set { SetField(ref _scale, value, "Scale"); }
        }

        private float _carWidth;
        public float CarWidth
        {
            get { return _carWidth; }
            set { SetField(ref _carWidth, value, "CarWidth"); }
        }

        private float _carHeight;
        public float CarHeight
        {
            get { return _carHeight; }
            set { SetField(ref _carHeight, value, "CarHeight"); }
        }


        private float _playerCarLeft;
        public float PlayerCarLeft
        {
            get { return _playerCarLeft; }
            set { SetField(ref _playerCarLeft, value, "PlayerCarLeft"); }
        }

        private float _playerCarTop;
        public float PlayerCarTop
        {
            get { return _playerCarTop; }
            set { SetField(ref _playerCarTop, value, "PlayerCarTop"); }
        }

        private int _dangerRadius;
        public int DangerRadius
        {
            get { return _dangerRadius; }
            set { SetField(ref _dangerRadius, value, "DangerRadius"); }
        }

        private int _warningRadius;
        public int WarningRadius
        {
            get { return _warningRadius; }
            set { SetField(ref _warningRadius, value, "WarningRadius"); }
        }


       

        public PacketMotionData MotionData;
        public PacketLapData LapData;
        public CarMotionData PlayerCarMotionData;
        public int PlayerIndex = -1;

        private void RadarUpdate(PacketMotionData packet)
        {
            MotionData = packet;
            PlayerIndex = packet.m_header.m_playerCarIndex;
            PlayerCarMotionData = packet.m_carMotionData[packet.m_header.m_playerCarIndex];
        }

        private void LapDataUpdate(PacketLapData packet)
        {
            LapData = packet;
        }


        private RadarViewModel() : base()
        {
            Scale = 20;

            CarWidth = 1.9f * Scale;
            // https://github.com/ryanabaker/F1T/issues/18
            CarHeight = 5.35f * Scale;

            PlayerCarLeft = (int)Settings.Width / 2 - CarWidth / 2;
            PlayerCarTop = (int)Settings.Height / 2 - CarHeight / 2;

            DangerRadius = (int)Math.Round(2f * Scale);
            WarningRadius = (int)Math.Round(3.5f * Scale);

            udpConnection.OnMotionDataReceive += RadarUpdate;
            udpConnection.OnLapDataReceive += LapDataUpdate;
        }
    }
}
