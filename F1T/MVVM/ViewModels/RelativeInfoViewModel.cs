using F1T.MVVM.Models;
using F1T.Settings;
using F1T.Structs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace F1T.MVVM.ViewModels
{
    /// <summary>
    /// ViewModel for the TyreOverlayView and TyreSettingView
    /// </summary>
    public class RelativeInfoViewModel : BaseModuleViewModel<RelativeInfoSettings>
    {

        // === BEGINING OF MODULE SETUP ===
        // === Singleton Instance with Thread Saftey ===
        private static RelativeInfoViewModel _instance = null;
        private static object _singletonLock = new object();
        public static RelativeInfoViewModel GetInstance()
        {
            lock (_singletonLock)
            {
                if (_instance == null) { _instance = new RelativeInfoViewModel(); }
                return _instance;
            }
        }
        // Settings
        private RelativeInfoSettings _settings = new RelativeInfoSettings().Read<RelativeInfoSettings>();
        public override RelativeInfoSettings Settings { get => _settings; }
        // === END OF MODULE SETUP ===



        public PacketCarStatusData CarStatusData;
        public PacketCarDamageData CarDamageData;
        public PacketLapData LapData;

        public Dictionary<int, PacketSessionHistoryData> SessionHistoryData = new Dictionary<int, PacketSessionHistoryData>();

        public int PlayerIndexCarStatus = -1;
        public int PlayerIndexCarDamage = -1;
        public int PlayerIndexLapData = -1;

        public LapData PlayerCarLapData;

        public ObservableCollection<RelativeInfo> RelativeInfoArr { get; set; } 

        private void TyreAgeUpdate(PacketCarStatusData packet)
        {
            CarStatusData = packet;
            PlayerIndexCarStatus = packet.m_header.m_playerCarIndex;
        }

        private void TyreWearUpdate(PacketCarDamageData packet)
        {
            CarDamageData = packet;
            PlayerIndexCarDamage = packet.m_header.m_playerCarIndex;
        }

        private void PositionUpdate(PacketLapData packet)
        {
            LapData = packet;
            PlayerIndexLapData = packet.m_header.m_playerCarIndex;
            PlayerCarLapData = LapData.m_lapData[PlayerIndexLapData];
        }

        private void SessionHistoryUpdate(PacketSessionHistoryData packet)
        {
            SessionHistoryData[packet.m_carIdx] = packet;
        }

        private RelativeInfoViewModel() : base()
        {
            RelativeInfoArr = new ObservableCollection<RelativeInfo>();
            RelativeInfoArr.Add(new RelativeInfo());
            RelativeInfoArr.Add(new RelativeInfo());
            RelativeInfoArr.Add(new RelativeInfo());
            RelativeInfoArr.Add(new RelativeInfo());
            RelativeInfoArr.Add(new RelativeInfo());

            udpConnection.OnCarStatusDataReceive += TyreAgeUpdate;
            udpConnection.OnCarDamageDataReceive += TyreWearUpdate;
            udpConnection.OnLapDataReceive += PositionUpdate;
            udpConnection.OnSessionHistoryDataReceive += SessionHistoryUpdate;
        }
    }
}
