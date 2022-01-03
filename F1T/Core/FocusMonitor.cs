using System;
using System.Windows;
using System.Windows.Automation;
using System.Diagnostics;
using System.Windows.Threading;
using System.Threading;
using System.Text;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using F1T.MVVM.ViewModels;

namespace F1T.Core
{
    public class FocusMonitor
    {
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);


        // A dictionary containing all Modules ViewModels and all Modules OverlayViews
        public static Dictionary<BaseModuleViewModel, Window> ViewModelAndOverlayView = new Dictionary<BaseModuleViewModel, Window>();


        // Checks if F1 or this app is focused
        private static bool F1Focused = false;

        private Timer timer;

        public static void SetModelsAndViews(Dictionary<BaseModuleViewModel, Window> ViewModelAndOverlayView)
        {
            FocusMonitor.ViewModelAndOverlayView = ViewModelAndOverlayView;
        }
        public FocusMonitor(Dictionary<BaseModuleViewModel, Window> ViewModelAndOverlayView)
        {
            FocusMonitor.SetModelsAndViews(ViewModelAndOverlayView);
            AutomationFocusChangedEventHandler focusHandler = OnFocusChanged;
            Automation.AddAutomationFocusChangedEventHandler(focusHandler);
            // How long to check the title of the window
            timer = new Timer(CheckForF1Window, null, 0, 10000);
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

        public static void HideOverlay(BaseModuleViewModel Model)
        {
            Window View;
            FocusMonitor.ViewModelAndOverlayView.TryGetValue(Model, out View);

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

        public static void DisplayOverlay(BaseModuleViewModel Model)
        {

            if (!F1Focused)
            {
                return;
            }

            Window View;
            FocusMonitor.ViewModelAndOverlayView.TryGetValue(Model, out View);

            if (View == null)
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
        private void DisplayOverlays()
        {
            foreach (KeyValuePair<BaseModuleViewModel, Window> entry in ViewModelAndOverlayView)
            {
                BaseModuleViewModel Model = entry.Key;
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
            foreach (KeyValuePair<BaseModuleViewModel, Window> entry in ViewModelAndOverlayView)
            {
                BaseModuleViewModel Model = entry.Key;
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
            if (currWindowName == null)
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
            catch (Exception ex) { }
        }
    }
}
