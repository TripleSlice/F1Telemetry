using F1T.Structs;
using F1T.PacketParsers;
using System;
using System.ComponentModel;
using System.Net.Sockets;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using F1T.MVVM.Models;
using System.Collections.ObjectModel;
using F1T.Core;
using F1T.MVVM.Views.InputTelemetry;
using F1T.MVVM.Views.Flags;
using System.Windows.Input;

namespace F1T.MVVM.ViewModels
{
    public class MainViewModel : ObservableObject
    {


        // === Current View ===
        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set { SetField(ref _currentView, value, "CurrentView"); }
        }


        // === Command for Views ===
        public RelayCommand InputTelemetrySettingViewCommand { get; set; }
        public RelayCommand FlagSettingViewCommand { get; set; }

        // === Views ===
        public InputTelemetrySettingView InputTelemetrySetting = new InputTelemetrySettingView();
        public FlagsSettingView FlagSetting = new FlagsSettingView();

        public PacketViewModel sharedViewModel = PacketViewModel.GetInstance();


        // === Singleton Instance with Thread Saftey ===
        private static MainViewModel _instance = null;
        private static object _singletonLock = new object();
        public static MainViewModel GetInstance()
        {
            lock (_singletonLock){
                if (_instance == null){ _instance = new MainViewModel(); }
                return _instance;
            }
        }

        // == UDP Client ===
        UdpClient Client;

        private MainViewModel()
        {
            CreateCommandInstances();
            CreateNewUDPClient();
            CurrentView = InputTelemetrySetting;
        }

        private void CreateCommandInstances()
        {
            InputTelemetrySettingViewCommand = new RelayCommand(obj =>{CurrentView = InputTelemetrySetting;});
            FlagSettingViewCommand = new RelayCommand(obj => { CurrentView = FlagSetting; });
        }

        private void CreateNewUDPClient()
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
                    CarTelemetryData playerCarTelemetryData = packetCarTelemetryData.m_carTelemetryData[playerCarIndex];
                    sharedViewModel.PlayerCarTelemetryData = new CarTelemetryDataObject(playerCarTelemetryData);

                    break;

            }
            Client.BeginReceive(new AsyncCallback(recv), null);
        }
    }
}
