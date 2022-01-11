using F1T.MVVM.ViewModels;
using F1T.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace F1T.MVVM.Views.Radar
{
    /// <summary>
    /// Interaction logic for RadarSettingView.xaml
    /// </summary>
    public partial class RadarSettingView : UserControl, ISettingView
    {
        public RadarSettingView()
        {
            InitializeComponent();
            this.DataContext = Model;
        }

        public BaseModuleViewModel Model { get => RadarViewModel.GetInstance(); }
        public ToggleButton VisibilityButton { get => VisibilityButtonInstance; }

        public void OnToggleVisibilityButton_Click(object sender, RoutedEventArgs e) { }
        public void ViewToggleVisibilityButton_Click(object sender, RoutedEventArgs e) { this.ToggleVisibilityButton_Click(sender, e); }
    }
}
