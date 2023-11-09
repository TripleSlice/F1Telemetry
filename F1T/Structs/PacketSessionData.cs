using System.Runtime.InteropServices;

namespace F1T.Structs
{
    public enum ZoneFlag : sbyte
    {
        Invalid = -1,
        None,
        Green,
        Blue,
        Yellow,
        Red
    }

    public enum SessionType : byte
    {
        Unknown,
        P1,
        P2,
        P3,
        ShortP,
        Q1,
        Q2,
        Q3,
        ShortQ,
        OSQ,
        R,
        R2,
        TimeTrial
    }

    public enum Weather : byte
    {
        Clear,
        LightCloud,
        Overcast,
        LightRain,
        HeavyRain,
        Storm
    }

    public enum Change : sbyte
    {
        Up,
        Down,
        NoChange
    }

    public enum TrackID : sbyte
    {
        Unknown = -1,
        Melbourne,
        PaulRicard,
        Shanghai,
        Bahrain,
        Catalunya,
        Monaco,
        Montreal,
        Silverstone,
        Hockenheim,
        Hungaroring,
        Spa,
        Monza,
        Singapore,
        Suzuka,
        AbuDhabi,
        Texas,
        Brazil,
        Austria,
        Sochi,
        Mexico,
        Azerbaijan,
        SakhirShort,
        SilverstoneShort,
        TexasShort,
        SuzukaShort,
        Hanoi,
        Zandvoort,
        Imola,
        Portimão,
        Jeddah,
        Miami,
        LasVegas,
        Qatar
    }

    public enum Formula : byte
    {
        F1Modern,
        F1Classic,
        F2,
        F1Generic
    }

    public enum SafteyCarStatus : byte
    {
        NoSafety,
        Full,
        Virtual,
        FormationLap
    }

    public enum NetworkGame : byte
    {
        Offline,
        Online
    }

    public enum ForecastAccuracy : byte
    {
        Perfect,
        Approximate
    }

    public enum AssistLevel : byte
    {
        Off,
        On
    }

    public enum BrakingAssistLevel : byte
    {
        Off,
        Low,
        Medium,
        High
    }

    public enum GearboxAssistLevel : byte
    {
        Manual,
        ManualAndSuggested,
        Auto
    }

    public enum RacingLineAssistLevel : byte
    {
        Off,
        CornersOnly,
        Full
    }

    public enum RacingLineType : byte
    {
        TwoD,
        ThreeD
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct MarshalZone
    {
        public float m_zoneStart;   // Fraction (0..1) of way through the lap the marshal zone starts
        public ZoneFlag m_zoneFlag;    // -1 = invalid/unknown, 0 = none, 1 = green, 2 = blue, 3 = yellow, 4 = red
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct WeatherForecastSample
    {
        public SessionType m_sessionType;              // 0 = unknown, 1 = P1, 2 = P2, 3 = P3, 4 = Short P, 5 = Q1
                                                // 6 = Q2, 7 = Q3, 8 = Short Q, 9 = OSQ, 10 = R, 11 = R2
                                                // 12 = R3, 13 = Time Trial
        public byte m_timeOffset;               // Time in minutes the forecast is for
        public Weather m_weather;                  // Weather - 0 = clear, 1 = light cloud, 2 = overcast
                                                // 3 = light rain, 4 = heavy rain, 5 = storm
        public sbyte m_trackTemperature;         // Track temp. in degrees Celsius
        public Change m_trackTemperatureChange;   // Track temp. change – 0 = up, 1 = down, 2 = no change
        public sbyte m_airTemperature;           // Air temp. in degrees celsius
        public Change m_airTemperatureChange;     // Air temp. change – 0 = up, 1 = down, 2 = no change
        public byte m_rainPercentage;           // Rain percentage (0-100)
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PacketSessionData
    {
        public PacketHeader m_header;                  // Header

        public Weather m_weather;                // Weather - 0 = clear, 1 = light cloud, 2 = overcast
                                              // 3 = light rain, 4 = heavy rain, 5 = storm
        public sbyte m_trackTemperature;        // Track temp. in degrees celsius
        public sbyte m_airTemperature;          // Air temp. in degrees celsius
        public byte m_totalLaps;              // Total number of laps in this race
        public ushort m_trackLength;               // Track length in metres
        public SessionType m_sessionType;            // 0 = unknown, 1 = P1, 2 = P2, 3 = P3, 4 = Short P
                                              // 5 = Q1, 6 = Q2, 7 = Q3, 8 = Short Q, 9 = OSQ
                                              // 10 = R, 11 = R2, 12 = R3, 13 = Time Trial
        public TrackID m_trackId;                 // -1 for unknown, see appendix
        public Formula m_formula;                    // Formula, 0 = F1 Modern, 1 = F1 Classic, 2 = F2,
                                                 // 3 = F1 Generic, 4 = Beta, 5 = Supercars 6 = Esports, 7 = F2 2021
        public ushort m_sessionTimeLeft;       // Time left in session in seconds
        public ushort m_sessionDuration;       // Session duration in seconds
        public byte m_pitSpeedLimit;          // Pit speed limit in kilometres per hour
        public byte m_gamePaused;                // Whether the game is paused - network game only
        public byte m_isSpectating;           // Whether the player is spectating
        public byte m_spectatorCarIndex;      // Index of the car being spectated
        public byte m_sliProNativeSupport;    // SLI Pro support, 0 = inactive, 1 = active
        public byte m_numMarshalZones;            // Number of marshal zones to follow
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 21)]
        public MarshalZone[] m_marshalZones;             // List of marshal zones – max 21
        public SafteyCarStatus m_safetyCarStatus;           // 0 = no safety car, 1 = full
                                                 // 2 = virtual, 3 = formation lap
        public NetworkGame m_networkGame;               // 0 = offline, 1 = online
        public byte m_numWeatherForecastSamples; // Number of weather samples to follow
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 56)]
        public WeatherForecastSample[] m_weatherForecastSamples;   // Array of weather forecast samples
        public ForecastAccuracy m_forecastAccuracy;          // 0 = Perfect, 1 = Approximate
        public byte m_aiDifficulty;              // AI Difficulty rating – 0-110
        public uint m_seasonLinkIdentifier;      // Identifier for season - persists across saves
        public uint m_weekendLinkIdentifier;     // Identifier for weekend - persists across saves
        public uint m_sessionLinkIdentifier;     // Identifier for session - persists across saves
        public byte m_pitStopWindowIdealLap;     // Ideal lap to pit on for current strategy (player)
        public byte m_pitStopWindowLatestLap;    // Latest lap to pit on for current strategy (player)
        public byte m_pitStopRejoinPosition;     // Predicted position to rejoin at (player)
        public AssistLevel m_steeringAssist;            // 0 = off, 1 = on
        public BrakingAssistLevel m_brakingAssist;             // 0 = off, 1 = low, 2 = medium, 3 = high
        public GearboxAssistLevel m_gearboxAssist;             // 1 = manual, 2 = manual & suggested gear, 3 = auto
        public AssistLevel m_pitAssist;                 // 0 = off, 1 = on
        public AssistLevel m_pitReleaseAssist;          // 0 = off, 1 = on
        public AssistLevel m_ERSAssist;                 // 0 = off, 1 = on
        public AssistLevel m_DRSAssist;                 // 0 = off, 1 = on
        public RacingLineAssistLevel m_dynamicRacingLine;         // 0 = off, 1 = corners only, 2 = full
        public RacingLineType m_dynamicRacingLineType;     // 0 = 2D, 1 = 3D
        public byte m_gameMode;                  // Game mode id - see appendix
        public byte m_ruleSet;                   // Ruleset - see appendix
        public uint m_timeOfDay;                 // Local time of day - minutes since midnight
        public byte m_sessionLength;             // 0 = None, 2 = Very Short, 3 = Short, 4 = Medium 5 = Medium Long, 6 = Long, 7 = Full
    }
}
