using F1T.MVVM.ViewModels;
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

namespace F1T.MVVM.Views.Settings
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : UserControl
    {
 
        public SettingsViewModel ViewModel = SettingsViewModel.GetInstance();
        public SettingsView()
        {
            this.DataContext = ViewModel;
            InitializeComponent();
        }

        private void btnOpenFileFinalClassification_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                var path = dialog.SelectedPath;
                ViewModel.Settings.FinalClassificationSaveLocation = path;
            }
        }

        private void btnOpenFileMotion_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                var path = dialog.SelectedPath;
                ViewModel.Settings.MotionDataSaveLocation = path;
            }
        }
    }
}
