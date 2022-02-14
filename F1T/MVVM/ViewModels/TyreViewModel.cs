using F1T.MVVM.Models;
using F1T.MVVM.Views.Tyre;
using F1T.Settings;
using F1T.Structs;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace F1T.MVVM.ViewModels
{
    /// <summary>
    /// ViewModel for the TyreOverlayView and TyreSettingView
    /// </summary>
    public class TyreViewModel : BaseModuleViewModel<TyreSettings>
    {

        // === BEGINING OF MODULE SETUP ===
        // === Singleton Instance with Thread Saftey ===
        private static TyreViewModel _instance = null;
        private static object _singletonLock = new object();
        public static TyreViewModel GetInstance()
        {
            lock (_singletonLock)
            {
                if (_instance == null) { _instance = new TyreViewModel(); }
                return _instance;
            }
        }
        // Settings
        private TyreSettings _settings = new TyreSettings().Read<TyreSettings>();
        public override TyreSettings Settings { get => _settings; }
        // === END OF MODULE SETUP ===



        public PacketCarStatusData CarStatusData;
        public PacketCarDamageData CarDamageData;
        public PacketLapData LapData;

        public int PlayerIndexCarStatus = -1;
        public int PlayerIndexCarDamage = -1;
        public int PlayerIndexLapData = -1;

        public LapData PlayerCarLapData;

        public ObservableCollection<TyreInfo> TyreInfoArr { get; set; } 

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


        private TyreViewModel() : base()
        {
            TyreInfoArr = new ObservableCollection<TyreInfo>();
            TyreInfoArr.Add(new TyreInfo(0, 0));
            TyreInfoArr.Add(new TyreInfo(0, 0));
            TyreInfoArr.Add(new TyreInfo(0, 0));
            TyreInfoArr.Add(new TyreInfo(0, 0));
            TyreInfoArr.Add(new TyreInfo(0, 0));



            udpConnection.OnCarStatusDataReceive += TyreAgeUpdate;
            udpConnection.OnCarDamageDataReceive += TyreWearUpdate;
            udpConnection.OnLapDataReceive += PositionUpdate;
        }

    }
}
