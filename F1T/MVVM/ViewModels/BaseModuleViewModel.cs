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

    /// <summary>
    /// Abstract class used for common properties of a <see cref="BaseModuleViewModel"/> which do not require a generic
    /// </summary>
    public abstract class BaseModuleViewModel : ObservableObject
    {
        // each module requires a UDPConnection
        protected UDPConnection udpConnection = UDPConnection.GetInstance();

        private bool _toggled;
        /// <summary>
        /// Property used to see if the Module is currently toggled
        /// </summary>
        public bool Toggled
        {
            get { return _toggled; }
            set { SetField(ref _toggled, value, "Toggled"); }
        }

        private bool _overlayVisible;
        /// <summary>
        /// Property used to see if the Module is currenty visible (displayed)
        /// </summary>
        public bool OverlayVisible
        {
            get { return _overlayVisible; }
            set { SetField(ref _overlayVisible, value, "OverlayVisible"); }
        }
    }
    /// <summary>
    /// Abstract class used for common properties of a <see cref="BaseModuleViewModel{T}"/> which require a generic
    /// <para><see cref="T"/> must be of type <see cref="BaseSettings"/></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseModuleViewModel<T> : BaseModuleViewModel where T : SaveableSettings
    {
        /// <summary>
        /// Settings for the module
        /// </summary>
        /// Each module requires it's own settings of type T, because each one has different settings which are toggleable
        public abstract T Settings { get; }

        /// <summary>
        /// Constructs a <see cref="BaseModuleViewModel"/> instance
        /// </summary>
        public BaseModuleViewModel()
        {
            // If AutoToggled, toggle module on creation
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
