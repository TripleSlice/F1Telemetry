using F1T.Core;

namespace F1T.MVVM.ViewModels
{
    public class InputTelemetryViewModel : BaseOverlayViewModel
    {



        // === Singleton Instance with Thread Saftey ===
        private static InputTelemetryViewModel _instance = null;
        private static object _singletonLock = new object();
        public static InputTelemetryViewModel GetInstance()
        {
            lock (_singletonLock)
            {
                if (_instance == null) { _instance = new InputTelemetryViewModel(); }
                return _instance;
            }
        }

        private InputTelemetryViewModel()
        {
            // Set Defaults...
            // This should be loaded and saved to file eventually
            Opacity = 0.50f;
        }

        private int _opacitySliderValue;
        public int OpacitySliderValue
        {
            get { return _opacitySliderValue; }
            set {
                SetField(ref _opacitySliderValue, value, "OpacitySliderValue");
                Opacity = OpacitySliderValue / 100f; 
            }
        }

        private float _opacity;
        public float Opacity
        {
            get { return _opacity; }
            set { 
                SetField(ref _opacity, value, "Opacity");
            }
        }


        private PacketViewModel _sharedViewModel = PacketViewModel.GetInstance();
        public PacketViewModel sharedViewModel
        {
            get { return _sharedViewModel; }
            set
            {
                SetField(ref _sharedViewModel, value, "sharedViewModel");
            }
        }
    }
}
