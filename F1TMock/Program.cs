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
        //MockEventData mockEventData = new MockEventData();
        MockCarTelemetryData mockCarTelemetryData = new MockCarTelemetryData();
        MockCarStatusData mockCarStatusData = new MockCarStatusData();
        MockCarDamageData mockCarDamageData = new MockCarDamageData();

        UdpClient udpClient = new UdpClient();

        void sendData(byte[] data)
        {
            udpClient.Send(data, data.Length, "127.0.0.1", PORT);
        }

        while (true)
        {
            byte[] data = mockParticipantData.GetBytesParticipantData();
            sendData(data);
            data = mockMotionData.GetBytesMotionData();
            sendData(data);
            data = mockLapData.GetBytesLapData();
            sendData(data);
            data = mockCarTelemetryData.GetBytesCarTelemetryData();
            sendData(data);
            data = mockCarStatusData.GetBytesCarStatusData();
            sendData(data);
            data = mockCarDamageData.GetBytesCarDamageData();
            sendData(data);
            Thread.Sleep(100);
        }
    }

    

    
}