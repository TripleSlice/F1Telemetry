using F1T.Core;
using F1T.MVVM.Views.InputTelemetry;
using System.Collections.Generic;
using System.Windows.Controls;
using F1T.MVVM.Views.Home;
using F1T.MVVM.Views.Radar;
using F1T.MVVM.Views.Setup;
using F1T.MVVM.Views.RelativeInfo;
using F1T.Settings;
using F1T.MVVM.Views;
using System;


namespace F1T.MVVM.ViewModels
{
    /// <summary>
    /// ViewModel for the MainWindow
    /// </summary>
    public class MainViewModel : ObservableObject
    {

        // ============= CREATING NEW MODULES =============
        // TODO - Write how to create a new module


        // === COMMANDS ===
        // === Commands To Switch Views ===
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand InputTelemetrySettingViewCommand { get; set; }
        public RelayCommand FlagSettingViewCommand { get; set; }
        public RelayCommand RadarSettingViewCommand { get; set; }
        public RelayCommand RelativeInfoSettingViewCommand { get; set; }
        public RelayCommand SetupViewCommand { get; set; }

        // === VIEWS ===
        // === HomeView/SetupView Instance ===
        HomeView Home = new HomeView();
        SetupView Setup = new SetupView();
        // === SettingView Instances ===
        InputTelemetrySettingView InputTelemetrySetting = new InputTelemetrySettingView();
        RadarSettingView RadarSetting = new RadarSettingView();
        RelativeInfoSettingView RelativeInfoSetting = new RelativeInfoSettingView();

        // === OverlayView Instances ===
        InputTelemetryOverlayView InputTelemetryOverlay = new InputTelemetryOverlayView();
        RadarOverlayView RadarOverlay = new RadarOverlayView();
        RelativeInfoOverlayView RelativeInfoOverlay = new RelativeInfoOverlayView();


        // === VIEW MODELS ===
        // === View Models Associated with Views
        public InputTelemetryViewModel InputTelemetryModel { get { return InputTelemetryViewModel.GetInstance(); } }
        public RadarViewModel RadarModel { get { return RadarViewModel.GetInstance(); } }   
        public RelativeInfoViewModel RelativeInfoModel { get { return RelativeInfoViewModel.GetInstance(); } }


        // Dict of ViewModel and OverlayView
        Dictionary<BaseModuleViewModel, UserControl> ViewModelAndOverlayView = new Dictionary<BaseModuleViewModel, UserControl>();
        // Dict of ViewModel and SettingView
        Dictionary<BaseModuleViewModel, UserControl> ViewModelAndSettingView = new Dictionary<BaseModuleViewModel, UserControl>();


        private void Init()
        {
            // == HOME MODULE ==
            HomeViewCommand = new RelayCommand(obj => { CurrentView = Home; });

            // == INPUT TELEMETRY MODULE ==
            ViewModelAndOverlayView.Add(InputTelemetryModel, InputTelemetryOverlay);
            ViewModelAndSettingView.Add(InputTelemetryModel, InputTelemetrySetting);
            InputTelemetrySettingViewCommand = new RelayCommand(obj => { CurrentView = InputTelemetrySetting; });

            // == RADAR MODULE ==
            ViewModelAndOverlayView.Add(RadarModel, RadarOverlay);
            ViewModelAndSettingView.Add(RadarModel, RadarSetting);
            RadarSettingViewCommand = new RelayCommand(obj => { CurrentView = RadarSetting; });

            // == RELATIVE INFO MODULE ==
            ViewModelAndOverlayView.Add(RelativeInfoModel, RelativeInfoOverlay);
            ViewModelAndSettingView.Add(RelativeInfoModel, RelativeInfoSetting);
            RelativeInfoSettingViewCommand = new RelayCommand(obj => { CurrentView = RelativeInfoSetting; });

            // == SETUP MODULE ==
            SetupViewCommand = new RelayCommand(obj => { CurrentView = Setup; });
        }

        public void SaveSettings()
        {
            InputTelemetryModel.Settings.Save<InputTelemetrySettings>();
            RadarModel.Settings.Save<RadarSettings>();
            RelativeInfoModel.Settings.Save<RelativeInfoSettings>();
        }


        // === Singleton Instance with Thread Saftey ===
        private static MainViewModel _instance = null;
        private static object _singletonLock = new object();
        public static MainViewModel GetInstance()
        {
            lock (_singletonLock){
                if (_instance == null){ _instance = new MainViewModel(); }
                return _instance;
            }
        }


        // === Current View ===
        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set { SetField(ref _currentView, value, "CurrentView"); }
        }

        private MainViewModel()
        {
            // Init all OverlayViews, SettingViews, and ViewModels and link them
            Init();

            // == DEFAULT VIEW ON STARTUP ==
            // Set current view to default view
            CurrentView = Home;


            // INIT Static Classes
            // Create FocusMonitor to monitor application for when to display overlays
            FocusMonitor FocusMonitor = new FocusMonitor(ViewModelAndOverlayView);
            // Create and start UDP Connection to game on port 21777
            UDPConnection UDPConnection = UDPConnection.GetInstance();
        } 
    }
}
