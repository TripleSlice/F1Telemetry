using F1T.Settings;
using F1T.Structs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1T.MVVM.ViewModels
{
    public class SettingsViewModel : BaseModuleViewModel<SettingsSettings>
    {
        // === BEGINING OF MODULE SETUP ===
        // === Singleton Instance with Thread Saftey ===
        private static SettingsViewModel _instance = null;
        private static object _singletonLock = new object();
        public static SettingsViewModel GetInstance()
        {
            lock (_singletonLock)
            {
                if (_instance == null) { _instance = new SettingsViewModel(); }
                return _instance;
            }
        }
        // Settings
        private SettingsSettings _settings = new SettingsSettings().Read<SettingsSettings>();
        public override SettingsSettings Settings { get => _settings; }

        private SettingsViewModel() : base()
        {
            udpConnection.OnFinalClassificationDataReceive += OnFinalClassificationDataReceive;
        }
        // === END OF MODULE SETUP ===

        private async void OnFinalClassificationDataReceive(PacketFinalClassificationData packet)
        {
            if (Settings.SaveFinalClassification)
            {
                // Save the packet
                var json = JsonConvert.SerializeObject(packet, Formatting.Indented, new ByteArrayConverter());

                Directory.CreateDirectory(Settings.FinalClassificationSaveLocation);
                using (StreamWriter outputFile = new StreamWriter(Path.Combine(Settings.FinalClassificationSaveLocation, "race-" + DateTime.Now.ToString("yyyy-MM-dd-HHmmssfff") + ".json"), true))
                {
                    outputFile.WriteLine(json);
                }
            }
        }
    }

    // Converts the packets byte[] to int[]
    class ByteArrayConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(byte[]));
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var bytes = value as byte[];
            int[] bytesAsInts = bytes.Select(x => (int)x).ToArray();
            string result = "[" + string.Join(",", bytesAsInts) + "]";
            writer.WriteRawValue(result);
        }

        public override bool CanRead
        {
            get { return false; }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
