using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1T.Settings
{
    [Serializable]
    public class RadarSettings : BaseSettings
    {
        protected override string Filename => "RadarSettings.config";
        public RadarSettings() : base()
        {
            // OVERRIDE DEFAULT SETTINGS
            Width = 500;
            Height = 500;
            FramesPerSecond = 60;
        }

    }
}
