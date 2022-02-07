using F1T.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace F1T.MVVM.ViewModels
{
    public class RadarViewModel : BaseModuleViewModel
    {

        private int _scale;
        public int Scale
        {
            get { return _scale; }
            set
            {
                SetField(ref _scale, value, "Scale");
            }
        }

        private float _carWidth;
        public float CarWidth
        {
            get { return _carWidth; }
            set
            {
                SetField(ref _carWidth, value, "CarWidth");
            }
        }

        private float _carHeight;
        public float CarHeight
        {
            get { return _carHeight; }
            set
            {
                SetField(ref _carHeight, value, "CarHeight");
            }
        }


        private float _playerCarLeft;
        public float PlayerCarLeft
        {
            get { return _playerCarLeft; }
            set
            {
                SetField(ref _playerCarLeft, value, "PlayerCarLeft");
            }
        }

        private float _playerCarTop;
        public float PlayerCarTop
        {
            get { return _playerCarTop; }
            set
            {
                SetField(ref _playerCarTop, value, "PlayerCarTop");
            }
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


        public PacketMotionData MotionData;
        public CarMotionData PlayerCarMotionData;
        public int PlayerIndex = -1;

        private void RadarUpdate(PacketMotionData packet)
        {
            MotionData = packet;
            PlayerIndex = packet.m_header.m_playerCarIndex;
            PlayerCarMotionData = packet.m_carMotionData[packet.m_header.m_playerCarIndex];
        }

        // TODO
        // Load/Set Defaults of this Module
        private RadarViewModel() : base()
        {
            Scale = 20;

            CarWidth = 1.9f * Scale;
            CarHeight = 5.35f * Scale;

            Width = 500;
            Height = 500;

            PlayerCarLeft = (int)Width / 2 - CarWidth / 2;
            PlayerCarTop = (int)Height / 2 - CarHeight / 2;

            DangerRadius = (int)Math.Round(2f * Scale);
            WarningRadius = (int)Math.Round(3.5f * Scale);

            udpConnection.OnMotionDataReceive += RadarUpdate;
        }
    }
}
