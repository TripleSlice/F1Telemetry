using F1T.Core;
using F1T.MVVM.Views;
using F1T.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace F1T.MVVM.ViewModels
{


    public abstract class BaseModuleViewModel : ObservableObject
    {
        protected UDPConnection udpConnection = UDPConnection.GetInstance();

        private bool _toggled;
        public bool Toggled
        {
            get { return _toggled; }
            set { SetField(ref _toggled, value, "Toggled"); }
        }

        private bool _overlayVisible;
        public bool OverlayVisible
        {
            get { return _overlayVisible; }
            set { SetField(ref _overlayVisible, value, "OverlayVisible"); }
        }
    }
    public abstract class BaseModuleViewModel<T> : BaseModuleViewModel where T : BaseSettings
    {
        public abstract T Settings { get; }

        public BaseModuleViewModel()
        {
            if (Settings.AutoToggled)
            {
                Settings.Toggled = true;
                Toggled = true;
            }
            else
            {
                Settings.Toggled = false;
                Toggled = false;
            }
        }
    }
}
