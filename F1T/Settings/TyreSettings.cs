using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1T.Settings
{
    [Serializable]
    public class TyreSettings : BaseSettings
    {
        protected override string Filename => "TyreSettings.config";
        public TyreSettings() : base()
        {
            // OVERRIDE DEFAULT SETTINGS
            Height = 170;
            FramesPerSecond = 10;

            // MODULE DEFAULT SETTINGS
            TyreWearVisible = false;
            TyreAgeVisible = true;
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

    }
}
