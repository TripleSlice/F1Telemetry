using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1T.Settings
{
    [Serializable]
    public class InputTelemetrySettings : BaseSettings
    {
        protected override string Filename => "InputTelemetrySettings.config";
        public InputTelemetrySettings() : base()
        {
            // OVERRIDE DEFAULT SETTINGS
            Height = 125d;
            FramesPerSecond = 35;


            // Safe defaults to use if they are not overridden
            Setting1 = "This is the default";
            Setting2 = "This is another default";
        }


        private string _setting1;
        public string Setting1
        {
            get { return _setting1; }
            set { SetField(ref _setting1, value, "Setting1"); }
        }


        private string _setting2;
        public string Setting2
        {
            get { return _setting2; }
            set { SetField(ref _setting2, value, "Setting2"); }
        }

    }
}
