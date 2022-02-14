using F1T.Core;
using F1T.MVVM.ViewModels;
using F1T.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace F1T.MVVM.Views
{

    /// <summary>
    /// Interaction logic for OverlayContainer.xaml
    /// </summary>
    /// The OverlayContainer will hold the OverlayView (UserControl) inside of it
    public partial class OverlayContainer : Window, INotifyPropertyChanged
    {
        // boiler-plate
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        protected bool SetField<T>(ref T field, T value, string propertyName)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        // This ViewModel
        public OverlayContainerViewModel Model = new OverlayContainerViewModel();

        // ViewModel of the OverlayView
        private BaseModuleViewModel _viewModel;
        public BaseModuleViewModel ViewModel
        {
            get { return _viewModel; }
            set { SetField(ref _viewModel, value, "ViewModel"); }
        }

        public OverlayContainer(UserControl overlayView, BaseModuleViewModel vm)
        {
            ViewModel = vm;
            InitializeComponent();
            this.DataContext = vm;
            ContentHolder.DataContext = Model;
            Model.CurrentOverlay = overlayView;
        }
    }
}
