using F1T.Structs;
using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace F1T.Core
{
    // https://github.com/thomz12/F12020-Telemetry
    public class UDPConnection
    {
        // == UDP Client ===
        private UdpClient Client;

        // Delegates
        public delegate void MotionDataReceiveDelegate(PacketMotionData packet);
        public delegate void CarTelemetryDataReceiveDelegate(PacketCarTelemetryData packet);
        public delegate void CarStatusDataReceiveDelegate(PacketCarStatusData packet);
        public delegate void CarDamageDataReceiveDelegate(PacketCarDamageData packet);
        public delegate void LapDataReceiveDelegate(PacketLapData packet);
        public delegate void ParticipantDataReceiveDelegate(PacketParticipantsData packet);

        // Packet events
        public event MotionDataReceiveDelegate OnMotionDataReceive;
        public event CarTelemetryDataReceiveDelegate OnCarTelemetryDataReceive;
        public event CarStatusDataReceiveDelegate OnCarStatusDataReceive;
        public event CarDamageDataReceiveDelegate OnCarDamageDataReceive;
        public event LapDataReceiveDelegate OnLapDataReceive;
        public event ParticipantDataReceiveDelegate OnPaticipantDataReceive;



        // === Singleton Instance with Thread Saftey ===
        private static UDPConnection _instance = null;
        private static object _singletonLock = new object();
        public static UDPConnection GetInstance()
        {
            lock (_singletonLock)
            {
                if (_instance == null) { _instance = new UDPConnection(); }
                return _instance;
            }
        }


        private UDPConnection()
        {
            //Client uses as receive udp client
            Client = new UdpClient(20777);

            try { Client.BeginReceive(new AsyncCallback(recv), null); }
            catch (Exception e) { Console.WriteLine(e.ToString()); }
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

            GCHandle handle = GCHandle.Alloc(received, GCHandleType.Pinned);
            PacketHeader packetHeader = (PacketHeader)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(PacketHeader));

            switch (packetHeader.m_packetId)
            {
                case PacketType.CarTelemetry:
                    PacketCarTelemetryData telemetryData = (PacketCarTelemetryData)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(PacketCarTelemetryData));
                    OnCarTelemetryDataReceive?.Invoke(telemetryData);
                    break;

                case PacketType.Motion:
                    PacketMotionData motionData = (PacketMotionData)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(PacketMotionData));
                    OnMotionDataReceive?.Invoke(motionData);
                    break;

                case PacketType.CarDamage:
                    PacketCarDamageData damageData = (PacketCarDamageData)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(PacketCarDamageData));
                    OnCarDamageDataReceive?.Invoke(damageData);
                    break;

                case PacketType.CarStatus:
                    PacketCarStatusData statusData = (PacketCarStatusData)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(PacketCarStatusData));
                    OnCarStatusDataReceive?.Invoke(statusData);
                    break;

                case PacketType.LapData:
                    PacketLapData lapData = (PacketLapData)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(PacketLapData));
                    OnLapDataReceive?.Invoke(lapData);
                    break;

                case PacketType.Participants:
                    PacketParticipantsData participantData = (PacketParticipantsData)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(PacketParticipantsData));
                    OnPaticipantDataReceive?.Invoke(participantData);
                    break;
            }
            Client.BeginReceive(new AsyncCallback(recv), null);
        }
    }
}
