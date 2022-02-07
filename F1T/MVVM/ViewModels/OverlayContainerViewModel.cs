using F1T.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1T.MVVM.ViewModels
{
    public class OverlayContainerViewModel : ObservableObject
    {

        // === Current Overlay ===
        private object _currentOverlay;
        public object CurrentOverlay
        {
            get { return _currentOverlay; }
            set { SetField(ref _currentOverlay, value, "CurrentOverlay"); }
        }

        private int _top;
        public int Top
        {
            get { return _top; }
            set { SetField(ref _top, value, "Top"); }
        }

        private int _left;
        public int Left
        {
            get { return _left; }
            set { SetField(ref _left, value, "Left"); }
        }

    }
}
