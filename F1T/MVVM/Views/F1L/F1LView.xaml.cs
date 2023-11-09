using F1T.MVVM.ViewModels;
using F1T.Settings;
using System.Windows;
using System.Windows.Controls;
using System.IO;
namespace F1T.MVVM.Views.Settings
{
    /// <summary>
    /// Interaction logic for F1LView.xaml
    /// </summary>
    public partial class F1LView : UserControl
    {


        public F1LViewModel ViewModel = F1LViewModel.GetInstance();
        public F1LView()
        {
            this.DataContext = ViewModel;
            InitializeComponent();
        }

        public SettingsSettings Settings = SettingsViewModel.GetInstance().Settings;

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.LoginAsync();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Logout();
        }

        //FinalClassificationSaveLocation
        // MotionDataSaveLocation
        public void SelectFormationLapReplayButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog fileDialog = new System.Windows.Forms.OpenFileDialog();
            fileDialog.DefaultExt = ".json";
            fileDialog.Filter = "Json (.json)|*.json";
            fileDialog.InitialDirectory = Settings.MotionDataSaveLocation;

            if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ViewModel.FormationLapReplayFileLocation = fileDialog.FileName;
            }
        }

        public void SelectRaceReplayButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog fileDialog = new System.Windows.Forms.OpenFileDialog();
            fileDialog.DefaultExt = ".json";
            fileDialog.Filter = "Json (.json)|*.json";
            fileDialog.InitialDirectory = Settings.MotionDataSaveLocation;

            if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ViewModel.RaceReplayFileLocation = fileDialog.FileName;
            }
        }

        public void SelectQualifyingResultsButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog fileDialog = new System.Windows.Forms.OpenFileDialog();
            fileDialog.DefaultExt = ".json";
            fileDialog.Filter = "Json (.json)|*.json";
            fileDialog.InitialDirectory = Settings.FinalClassificationSaveLocation;

            if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ViewModel.QualifyingResultsFileLocation = fileDialog.FileName;
            }
        }

        public void SelectRaceResultsButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog fileDialog = new System.Windows.Forms.OpenFileDialog();
            fileDialog.DefaultExt = ".json";
            fileDialog.Filter = "Json (.json)|*.json"; 
            fileDialog.InitialDirectory = Settings.FinalClassificationSaveLocation;

            if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ViewModel.RaceResultsFileLocation = fileDialog.FileName;
            }
        }

        public void UploadButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.UploadResults();
        }
    }
}
