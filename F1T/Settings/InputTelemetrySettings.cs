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
        }
    }
}
