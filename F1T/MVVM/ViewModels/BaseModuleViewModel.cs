using F1T.Core;
using F1T.MVVM.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace F1T.MVVM.ViewModels
{
    public abstract class BaseModuleViewModel : ObservableObject
    {

        protected UDPConnection udpConnection = UDPConnection.GetInstance();

        public BaseModuleViewModel()
        {
            Left = 0;
            Top = 0;

            Height = Double.NaN;
            Width = Double.NaN;

            OpacitySliderValue = 50;

            FramesPerSecond = 2;
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




        private bool _overlayVisible;
        public bool OverlayVisible
        {
            get { return _overlayVisible; }
            set { SetField(ref _overlayVisible, value, "OverlayVisible"); }
        }

        private bool _toggled;
        public bool Toggled
        {
            get { return _toggled; }
            set { SetField(ref _toggled, value, "Toggled"); }
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
            set
            {
                SetField(ref _opacity, value, "Opacity");
            }
        }

        private int _framesPerSecond;
        public int FramesPerSecond
        {
            get { return _framesPerSecond; }
            set {
                SetField(ref _framesPerSecond, value, "FramesPerSecond");
                Frequency = 1000 / FramesPerSecond;
            }
        }

        public int Frequency;

        // TODO
        // As we change the scale slider
        // Change Width if its there
        // Change Height if its there
        private int _scaleSliderValue;
        public int ScaleSliderValue
        {
            get { return _scaleSliderValue; }
            set { SetField(ref _scaleSliderValue, value, "ScaleSliderValue"); }
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
            set { SetField(ref _height, value, "Height");  }
        }
    }
}
