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
            participantData.m_driverId = 255;
            participantData.m_networkId = 2;
            participantData.m_teamId = 2;
            participantData.m_myTeam = 0;
            participantData.m_raceNumber = 2;
            participantData.m_nationality = 1;
            participantData.m_name = "MockCar".ToCharArray();
            participantData.m_yourTelemetry = 1;
            return participantData;
        }

        private PacketParticipantsData GetRandomPacketParticipantsData()
        {
            PacketParticipantsData packetParticipantsData = new PacketParticipantsData();
            packetParticipantsData.m_header = GetPacketHeader(PacketType.Participants);
            packetParticipantsData.m_numverActiveCars = 20;
            ParticipantData[] participantDatas = new ParticipantData[22];
            for (int i = 0; i < participantDatas.Length; i++)
            {
                participantDatas[i] = GetRandomParticipantData();
            }
            return packetParticipantsData;
        }

        public byte[] getBytesParticipantData()
        {
            var packet = GetRandomPacketParticipantsData();
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
