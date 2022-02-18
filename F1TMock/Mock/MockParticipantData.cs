using F1T.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace F1TMock.Mock
{
    class MockParticipantData
    {
        private ParticipantData GetRandomParticipantData()
        {
            ParticipantData participantData = new ParticipantData();
            participantData.m_aiControlled = 1;
            participantData.m_driverId = DriverID.Unknown;
            participantData.m_networkId = 2;
            participantData.m_teamId = TeamID.Mclaren;
            participantData.m_myTeam = 0;
            participantData.m_raceNumber = 2;
            participantData.m_nationality = 1;
            char[] charArray = new char[48];
            charArray[0] = 'M';
            charArray[1] = 'o';
            charArray[2] = 'c';
            charArray[3] = 'k';
            participantData.m_name = charArray;
            participantData.m_yourTelemetry = 1;
            return participantData;
        }

        private PacketParticipantsData GetRandomPacketParticipantsData()
        {
            PacketParticipantsData packetParticipantsData = new PacketParticipantsData();
            packetParticipantsData.m_header = MockHeader.GetPacketHeader(PacketType.Participants);
            packetParticipantsData.m_numverActiveCars = 22;
            ParticipantData[] participantDatas = new ParticipantData[22];
            for (int i = 0; i < participantDatas.Length; i++)
            {
                participantDatas[i] = GetRandomParticipantData();
            }
            packetParticipantsData.m_participants = participantDatas;
            return packetParticipantsData;
        }

        public byte[] GetBytesParticipantData()
        {
            PacketParticipantsData packet = GetRandomPacketParticipantsData();
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
