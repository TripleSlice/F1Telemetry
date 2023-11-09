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
            MotionDataSaveLocation = FilesPath;
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

        private bool _saveMotionaData;
        public bool SaveMotionData
        {
            get { return _saveMotionaData; }
            set { SetField(ref _saveMotionaData, value, "SaveMotionData"); }
        }

        private string _motionDataSaveLocation;
        public string MotionDataSaveLocation
        {
            get { return _motionDataSaveLocation; }
            set { SetField(ref _motionDataSaveLocation, value, "MotionDataSaveLocation"); }
        }

    }
}
