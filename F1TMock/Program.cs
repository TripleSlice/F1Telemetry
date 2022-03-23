using System;
using System.Net;
using System.Net.Sockets;
using F1TMock.Mock;
using System.Text;
using System.Diagnostics;

namespace F1TMock
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("...starting to send mock data...");
            int PORT = 20777;

            UdpClient udpClient = new UdpClient();

            void sendData(byte[] data)
            {
                udpClient.Send(data, data.Length, "127.0.0.1", PORT);
            }

            bool oneTimeFlag = true;

            while (true)
            {

                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                sendData(MockPacketCarDamageData.GetBytes());
                sendData(MockPacketCarStatusData.GetBytes());
                sendData(MockPacketCarTelemetryData.GetBytes());
                //sendData(MockPacketEventData.GetBytes());
                sendData(MockPacketLapData.GetBytes());
                sendData(MockPacketMotionData.GetBytes());
                sendData(MockPacketParticipantData.GetBytes());

                if (oneTimeFlag)
                {
                    sendData(MockPacketFinalClassificationData.GetBytes());
                    Console.WriteLine("Sent one time PacketFinalClassifcationData...");
                    oneTimeFlag = false;
                }

                stopWatch.Stop();
               
                Console.WriteLine("Sent data in " + stopWatch.ElapsedMilliseconds + "ms");

                // This is far below what the game will send us
                // This is OK, as it is just for test purposes
                // The game sends it at 60Hz, which is 1000ms / 60hz = 16.666 
                Thread.Sleep(100);
            }
        }
    }
}
