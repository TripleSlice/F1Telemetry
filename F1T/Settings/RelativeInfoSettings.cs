using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1T.Settings
{
    [Serializable]
    public class RelativeInfoSettings : BaseSettings
    {
        protected override string Filename => "RelativeInfoSettings.config";
        public RelativeInfoSettings() : base()
        {
            // OVERRIDE DEFAULT SETTINGS
            Height = 170;
            FramesPerSecond = 10;

            // MODULE DEFAULT SETTINGS
            TyreWearVisible = false;
            TyreAgeVisible = true;
            FastestLapVisible = true;
            LastLapVisible = true;
        }


        private bool _tyreWearVisibile;
        public bool TyreWearVisible
        {
            get { return _tyreWearVisibile; }
            set { SetField(ref _tyreWearVisibile, value, "TyreWearVisible"); }
        }

        private bool _tyreAgeVisibile;
        public bool TyreAgeVisible
        {
            get { return _tyreAgeVisibile; }
            set { SetField(ref _tyreAgeVisibile, value, "TyreAgeVisible"); }
        }

        private bool _lastLapVisible;
        public bool LastLapVisible
        {
            get { return _lastLapVisible; }
            set { SetField(ref _lastLapVisible, value, "LastLapVisible"); }
        }

        private bool _fastestLapVisible;
        public bool FastestLapVisible
        {
            get { return _fastestLapVisible; }
            set { SetField(ref _fastestLapVisible, value, "FastestLapVisible"); }
        }
    }
}
