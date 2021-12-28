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
        public ObservableCollection<CarTelemetryDataObject> CarTelemetryDataObjects { get; set; }

        private CarTelemetryDataObject _PlayerCarTelemetryData;
        public CarTelemetryDataObject PlayerCarTelemetryData
        {
            get { return _PlayerCarTelemetryData; }
            set { SetField(ref _PlayerCarTelemetryData, value, "PlayerCarTelemetryData"); }
        }
    }
}
