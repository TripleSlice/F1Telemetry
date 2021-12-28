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

    [StructLayout(LayoutKind.Explicit, Pack = 0, Size = 24)]
    public struct PacketHeader
    {
        [FieldOffset(0)]
        public ushort m_packetFormat;         // 2021
        [FieldOffset(2)]
        public byte m_gameMajorVersion;     // Game major version - "X.00"
        [FieldOffset(3)]
        public byte m_gameMinorVersion;     // Game minor version - "1.XX"
        [FieldOffset(4)]
        public byte m_packetVersion;        // Version of this packet type, all start from 1
        [FieldOffset(5)]
        public PacketType m_packetId;             // Identifier for the packet type, see above
        [FieldOffset(6)]
        public ulong m_sessionUID;           // Unique identifier for the session
        [FieldOffset(14)]
        public float m_sessionTime;          // Session timestamp
        [FieldOffset(18)]
        public uint m_frameIdentifier;      // Identifier for the frame the data was retrieved on
        [FieldOffset(22)]
        public byte m_playerCarIndex;       // Index of player's car in the array
        [FieldOffset(23)]
        public byte m_secondaryPlayerCarIndex;       // Index of 2nd player's car in the array
    };
}
