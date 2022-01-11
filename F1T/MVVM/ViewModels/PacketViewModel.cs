using F1T.Core;
using F1T.MVVM.Models;
using System.Collections.ObjectModel;

namespace F1T.MVVM.ViewModels
{

    // Place all the packet parsing here
    // This is stuff that ever model will inherit...
    public class PacketViewModel : ObservableObject
    {

        // === Singleton Instance with Thread Saftey ===
        private static PacketViewModel _instance = null;
        private static object _singletonLock = new object();
        public static PacketViewModel GetInstance()
        {
            lock (_singletonLock)
            {
                if (_instance == null) { _instance = new PacketViewModel(); }
                return _instance;
            }
        }

        private PacketViewModel()
        {

        }

        // === Packets as Observable Objects ===
        private PacketCarTelemetryDataObject _AllCarTelemetryData;
        public PacketCarTelemetryDataObject AllCarTelemetryData
        {
            get { return _AllCarTelemetryData; }
            set { SetField(ref _AllCarTelemetryData, value, "AllCarTelemetryData"); }
        }

        private CarTelemetryDataObject _PlayerCarTelemetryData;
        public CarTelemetryDataObject PlayerCarTelemetryData
        {
            get { return _PlayerCarTelemetryData; }
            set { SetField(ref _PlayerCarTelemetryData, value, "PlayerCarTelemetryData"); }
        }


        private PacketMotionDataObject _AllCarMotionData;
        public PacketMotionDataObject AllCarMotionData
        {
            get { return _AllCarMotionData; }
            set { SetField(ref _AllCarMotionData, value, "AllCarMotionData"); }
        }


        private CarMotionDataObject _PlayerCarMotionData;
        public CarMotionDataObject PlayerCarMotionData
        {
            get { return _PlayerCarMotionData; }
            set { SetField(ref _PlayerCarMotionData, value, "PlayerCarMotionData"); }
        }


        private int _PlayCarMotionIndex;
        public int PlayerCarMotionIndex
        {
            get { return _PlayCarMotionIndex; }
            set { SetField(ref _PlayCarMotionIndex, value, "PlayerCarMotionIndex"); }
        }




    }
}
