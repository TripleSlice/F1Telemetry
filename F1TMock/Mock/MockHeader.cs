using F1T.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace F1TMock.Mock
{
    class MockHeader
    {
        public static PacketHeader GetPacketHeader(PacketType packetType)
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
            header.m_frameIdentifier = 1;
            header.m_playerCarIndex = 1;
            header.m_secondaryPlayerCarIndex = 2;
            return header;
        }
    }
}
