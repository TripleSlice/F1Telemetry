using F1T.Structs;

namespace F1TMock.Mock
{
    public static class MockPacketHeader
    {
        private static uint _frame = 0;
        public static PacketHeader GetBytes(PacketType packetType)
        {
            PacketHeader header = new PacketHeader();
            header.m_packetFormat = 2021;
            header.m_gameMajorVersion = 1;
            header.m_gameMinorVersion = 12;
            header.m_packetVersion = 1;
            header.m_packetId = packetType;
            header.m_sessionUID = 1;
            header.m_sessionUID = 1;
            header.m_sessionTime = 1.0f;
            header.m_frameIdentifier = _frame;
            header.m_playerCarIndex = 0;
            header.m_secondaryPlayerCarIndex = 1;

            _frame++;

            return header;
        }
    }
}
