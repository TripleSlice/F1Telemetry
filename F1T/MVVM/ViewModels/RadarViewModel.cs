using System;
using System.Collections.Generic;
using System.Linq;
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

        private double _carWidth;
        public double CarWidth
        {
            get { return _carWidth; }
            set
            {
                SetField(ref _carWidth, value, "CarWidth");
            }
        }

        private double _carHeight;
        public double CarHeight
        {
            get { return _carHeight; }
            set
            {
                SetField(ref _carHeight, value, "CarHeight");
            }
        }


        private double _playerCarLeft;
        public double PlayerCarLeft
        {
            get { return _playerCarLeft; }
            set
            {
                SetField(ref _playerCarLeft, value, "PlayerCarLeft");
            }
        }

        private double _playerCarTop;
        public double PlayerCarTop
        {
            get { return _playerCarTop; }
            set
            {
                SetField(ref _playerCarTop, value, "PlayerCarTop");
            }
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

            CarWidth = 1.9 * Scale;
            CarHeight = 5.3 * Scale;

            PlayerCarLeft = ViewWidth / 2 - CarWidth / 2;
            PlayerCarTop = ViewHeight / 2 - CarHeight / 2;

            // TODO
            // Set Defaults...
            // Regarding this module specifically....
        }
    }
}
