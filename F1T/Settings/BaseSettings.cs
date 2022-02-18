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
    public abstract class BaseSettings : ObservableObject
    {
        protected static string SettingsPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\F1T\\config\\";
        protected abstract string Filename { get; }

        /// <summary>
        /// Serialize (save) this as type <see cref="{T}"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void Save<T>()
        {
            Directory.CreateDirectory(SettingsPath);

            using (StreamWriter sw = new StreamWriter(SettingsPath + Filename))
            {
                XmlSerializer xmls = new XmlSerializer(typeof(T));
                xmls.Serialize(sw, this);
            }
        }

        /// <summary>
        /// Deserialize (read) this as type <see cref="{T}"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns><see cref="{T}"/></returns>
        public T Read<T>()
        {
            // Try and read it
            // If the file does not exist,
            // OR has an error in it
            // Regen the file, and then Read() it
            try
            {
                using (StreamReader sw = new StreamReader(SettingsPath + Filename))
                {
                    XmlSerializer xmls = new XmlSerializer(typeof(T));
                    return (T)xmls.Deserialize(sw);
                }
            }
            catch (Exception)
            {
                Save<T>();
                return Read<T>();
            }

        }

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
        }


        private bool _toggled;
        /// <summary>
        /// True if Module switch is toggled, False otherwise
        /// </summary>
        public bool Toggled
        {
            get { return _toggled; }
            set { SetField(ref _toggled, value, "Toggled"); }
        }

        private bool _autoToggled;
        public bool AutoToggled
        {
            get { return _autoToggled; }
            set { SetField(ref _autoToggled, value, "AutoToggled"); }
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
