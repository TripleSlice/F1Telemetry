using F1T.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace F1T.Settings
{
    [Serializable]
    public abstract class BaseSettings : SaveableSettings
    {

        // SETTINGS WHICH ARE COMMON ACROSS ALL MODULES
        public BaseSettings()
        {
            // DEFAULT SETTINGS INCASE WE MUST GENERATE A CONFIG
            Left = 0;
            Top = 0;
            Height = Double.NaN;
            Width = Double.NaN;
            OpacitySliderValue = 50;
            FramesPerSecond = 20;
            AutoToggled = false;
            ScaleSliderValue = 100;
            Locked = false;
        }


        private int _left;
        public int Left
        {
            get { return _left; }
            set { SetField(ref _left, value, "Left"); }
        }

        private int _top;
        public int Top
        {
            get { return _top; }
            set { SetField(ref _top, value, "Top"); }
        }


        private double _width;
        public double Width
        {
            get { return _width; }
            set { SetField(ref _width, value, "Width"); }
        }

        private double _height;
        public double Height
        {
            get { return _height; }
            set { SetField(ref _height, value, "Height"); }
        }


        private double _scaledWidth;
        public double ScaledWidth
        {
            get { return _scaledWidth; }
            set { SetField(ref _scaledWidth, value, "ScaledWidth"); }
        }

        private double _scaledHeight;
        public double ScaledHeight
        {
            get { return _scaledHeight; }
            set { SetField(ref _scaledHeight, value, "ScaledHeight"); }
        }


        private int _framesPerSecond;
        public int FramesPerSecond
        {
            get { return _framesPerSecond; }
            set
            {
                SetField(ref _framesPerSecond, value, "FramesPerSecond");
                Frequency = 1000 / FramesPerSecond;
            }
        }

        private int _frequency;
        public int Frequency
        {
            get { return _frequency; }
            set { SetField(ref _frequency, value, "Frequency"); }
        }

        private int _opacitySliderValue;
        public int OpacitySliderValue
        {
            get { return _opacitySliderValue; }
            set
            {
                SetField(ref _opacitySliderValue, value, "OpacitySliderValue");
                Opacity = OpacitySliderValue / 100f;
            }
        }

        private float _opacity;
        public float Opacity
        {
            get { return _opacity; }
            set { SetField(ref _opacity, value, "Opacity"); }
        }

        private bool _locked;
        public bool Locked
        {
            get { return _locked; }
            set { SetField(ref _locked, value, "Locked"); }
        }



        private int _scaleSliderValue;
        public int ScaleSliderValue
        {
            get { return _scaleSliderValue; }
            set
            {
                SetField(ref _scaleSliderValue, value, "ScaleSliderValue");
                ScaledHeight = Height * (ScaleSliderValue / 100f);
                ScaledWidth = Width * (ScaleSliderValue / 100f);
            }
        }
    }
}
