using F1T.MVVM.Models;
using F1T.MVVM.ViewModels;
using F1T.PacketParsers;
using F1T.Structs;
using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace F1T.Core
{
    public class UDPConnection
    {
        // == UDP Client ===
        private UdpClient Client;

        private PacketViewModel packetViewModel = PacketViewModel.GetInstance();

        public UDPConnection()
        {
            //Client uses as receive udp client
            Client = new UdpClient(20777);

            try
            {
                Client.BeginReceive(new AsyncCallback(recv), null);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        // === Ingestion point of data from the game ===
        // === We create packets, then observable objects ===
        // === from the packets which we can use anywhere ===
        private void recv(IAsyncResult res)
        {
            // https://stackoverflow.com/questions/7266101/receive-messages-continuously-using-udpclient
            // https://stackoverflow.com/questions/60352529/byte-array-to-struct-udp-packet
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
            byte[] received = Client.EndReceive(res, ref RemoteIpEndPoint);
            ReadOnlySpan<byte> remaining = received;

            PacketHeader packetHeader = PacketHeaderParser.Parse(remaining);
            remaining = PacketHeaderParser.Slice(remaining);

            byte playerCarIndex = packetHeader.m_playerCarIndex;

            switch (packetHeader.m_packetId)
            {
                case PacketType.CarTelemetry:
                    PacketCarTelemetryData packetCarTelemetryData = PacketCarTelemetryDataParser.Parse(remaining);
                    packetViewModel.AllCarTelemetryData = new PacketCarTelemetryDataObject(packetCarTelemetryData);
                    CarTelemetryData playerCarTelemetryData = packetCarTelemetryData.m_carTelemetryData[playerCarIndex];
                    packetViewModel.PlayerCarTelemetryData = new CarTelemetryDataObject(playerCarTelemetryData);

                    break;

                case PacketType.Motion:
                    PacketMotionData packetMotionData = PacketMotionDataParser.Parse(remaining);
                    packetViewModel.AllCarMotionData = new PacketMotionDataObject(packetMotionData);
                    CarMotionData playerCarMotionData = packetMotionData.m_carMotionData[playerCarIndex];
                    packetViewModel.PlayerCarMotionData = new CarMotionDataObject(playerCarMotionData);
                    packetViewModel.PlayerCarMotionIndex = packetHeader.m_playerCarIndex;



                    break;
            }
            Client.BeginReceive(new AsyncCallback(recv), null);
        }
    }
}
