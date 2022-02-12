using F1T.MVVM.ViewModels;
using F1T.Core;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Diagnostics;
using System.Windows.Threading;
using F1T.MVVM.Views.InputTelemetry;
using System.Windows.Input;
using System.Reflection;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Text;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using F1T.Settings;

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

            if (assemblyName.CultureInfo == null) return null;

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


        public MainWindow()
        {
            InitializeComponent();
            Closing += OnWindowclose;
            // set our model to our current datacontext
            this.DataContext = Model;
        }


        private void OnWindowclose(object sender, EventArgs e)
        {
            Model.SaveSettings();
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

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
