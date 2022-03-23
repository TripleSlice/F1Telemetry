using F1T.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace F1TMock.Mock
{
    class MockPacketEventData
    {
        private static PacketEventData GetRandomPacketEventData()
        {
            PacketEventData data = new PacketEventData();
            data.m_header = MockPacketHeader.GetBytes(PacketType.Event);
            // Generate a random event
            data.m_eventStringCode = new char[]{ 'c'};
            // Get the details for the event
            data.m_eventDetails = new EventDataDetails();
            return data;
        }


        public PacketHeader m_header;                  // Header
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public char[] m_eventStringCode;     // Event string code, see below
        public EventDataDetails m_eventDetails;            // Event details - should be interpreted differently for each type


        public static byte[] GetBytes()
        {
            var packet = GetRandomPacketEventData();
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
