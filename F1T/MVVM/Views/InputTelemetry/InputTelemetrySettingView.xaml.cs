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
    public partial class InputTelemetrySettingView : UserControl, ISettingView
    {

        public InputTelemetrySettingView()
        {
            InitializeComponent();
            this.DataContext = Model;
        }

        public BaseModuleViewModel Model { get => InputTelemetryViewModel.GetInstance(); }

        public void OnToggleVisibilityButton_Click(object sender, RoutedEventArgs e){}
    }
}
