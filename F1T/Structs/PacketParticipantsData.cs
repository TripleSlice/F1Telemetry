using System.Runtime.InteropServices;

namespace F1T.Structs
{
    public enum DriverID : byte
    {
        CarlosSainz = 0,
        DaniilKvyat = 1,
        DanielRicciardo = 2,
        FernandoAlonso = 3,
        FelipeMassa = 4,
        KimiRäikkönen = 6,
        LewisHamilton = 7,
        MaxVerstappen = 9,
        NicoHulkenburg = 10,
        KevinMagnussen = 11,
        RomainGrosjean = 12,
        SebastianVettel = 13,
        SergioPerez = 14,
        ValtteriBottas = 15,
        EstebanOcon = 17,
        LanceStroll = 19,
        ArronBarnes = 20,
        MartinGiles = 21,
        AlexMurray = 22,
        LucasRoth = 23,
        IgorCorreia = 24,
        SophieLevasseur = 25,
        JonasSchiffer = 26,
        AlainForest = 27,
        JayLetourneau = 28,
        EstoSaari = 29,
        YasarAtiyeh = 30,
        CallistoCalabresi = 31,
        NaotaIzum = 32,
        HowardClarke = 33,
        WilheimKaufmann = 34,
        MarieLaursen = 35,
        FlavioNieves = 36,
        PeterBelousov = 37,
        KlimekMichalski = 38,
        SantiagoMoreno = 39,
        BenjaminCoppens = 40,
        NoahVisser = 41,
        GertWaldmuller = 42,
        JulianQuesada = 43,
        DanielJones = 44,
        ArtemMarkelov = 45,
        TadasukeMakino = 46,
        SeanGelael = 47,
        NyckDeVries = 48,
        JackAitken = 49,
        GeorgeRussell = 50,
        MaximilianGünther = 51,
        NireiFukuzumi = 52,
        LucaGhiotto = 53,
        LandoNorris = 54,
        SérgioSetteCâmara = 55,
        LouisDelétraz = 56,
        AntonioFuoco = 57,
        CharlesLeclerc = 58,
        PierreGasly = 59,
        AlexanderAlbon = 62,
        NicholasLatifi = 63,
        DorianBoccolacci = 64,
        NikoKari = 65,
        RobertoMerhi = 66,
        ArjunMaini = 67,
        AlessioLorandi = 68,
        RubenMeijer = 69,
        RashidNair = 70,
        JackTremblay = 71,
        DevonButler = 72,
        LukasWeber = 73,
        AntonioGiovinazzi = 74,
        RobertKubica = 75,
        AlainProst = 76,
        AyrtonSenna = 77,
        NobuharuMatsushita = 78,
        NikitaMazepin = 79,
        GuanyaZhou = 80,
        MickSchumacher = 81,
        CallumIlott = 82,
        JuanManuelCorrea = 83,
        JordanKing = 84,
        MahaveerRaghunathan = 85,
        TatianaCalderon = 86,
        AnthoineHubert = 87,
        GuilianoAlesi = 88,
        RalphBoschung = 89,
        MichaelSchumacher = 90,
        DanTicktum = 91,
        MarcusArmstrong = 92,
        ChristianLundgaard = 93,
        YukiTsunoda = 94,
        JehanDaruvala = 95,
        GulhermeSamaia = 96,
        PedroPiquet = 97,
        FelipeDrugovich = 98,
        RobertSchwartzman = 99,
        RoyNissany = 100,
        MarinoSato = 101,
        AidanJackson = 102,
        CasperAkkerman = 103,
        JensonButton = 109,
        DavidCoulthard = 110,
        NicoRosberg = 111,
        Unknown = 255,
    }
    public enum TeamID : byte
    {
        MyTeam = 255,
        Unknown = 254,
        Mercedes = 0,
        Ferrari = 1,
        RedBullRacing = 2,
        Williams = 3,
        AstonMartin = 4,
        Alpine = 5,
        AlphaTauri = 6,
        Haas = 7,
        Mclaren = 8,
        AlfaRomeo = 9,
        ArtGP19 = 42,
        Campos19 = 43,
        Carlin19 = 44,
        SauberJuniorCharouz19 = 45,
        Dams19 = 46,
        UniVirtuosi19 = 47,
        MPMotorsport19 = 48,
        Prema19 = 49,
        Trident19 = 50,
        Arden19 = 51,
        ArtGP20 = 70,
        Campos20 = 71,
        Carlin20 = 72,
        Charouz20 = 73,
        Dams20 = 74,
        UniVirtuosi20 = 75,
        MPMotorsport20 = 76,
        Prema20 = 77,
        Trident20 = 78,
        BWT20 = 79,
        Hitech20 = 80,
        Mercedes2020 = 85,
        Ferrari2020 = 86,
        RedBull2020 = 87,
        Williams2020 = 88,
        RacingPoint2020 = 89,
        Renault2020 = 90,
        AlphaTauri2020 = 91,
        Haas2020 = 92,
        McLaren2020 = 93,
        AlfaRomeo2020 = 94,
    }

    public enum MyTeam : byte
    {
        No,
        Yes
    }

    public enum AiControlled : byte
    {
        Human,
        AI
    }

    public struct ParticipantData
    {
        public AiControlled m_aiControlled;           // Whether the vehicle is AI (1) or Human (0) controlled
        public DriverID m_driverId;       // Driver id - see appendix, 255 if network human
        public byte m_networkId;      // Network id – unique identifier for network players
        public TeamID m_teamId;                 // Team id - see appendix
        public MyTeam m_myTeam;                 // My team flag – 1 = My Team, 0 = otherwise
        public byte m_raceNumber;             // Race number of the car
        public byte m_nationality;            // Nationality of the driver
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 48)]
        public char[] m_name;               // Name of participant in UTF-8 format – null terminated
                                   // Will be truncated with … (U+2026) if too long
        public byte m_yourTelemetry;          // The player's UDP setting, 0 = restricted, 1 = public
        byte m_showOnlineNames;        // The player's show online names setting, 0 = off, 1 = on
        byte m_platform;               // 1 = Steam, 3 = PlayStation, 4 = Xbox, 6 = Origin, 255 = unknown
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PacketParticipantsData
    {
        public PacketHeader m_header;        // Header
        public byte m_numverActiveCars;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 22)]
        public ParticipantData[] m_participants;
    }
}
