using F1T.Core;

namespace F1T.MVVM.ViewModels
{
    public class InputTelemetryViewModel : BaseModuleViewModel
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

        private InputTelemetryViewModel() : base()
        {
            // TODO
            // Set Defaults...
            // Regarding this module specifically....
        }
    }
}
