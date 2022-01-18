using System.Runtime.InteropServices;

namespace F1T.Structs
{

    public enum PacketType : byte
    {
        Motion = 0,	// Contains all motion data for player’s car – only sent while player is in control
        Session	= 1,	// Data about the session – track, time left
        LapData = 2,	// Data about all the lap times of cars in the session
        Event = 3,	// Various notable events that happen during a session
        Participants = 4,	// List of participants in the session, mostly relevant for multiplayer
        CarSetups = 5,	// Packet detailing car setups for cars in the race
        CarTelemetry = 6,	// Telemetry data for all cars
        CarStatus = 7,	// Status data for all cars
        FinalClassification = 8,	// Final classification confirmation at the end of a race
        LobbyInfo = 9,	// Information about players in a multiplayer lobby
        CarDamage = 10,	// Damage status for all cars
        SessionHistory = 11,	// Lap and tyre data for session
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PacketHeader
    {
        public ushort m_packetFormat;         // 2021
        public byte m_gameMajorVersion;     // Game major version - "X.00"
        public byte m_gameMinorVersion;     // Game minor version - "1.XX"
        public byte m_packetVersion;        // Version of this packet type, all start from 1
        public PacketType m_packetId;             // Identifier for the packet type, see above
        public ulong m_sessionUID;           // Unique identifier for the session
        public float m_sessionTime;          // Session timestamp
        public uint m_frameIdentifier;      // Identifier for the frame the data was retrieved on
        public byte m_playerCarIndex;       // Index of player's car in the array
        public byte m_secondaryPlayerCarIndex;       // Index of 2nd player's car in the array
    };
}
