using F1T.MVVM.ViewModels;
using F1T.Core;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace F1T.MVVM.Views.InputTelemetry
{
    /// <summary>
    /// Interaction logic for InputTelemetrySettingView.xaml
    /// </summary>
    public partial class InputTelemetrySettingView : UserControl
    {

        // === ViewModel ===
        InputTelemetryViewModel Model = InputTelemetryViewModel.GetInstance();
        public InputTelemetrySettingView()
        {
            InitializeComponent();
            this.DataContext = Model;
        }

        private void ToggleVisibilityButton_Click(object sender, RoutedEventArgs e)
        {


            // TODO This logic is wrong, that and the FocusMonitor logic
            // desired behaviour is when the button is pressed
            // the overlay is only shown when the game is up...
            if (Model.Toggled)
            {
                FocusMonitor.HideOverlay(Model);
            }
            else
            {
                FocusMonitor.DisplayOverlay(Model);
            }

            Model.OverlayVisible = !Model.OverlayVisible;
            Model.Toggled = !Model.Toggled;
        }
    }
}
