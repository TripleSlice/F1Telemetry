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

        public int ViewWidth = 500;
        public int ViewHeight = 500;


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

        private RadarViewModel() : base()
        {
            Scale = 20;

            CarWidth = 1.9f * Scale;
            CarHeight = 5.35f * Scale;

            PlayerCarLeft = ViewWidth / 2 - CarWidth / 2;
            PlayerCarTop = ViewHeight / 2 - CarHeight / 2;

            DangerRadius = (int)Math.Round(2f * Scale);
            WarningRadius = (int)Math.Round(3.5f * Scale);

            // TODO
            // Set Defaults...
            // Regarding this module specifically....
        }
    }
}
