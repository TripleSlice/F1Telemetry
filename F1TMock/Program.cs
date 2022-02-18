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

        MockParticipantData mockParticipantData = new MockParticipantData();
        MockMotionData mockMotionData = new MockMotionData();
        MockLapData mockLapData = new MockLapData();
        MockHeader mockHeader = new MockHeader();
        MockEventData mockEventData = new MockEventData();
        MockCarTelemetryData mockCarTelemetryData = new MockCarTelemetryData();
        MockCarStatusData mockCarStatusData = new MockCarStatusData();
        MockCarDamageData mockCarDamageData = new MockCarDamageData();

        UdpClient udpClient = new UdpClient();
        udpClient.Client.Bind(new IPEndPoint(IPAddress.Any, PORT));

        IPEndPoint from = new IPEndPoint(0, 0);

        void sendData(byte[] data)
        {
            udpClient.Send(data, data.Length, "255.255.255.255", PORT);
        }

        while (true)
        {
            byte[] data = mockParticipantData.getBytesParticipantData();
            sendData(data);
            data = mockMotionData.getBytesMotionData();
            sendData(data);
            Thread.Sleep(1000);
        }
    }

    

    
}