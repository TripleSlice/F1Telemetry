using F1T.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1T.MVVM.ViewModels
{
    /// <summary>
    /// ViewModel for the OverlayContainer
    /// </summary>
    public class OverlayContainerViewModel : ObservableObject
    {

        // === Current Overlay ===
        private object _currentOverlay;
        public object CurrentOverlay
        {
            get { return _currentOverlay; }
            set { SetField(ref _currentOverlay, value, "CurrentOverlay"); }
        }
    }
}
