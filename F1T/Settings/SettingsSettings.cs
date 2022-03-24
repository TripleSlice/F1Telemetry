using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1T.Settings
{
    public class SettingsSettings : SaveableSettings
    {
        protected override string Filename => "SettingSettings.config";
        private static string FilesPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\F1T\\files\\";


        public SettingsSettings() : base()
        {
            SaveFinalClassification = false;
            FinalClassificationSaveLocation = FilesPath;
        }


        private bool _saveFinalClassification;
        public bool SaveFinalClassification
        {
            get { return _saveFinalClassification; }
            set { SetField(ref _saveFinalClassification, value, "SaveFinalClassification"); }
        }

        private string _finalClassifcationSaveLocation;
        public string FinalClassificationSaveLocation
        {
            get { return _finalClassifcationSaveLocation; }
            set { SetField(ref _finalClassifcationSaveLocation, value, "FinalClassificationSaveLocation"); }
        }
    }
}
