using F1T.MVVM.ViewModels;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Diagnostics;
using System.Windows.Threading;
using F1T.MVVM.Views.InputTelemetry;
using F1T.MVVM.Views.Flags;
using System.Windows.Input;
using System.Reflection;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Text;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace F1T
{


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        /// <summary>
        /// Main entry point
        /// </summary>
        // https://stackoverflow.com/questions/1025843/merging-dlls-into-a-single-exe-with-wpf?fbclid=IwAR2vdTCV2W3k9I-p4kJkqBMMbhfw4vfKIoTFEJUBcXzBgcbkRUWOgzt2Ipw
        [STAThreadAttribute]
        public static void Main()
        {
            AppDomain.CurrentDomain.AssemblyResolve += OnResolveAssembly;
            App.Main();
        }

        /// <summary>
        /// Combines the .dll files to become one exe
        /// </summary>
        // This is also for exe stuff
        private static Assembly OnResolveAssembly(object sender, ResolveEventArgs args)
        {
            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            AssemblyName assemblyName = new AssemblyName(args.Name);

            var path = assemblyName.Name + ".dll";

            if (assemblyName.CultureInfo.Equals(CultureInfo.InvariantCulture) == false) path = String.Format(@"{0}\{1}", assemblyName.CultureInfo, path);

            using (Stream stream = executingAssembly.GetManifestResourceStream(path))
            {
                if (stream == null) return null;

                var assemblyRawBytes = new byte[stream.Length];
                stream.Read(assemblyRawBytes, 0, assemblyRawBytes.Length);
                return Assembly.Load(assemblyRawBytes);
            }
        }

        // === ViewModel ===
        MainViewModel Model = MainViewModel.GetInstance();

        // In charge of displaying the screens when required
        FocusMonitor fm = FocusMonitor.GetInstance();

        public MainWindow()
        {
            InitializeComponent();
            Closing += OnWindowclose;

            // set our model to our current datacontext
            this.DataContext = Model;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

 
        private void OnWindowclose(object sender, EventArgs e)
        {
            Environment.Exit(Environment.ExitCode); // Prevent memory leak
            System.Windows.Application.Current.Shutdown(); // Not sure if needed
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void FullscreenButton_Click(object sender, RoutedEventArgs e)
        {

            if (this.WindowState == WindowState.Normal)
            {
                MinMaxIcon.Text = "2";
                this.WindowState = WindowState.Maximized;
            }
            else
            {
                MinMaxIcon.Text = "1";
                this.WindowState = WindowState.Normal;
            }
        }
    }


    public class FocusMonitor
    {
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        // === ViewModels ===
        // FlagsViewModel FlagsViewModel = FlagsViewModel.GetInstance();
        InputTelemetryViewModel InputTelemetryModel = InputTelemetryViewModel.GetInstance();

        // === All Popout Views ===
        InputTelemetryOverlayView InputTelemetryOverlay = new InputTelemetryOverlayView();
        FlagsOverlayView FlagsOverlay = new FlagsOverlayView();

        private Dictionary<BaseOverlayViewModel, Window> ModelsAndViews = new Dictionary<BaseOverlayViewModel, Window>();
        private void InitModelDict()
        {
            ModelsAndViews.Add(InputTelemetryModel, InputTelemetryOverlay);
            //ModelsAndViews.Add(FlagsViewModel, FlagsOverlay);
        }

        // Checks if F1 or this app is focused
        private bool F1Focused = false;

        private Timer timer;

        private static FocusMonitor _instance = null;
        private static object _singletonLock = new object();
        public static FocusMonitor GetInstance()
        {
            lock (_singletonLock)
            {
                if (_instance == null) { _instance = new FocusMonitor(); }
                return _instance;
            }
        }
        private FocusMonitor()
        {
            AutomationFocusChangedEventHandler focusHandler = OnFocusChanged;
            Automation.AddAutomationFocusChangedEventHandler(focusHandler);
            // How long to check the title of the window
            timer = new Timer(CheckForF1Window, null, 0, 10000);
            InitModelDict();
        }

        private string GetActiveWindowTitle()
        {
            const int nChars = 256;
            StringBuilder Buff = new StringBuilder(nChars);
            IntPtr handle = GetForegroundWindow();

            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                return Buff.ToString();
            }
            return null;
        }

        public static void HideOverlay(BaseOverlayViewModel Model)
        {
            FocusMonitor instance = FocusMonitor.GetInstance();
            Window View;
            instance.ModelsAndViews.TryGetValue(Model, out View);

            if (View == null)
            {
                return;
            }

            Application.Current.Dispatcher.BeginInvoke(
            DispatcherPriority.Background,
            new Action(() => {
                View.Hide();
                View.Topmost = false;
                Model.OverlayVisible = false;
            }));
        }

        public static void DisplayOverlay(BaseOverlayViewModel Model)
        {
            FocusMonitor instance = FocusMonitor.GetInstance();
            Window View;
            instance.ModelsAndViews.TryGetValue(Model, out View);

            if(View == null)
            {
                return;
            }

            Application.Current.Dispatcher.BeginInvoke(
            DispatcherPriority.Background,
            new Action(() => {
                View.Show();
                View.Topmost = true;
                Model.OverlayVisible = true;
            }));
        }

        // TODO Make these able to handle multiple windows at once...
        private void DisplayOverlays()
        {
            foreach (KeyValuePair<BaseOverlayViewModel, Window> entry in ModelsAndViews)
            {
                BaseOverlayViewModel Model = entry.Key;
                Window View = entry.Value;

                if (Model.Toggled && !Model.OverlayVisible)
                {
                    Application.Current.Dispatcher.BeginInvoke(
                        DispatcherPriority.Background,
                        new Action(() => {
                            View.Show();
                            View.Topmost = true;
                            Model.OverlayVisible = true;
                        }));
                }
            }
        }

        private void HideOverlays()
        {
            foreach (KeyValuePair<BaseOverlayViewModel, Window> entry in ModelsAndViews)
            {
                BaseOverlayViewModel Model = entry.Key;
                Window View = entry.Value;

                if (Model.Toggled && Model.OverlayVisible)
                {
                    Application.Current.Dispatcher.BeginInvoke(
                        DispatcherPriority.Background,
                        new Action(() => {
                            View.Hide();
                            View.Topmost = false;
                            Model.OverlayVisible = false;
                        }));
                }
            }
        }

        private void HandleWindow(string currWindowName)
        {
            if(currWindowName == null)
            {
                return;
            }

            if (!F1Focused && (currWindowName.StartsWith("F1 2021") || currWindowName.StartsWith("F1_2021")))
            {
                DisplayOverlays();
                F1Focused = true;
            }
            else if (F1Focused && !currWindowName.StartsWith("F1"))
            {
                HideOverlays();
                F1Focused = false;
            }
        }

        private void CheckForF1Window(object state = null)
        {
            HandleWindow(GetActiveWindowTitle());
        }

        private void OnFocusChanged(object sender, AutomationFocusChangedEventArgs e)
        {
            try
            {
                AutomationElement focusedElement = sender as AutomationElement;
                if (focusedElement != null)
                {
                    int processId = focusedElement.Current.ProcessId;
                    using (Process process = Process.GetProcessById(processId))
                    {
                        HandleWindow(process.ProcessName);
                    }
                }
            }
            catch (Exception ex){}
        }
    }
}
