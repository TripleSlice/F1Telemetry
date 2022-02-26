using System;
using System.Net;
using System.Net.Sockets;
using F1TMock.Mock;
using System.Text;

public class UDPServer
{
    public static void Main()
    {
        Console.WriteLine("Running");
        int PORT = 20777;

        UdpClient udpClient = new UdpClient();

        void sendData(byte[] data)
        {
            udpClient.Send(data, data.Length, "127.0.0.1", PORT);
        }

        while (true)
        {
            sendData(MockPacketCarDamageData.GetBytes());
            sendData(MockPacketCarStatusData.GetBytes());
            sendData(MockPacketCarTelemetryData.GetBytes());
            sendData(MockPacketEventData.GetBytes());
            sendData(MockPacketLapData.GetBytes());
            sendData(MockPacketMotionData.GetBytes());
            sendData(MockPacketParticipantData.GetBytes());

            Thread.Sleep(100);
        }
    }

    

    
}