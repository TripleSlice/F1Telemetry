using F1T.MVVM.Models;
using F1T.Structs;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace F1T.MVVM.ViewModels
{
    public class TyreViewModel : BaseModuleViewModel
    {
        public int ViewWidth = 500;
        public int ViewHeight = 500;

        private bool _tyreWearVisibile;
        public bool TyreWearVisible
        {
            get { return _tyreWearVisibile; }
            set { SetField(ref _tyreWearVisibile, value, "TyreWearVisible"); }
        }

        private bool _tyreAgeVisibile;
        public bool TyreAgeVisible
        {
            get { return _tyreAgeVisibile; }
            set { SetField(ref _tyreAgeVisibile, value, "TyreAgeVisible"); }
        }


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

        // TODO
        // Load/Set Defaults of this Module
        private TyreViewModel() : base()
        {
            Height = 170;


            TyreInfoArr = new ObservableCollection<TyreInfo>();
            TyreInfoArr.Add(new TyreInfo(1, 2));
            TyreInfoArr.Add(new TyreInfo(3, 4));
            TyreInfoArr.Add(new TyreInfo(0, 0));
            TyreInfoArr.Add(new TyreInfo(0, 0));
            TyreInfoArr.Add(new TyreInfo(0, 0));

            TyreWearVisible = true;
            TyreAgeVisible = true;

            udpConnection.OnCarStatusDataReceive += TyreAgeUpdate;
            udpConnection.OnCarDamageDataReceive += TyreWearUpdate;
            udpConnection.OnLapDataReceive += PositionUpdate;
        }

    }
}
