using F1T.Structs;
using F1T.PacketParsers;
using System;
using System.ComponentModel;
using System.Net.Sockets;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using F1T.MVVM.Models;
using System.Collections.ObjectModel;
using F1T.Core;
using F1T.MVVM.Views.InputTelemetry;
using System.Windows.Input;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using F1T.MVVM.Views.Home;
using F1T.MVVM.Views.Radar;

namespace F1T.MVVM.ViewModels
{
    public class MainViewModel : ObservableObject
    {

        // ============= CREATING NEW MODULES =============
        // Create new ViewModel class in ViewModels folder which extends BaseOverlayViewModel (Has to be done by a person)
        // Create ViewModel instance in FocusMonitor -> MainWindow.xaml.cs (This class?)

        // Create new two new View classes in Views/{module} (Has to be done by a person)
        // 1. SettingView
        // 2. OverlayView
        // Create OverlayView instance in FocusMonitor -> MainWindow.xaml.cs (This class?)

        // Create a new command which binds the SettingView to the CurrentView object on click -> MainViewModel.cs (This class?)
        // Bind command to button -> MainWindow.xaml (Has to be done by a person)


        // === COMMANDS ===
        // === Commands To Switch Views ===
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand InputTelemetrySettingViewCommand { get; set; }
        public RelayCommand FlagSettingViewCommand { get; set; }
        public RelayCommand RadarSettingViewCommand { get; set; }

        // === VIEWS ===
        // === HomeView Instance ===
        HomeView Home = new HomeView();
        // === SettingView Instances ===
        InputTelemetrySettingView InputTelemetrySetting = new InputTelemetrySettingView();
        RadarSettingView RadarSetting = new RadarSettingView();

        // === OverlayView Instances ===
        InputTelemetryOverlayView InputTelemetryOverlay = new InputTelemetryOverlayView();
        RadarOverlayView RadarOverlay = new RadarOverlayView();


        // === VIEW MODELS ===
        // === View Models Associated with Views
        InputTelemetryViewModel InputTelemetryModel = InputTelemetryViewModel.GetInstance();

        RadarViewModel RadarModel = RadarViewModel.GetInstance();


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


        // === Focus Monitor ===
        FocusMonitor FocusMonitor;

        // === UDP Connection ===
        UDPConnection UDPConnection;


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

            // Create FocusMonitor to monitor application for when to display overlays
            FocusMonitor = new FocusMonitor(ViewModelAndOverlayView);

            // Create and start UDP Connection to game on port 21777
            UDPConnection = new UDPConnection();
        } 
    }
}
