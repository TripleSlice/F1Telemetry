using System.Runtime.InteropServices;

namespace F1T.Structs
{

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct EventFastestLap
    {
        public byte vehicleIdx; // Vehicle index of car achieving fastest lap
        public float lapTime;    // Lap time is in seconds
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct EventRetirement
    {
        public byte vehicleIdx; // Vehicle index of car retiring
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct EventTeamMateInPits
    {
        public byte vehicleIdx; // Vehicle index of team mate
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct EventRaceWinner
    {
        public byte vehicleIdx; // Vehicle index of the race winner
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct EventPenalty
    {
        public byte penaltyType;		// Penalty type – see Appendices
        public byte infringementType;		// Infringement type – see Appendices
        public byte vehicleIdx;         	// Vehicle index of the car the penalty is applied to
        public byte otherVehicleIdx;    	// Vehicle index of the other car involved
        public byte time;               	// Time gained, or time spent doing action in seconds
        public byte lapNum;             	// Lap the penalty occurred on
        public byte placesGained;       	// Number of places gained by this

    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct EventSpeedTrap
    {
        public byte vehicleIdx;		// Vehicle index of the vehicle triggering speed trap
        public float speed;             // Top speed achieved in kilometres per hour
        public byte overallFastestInSession;   // Overall fastest speed in session = 1, otherwise 0
        public byte driverFastestInSession;    // Fastest speed for driver in session = 1, otherwise 0

    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct EventStartLights
    {
        public byte numLights;		// Number of lights showing
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct EventDriveThroughPentaltyServed
    {
        public byte vehicleIdx;                 // Vehicle index of the vehicle serving drive through
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct EventStopGoPenaltyServed
    {
        public byte vehicleIdx;                 // Vehicle index of the vehicle serving stop go
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct EventFlashback
    {
        public uint flashbackFrameIdentifier;  // Frame identifier flashed back to
        public float flashbackSessionTime;       // Session time flashed back to
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct EventButtons
    {
        public uint m_buttonStatus;    // Bit flags specifying which buttons are being pressed currently - see appendices
    }


    [StructLayout(LayoutKind.Explicit)]
    public struct EventDataDetails
    {
        [FieldOffset(0)]
        public EventFastestLap fastestLap;

        [FieldOffset(0)]
        public EventRetirement retirement;

        [FieldOffset(0)]
        public EventTeamMateInPits teamMateInPits;

        [FieldOffset(0)]
        public EventRaceWinner raceWinner;

        [FieldOffset(0)]
        public EventPenalty penalty;

        [FieldOffset(0)]
        public EventSpeedTrap speedTrap;

        [FieldOffset(0)]
        public EventStartLights startLights;

        [FieldOffset(0)]
        public EventDriveThroughPentaltyServed driveThroughPentaltyServed;

        [FieldOffset(0)]
        public EventStopGoPenaltyServed stopGoPenaltyServed;

        [FieldOffset(0)]
        public EventFlashback flashback;

        [FieldOffset(0)]
        public EventButtons buttons;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PacketEventData
    {
        public PacketHeader m_header;                  // Header
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public char[] m_eventStringCode;     // Event string code, see below
        public EventDataDetails m_eventDetails;            // Event details - should be interpreted differently for each type
    }

    public static class EEventDataDetails
    {
        public const string SessionStarted = "SSTA"; // Sent when the session starts
        public const string SessionEnded = "SEND";  // Sent when the session ends
        public const string DriverFastestLap = "FTLP";  // When a driver achieves the fastest lap
        public const string DriverRetire = "RTMT";  // When a driver retires
        public const string RaceControlEnableDRS = "DRSE";  // Race control have enabled DRS
        public const string RaceControlDisableDRS = "DRSD";  // Race control have disabled DRS
        public const string TeamatePit = "TMPT";  // Your team mate has entered the pits
        public const string ChequeredFlag = "CHQF";  // The chequered flag has been waved
        public const string RaceWinner = "RCWN";  // The race winner is announced
        public const string PenaltyIssued = "PENA";  // A penalty has been issued – details in event
        public const string SpeedTrapFastest = "SPTP";  // Speed trap has been triggered by fastest speed
        public const string StartLights = "STLG";  // Start lights – number shown
        public const string LightsOut = "LGOT";  // Lights out
        public const string DriveThroughServed = "DTSV";  // Drive through penalty served
        public const string StopGoServed = "SGSV";  // Stop go penalty served
        public const string FlashbackActivated = "FLBK";  // Flashback activated
        public const string ButtonStatusChanged = "BUTN";  // Button status changed

    }
}
