using F1T.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace F1TMock.Mock
{
    public static class MockPacketSessionData
    {
        private static int MAX_MARSHAL_ZONES = 21;
        private static int MAX_WEATHER_FORECAST_SAMPLES = 56;
        private static MarshalZone[] GetMarshalZones(int length)
        {
            MarshalZone[] zones = new MarshalZone[MAX_MARSHAL_ZONES];

            for(int i = 0; i < length; i++)
            {
                MarshalZone data = new MarshalZone();
                data.m_zoneStart = i / length;
                data.m_zoneFlag = ZoneFlag.Green;
                zones[i] = data;
            }

            // Fill the rest of the array with dummy data
            for(int i = length; i < MAX_MARSHAL_ZONES; i++)
            {
                MarshalZone data = new MarshalZone();
                data.m_zoneStart = 0;
                data.m_zoneFlag = ZoneFlag.None;
                zones[i] = data;
            }

            return zones;
        }

        private static WeatherForecastSample[] GetWeatherForecastSamples(int length)
        {
            WeatherForecastSample[] weather = new WeatherForecastSample[MAX_WEATHER_FORECAST_SAMPLES];

            for (int i = 0; i < length; i++)
            {
                WeatherForecastSample data = new WeatherForecastSample();
                data.m_sessionType = SessionType.R;
                data.m_timeOffset = (byte)i;
                data.m_weather = Weather.Clear;
                data.m_trackTemperature = 20;
                data.m_trackTemperatureChange = Change.NoChange;
                data.m_rainPercentage = 20;
                weather[i] = data;
            }

            // Fill the rest of the array with dummy data
            for (int i = length; i < MAX_WEATHER_FORECAST_SAMPLES; i++)
            {
                WeatherForecastSample data = new WeatherForecastSample();
                data.m_sessionType = SessionType.Unknown;
                data.m_timeOffset = 0;
                data.m_weather = Weather.Clear;
                data.m_trackTemperature = 0;
                data.m_trackTemperatureChange = Change.NoChange;
                data.m_rainPercentage = 0;
                weather[i] = data;
            }

            return weather;
        }

        private static PacketSessionData GetRandomPacketSessionData()
        {
            PacketSessionData data = new PacketSessionData();
            data.m_header = MockPacketHeader.GetBytes(PacketType.Session);
            data.m_weather = Weather.Clear;
            data.m_trackTemperature = 20;
            data.m_airTemperature = 20;
            data.m_totalLaps = 20;
            data.m_trackLength = 4000;
            data.m_sessionType = SessionType.R;
            data.m_trackId = TrackID.Austria;
            data.m_formula = Formula.F1Modern;
            data.m_sessionTimeLeft = 500;
            data.m_sessionDuration = 4000;
            data.m_pitSpeedLimit = 20;
            data.m_gamePaused = 0;
            data.m_isSpectating = 0;
            data.m_spectatorCarIndex = 0;
            data.m_sliProNativeSupport = 0;
            data.m_numMarshalZones = 5;
            data.m_marshalZones = GetMarshalZones(data.m_numMarshalZones);
            data.m_safetyCarStatus = SafteyCarStatus.NoSafety;
            data.m_networkGame = NetworkGame.Online;
            data.m_numWeatherForecastSamples = 35;
            data.m_weatherForecastSamples = GetWeatherForecastSamples(data.m_numWeatherForecastSamples);
            data.m_forecastAccuracy = ForecastAccuracy.Perfect;
            data.m_aiDifficulty = 85;
            data.m_seasonLinkIdentifier = 0;
            data.m_weekendLinkIdentifier = 0;
            data.m_sessionLinkIdentifier = 0;
            data.m_pitStopWindowIdealLap = 5;
            data.m_pitStopWindowLatestLap = 7;
            data.m_pitStopRejoinPosition = 1;
            data.m_steeringAssist = AssistLevel.Off;
            data.m_brakingAssist = BrakingAssistLevel.Off;
            data.m_gearboxAssist = GearboxAssistLevel.Manual;
            data.m_pitAssist = AssistLevel.Off;
            data.m_pitReleaseAssist = AssistLevel.Off;
            data.m_ERSAssist = AssistLevel.Off;
            data.m_dynamicRacingLine = RacingLineAssistLevel.Off;
            data.m_dynamicRacingLineType = RacingLineType.ThreeD;
            return data;
        }

        public static byte[] GetBytes()
        {
            PacketSessionData packet = GetRandomPacketSessionData();
            int size = Marshal.SizeOf(packet);
            byte[] arr = new byte[size];

            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(packet, ptr, true);
            Marshal.Copy(ptr, arr, 0, size);
            Marshal.FreeHGlobal(ptr);
            return arr;
        }
    }
}
