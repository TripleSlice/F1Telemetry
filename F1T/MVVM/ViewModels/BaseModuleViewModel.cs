using F1T.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1T.MVVM.ViewModels
{
    public class BaseModuleViewModel : ObservableObject
    {

        private PacketViewModel _packetViewModel = PacketViewModel.GetInstance();
        public PacketViewModel PacketViewModel
        {
            get { return _packetViewModel; }
            set
            {
                SetField(ref _packetViewModel, value, "PacketViewModel");
            }
        }

        private bool _overlayVisible;
        public bool OverlayVisible
        {
            get { return _overlayVisible; }
            set { SetField(ref _overlayVisible, value, "OverlayVisible"); }
        }

        private bool _toggled;
        public bool Toggled
        {
            get { return _toggled; }
            set { SetField(ref _toggled, value, "Toggled"); }
        }
    }
}
