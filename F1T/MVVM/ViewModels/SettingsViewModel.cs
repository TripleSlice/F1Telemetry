using F1T.Settings;
using F1T.Structs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            udpConnection.OnMotionDataReceive += OnMotionDataReceive;
            udpConnection.OnSessionDataReceive += OnPacketSessionData;
            udpConnection.OnLapDataReceive += OnPacketLapData;
            udpConnection.OnCarStatusDataReceive += OnPacketCarStatusData;
            udpConnection.OnCarTelemetryDataReceive += OnPacketCarTelemetryData;
        }
        // === END OF MODULE SETUP ===

        private ulong ResultsCurrentSessionId = 0;

        private async void OnFinalClassificationDataReceive(PacketFinalClassificationData packet)
        {
            if (Settings.SaveFinalClassification && ResultsCurrentSessionId != packet.m_header.m_sessionUID)
            {
                // Save the packet
                var json = JsonConvert.SerializeObject(packet, Formatting.Indented, new ByteArrayConverter());

                Directory.CreateDirectory(Settings.FinalClassificationSaveLocation);
                using (StreamWriter outputFile = new StreamWriter(Path.Combine(Settings.FinalClassificationSaveLocation, "result-" + DateTime.Now.ToString("yyyy-MM-dd-HHmmssfff") + ".json"), true))
                {
                    outputFile.WriteLine(json);
                }

                ResultsCurrentSessionId = packet.m_header.m_sessionUID;   
            }
        }

        private ulong ReplayCurrentSessionId = 0;
        private string ReplaySavePath = null;
        private DateTime ReplayLastWriteTime = DateTime.Now;

        private RaceReplayFrame CurrentFrame = new RaceReplayFrame();

        private async void OnPacketSessionData(PacketSessionData packet)
        {
            if (Settings.SaveMotionData)
            {
                for(int i = 0; i < packet.m_marshalZones.Length; i++)
                {
                    CurrentFrame.ts = packet.m_safetyCarStatus;
                }   
            }
        }

        private async void OnPacketCarTelemetryData(PacketCarTelemetryData packet)
        {
            if (Settings.SaveMotionData)
            {
                for (int i = 0; i < packet.m_carTelemetryData.Length; i++)
                {
                    CurrentFrame.data[i].drs = packet.m_carTelemetryData[i].m_drs;
                }
            }
        }

        private async void OnPacketCarStatusData(PacketCarStatusData packet)
        {
            if (Settings.SaveMotionData)
            {
                for (int i = 0; i < packet.m_carStatusData.Length; i++)
                {
                    CurrentFrame.data[i].t = packet.m_carStatusData[i].m_visualTyreCompound;
                    CurrentFrame.data[i].ers = Math.Round((decimal)(packet.m_carStatusData[i].m_ersStoreEnergy / 4000000.0f), 2, MidpointRounding.AwayFromZero);
                    CurrentFrame.data[i].f = Math.Round((decimal)packet.m_carStatusData[i].m_fuelRemainingLaps, 2, MidpointRounding.AwayFromZero);
                }
            }
        }

        private async void OnPacketLapData(PacketLapData packet)
        {
            if (Settings.SaveMotionData)
            {
                for (int i = 0; i < packet.m_lapData.Length; i++)
                {
                    CurrentFrame.data[i].pos = packet.m_lapData[i].m_carPosition;
                    CurrentFrame.data[i].ll = packet.m_lapData[i].m_lastLapTimeInMS;
                    CurrentFrame.data[i].cl = packet.m_lapData[i].m_currentLapTimeInMS;
                    CurrentFrame.data[i].lap = packet.m_lapData[i].m_currentLapNum;
                    CurrentFrame.data[i].ps = packet.m_lapData[i].m_pitStatus;
                    CurrentFrame.data[i].np = packet.m_lapData[i].m_numPitStops;
       
                    CurrentFrame.data[i].s = packet.m_lapData[i].m_resultStatus;
   
                }
            }
        }

        private async void OnMotionDataReceive(PacketMotionData packet)
        {
            if (Settings.SaveMotionData)
            {
                // If we are in a new session, we must create a new file to write to
                if(ReplayCurrentSessionId != packet.m_header.m_sessionUID)
                {
                    ReplayCurrentSessionId = packet.m_header.m_sessionUID;
                    ReplaySavePath = Path.Combine(Settings.MotionDataSaveLocation, "replay-" + DateTime.Now.ToString("yyyy-MM-dd-HHmmssfff") + ".json");

                    // dump the header
                    var json = JsonConvert.SerializeObject(packet.m_header, Formatting.None, new ByteArrayConverter());
                    using (StreamWriter outputFile = new StreamWriter(ReplaySavePath, true))
                    {
                        outputFile.WriteLine("{\"header\":" + json + ",\"frames\":[");
                    }
                }

                // Update the CurrentFrame
                
                for(int i = 0; i < packet.m_carMotionData.Length; i++)
                {
                    CurrentFrame.data[i].x = Math.Round((decimal)packet.m_carMotionData[i].m_worldPositionX, 2, MidpointRounding.AwayFromZero);
                    CurrentFrame.data[i].y = Math.Round((decimal)packet.m_carMotionData[i].m_worldPositionZ, 2, MidpointRounding.AwayFromZero); // z is actually y
                    CurrentFrame.data[i].yaw = Math.Round((decimal)packet.m_carMotionData[i].m_yaw, 2, MidpointRounding.AwayFromZero);
                }

                var difference = DateTime.Now.Subtract(ReplayLastWriteTime);
                if (difference.TotalMilliseconds > 100 && CurrentFrame != null)
                {
                    ReplayLastWriteTime = DateTime.Now;

                    // Save the packet
                    // TODO Check if another Formatting.None would be more space efficient (it most defintley would be...)
                    var json = JsonConvert.SerializeObject(CurrentFrame, Formatting.None, new ByteArrayConverter());
                    using (StreamWriter outputFile = new StreamWriter(ReplaySavePath, true))
                    {
                        outputFile.WriteLine(json + ",");
                    }
                }
            }
        }
    }

    class RaceReplayFrame
    {
        public SafteyCarStatus ts;
        public RacerData[] data;

        public RaceReplayFrame()
        {
            this.ts = SafteyCarStatus.NoSafety;
            // max 22 racers
            this.data = new RacerData[22];
            for(int i = 0; i < this.data.Length; i++)
            {
                this.data[i] = new RacerData();
            }
        }

    }

    class RacerData
    {
        public int pos; //position
        public ResultStatus s; //status

        public decimal x;
        public decimal y;
        public decimal yaw;

        public uint ll; // last lap ms
        public uint cl; // current lap ms
        public byte lap;

        public PitStatus ps; // pit status
        public byte np; // number of pit stops

        public VisualTyreCompound t; // visual tyre compound
        public DRS drs;
        public decimal ers; // ers
        public decimal f; // fuel remaining (+1 lap etc)

        public RacerData() {}
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
