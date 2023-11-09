using F1T.MVVM.ViewModels;
using System.Windows;
using F1T.Settings;
using System.Windows.Forms;

namespace F1T.MVVM.Views.InputTelemetry
{
    /// <summary>
    /// Interaction logic for InputTelemetrySettingView.xaml
    /// </summary>
    public partial class InputTelemetrySettingView : BaseSettingView<InputTelemetryViewModel, InputTelemetrySettings>
    {
        public InputTelemetrySettingView()
        {
            InitializeComponent();
            this.DataContext = Model;
        }

        public override InputTelemetryViewModel Model { get => InputTelemetryViewModel.GetInstance(); }

        public void UploadWheel(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.DefaultExt = ".png";
            fileDialog.Filter = "Images (.png)|*.png";

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                Model.Settings.WheelName = fileDialog.FileName;
            }
        }

        public void SetDefaultWheel(object sender, RoutedEventArgs e)
        {
            Model.Settings.WheelName = "/Images/wheel.png";
        }

    }
}
