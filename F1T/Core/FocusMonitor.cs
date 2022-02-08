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
using System.Windows.Controls;
using F1T.MVVM.Views;

namespace F1T.Core
{
    public class FocusMonitor
    {


        // A dictionary containing all Modules ViewModels and associated Windows (which UserControls are contained)
        public static Dictionary<BaseModuleViewModel, Window> ViewModelAndOverlayWindow = new Dictionary<BaseModuleViewModel, Window>();

        // Flags for states of application
        private static bool F1Focused = false;
        private static bool ModulesDisplayed = false;

        // Prevent from being garbage collected
        private Timer timer;

        public static void SetModelsAndViews(Dictionary<BaseModuleViewModel, Window> ViewModelAndOverlayWindow)
        {
            FocusMonitor.ViewModelAndOverlayWindow = ViewModelAndOverlayWindow;
        }

        public FocusMonitor(Dictionary<BaseModuleViewModel, UserControl> ViewModelAndOverlayUserControl)
        {
            Dictionary<BaseModuleViewModel, Window> ViewModelAndContainerWindows = new Dictionary<BaseModuleViewModel, Window>();

            foreach (KeyValuePair<BaseModuleViewModel, UserControl> entry in ViewModelAndOverlayUserControl)
            {

                // do something with entry.Value or entry.Key
                Window display = new OverlayContainer(entry.Value, entry.Key.Top, entry.Key.Left);
                display.Height = entry.Key.Height;
                display.Width = entry.Key.Width;
                ViewModelAndContainerWindows.Add(entry.Key, display);
            }

            SetModelsAndViews(ViewModelAndContainerWindows);
            // TIMER BASED
            timer = new Timer(CheckForF1Window, null, 0, 1000);
        }


        // TODO
        // Find a way to call StartTimer() and StopTimer() from here...
        private static void PerformHideAction(Window View, BaseModuleViewModel Model)
        {
            Application.Current.Dispatcher.BeginInvoke(
                DispatcherPriority.Normal,
                new Action(() => {
                    View.Hide();
                    View.Topmost = false;
                    Model.OverlayVisible = false;
                }));
        }

        private static void PerformDisplayAction(Window View, BaseModuleViewModel Model)
        {
            Application.Current.Dispatcher.BeginInvoke(
                DispatcherPriority.Normal,
                new Action(() => {
                    View.Show();
                    View.Topmost = true;
                    Model.OverlayVisible = true;
                }));
        }


        public static void HideOverlay(BaseModuleViewModel Model)
        {
            Window View;
            ViewModelAndOverlayWindow.TryGetValue(Model, out View);

            if (View == null)
            {
                return;
            }

            PerformHideAction(View, Model);

        }

        public static void DisplayOverlay(BaseModuleViewModel Model)
        {

            #if DEBUG
                Console.WriteLine("Overlay toggled regardless due to being in DEBUG mode..."); 
            #else
                if (!F1Focused)
                {
                    return;
                }
            #endif

            Window View;
            ViewModelAndOverlayWindow.TryGetValue(Model, out View);

            if (View == null)
            {
                return;
            }

            PerformDisplayAction(View, Model);
        }
        private void DisplayOverlays()
        {
            foreach (KeyValuePair<BaseModuleViewModel, Window> entry in ViewModelAndOverlayWindow)
            {
                BaseModuleViewModel Model = entry.Key;
                Window View = entry.Value;

                if (Model.Toggled && !Model.OverlayVisible)
                {
                    PerformDisplayAction(View, Model);
                }
            }
        }

        private void HideOverlays()
        {
            foreach (KeyValuePair<BaseModuleViewModel, Window> entry in ViewModelAndOverlayWindow)
            {
                BaseModuleViewModel Model = entry.Key;
                Window View = entry.Value;

                if (Model.OverlayVisible)
                {
                    PerformHideAction(View, Model);
                }
            }
        }

        private bool IsF1Focussed(string currWindowName)
        {
            if (currWindowName == null) return false;
            return (currWindowName.StartsWith("F1 2021") || currWindowName.StartsWith("F1_2021") || currWindowName.StartsWith("F1T"));
        }

        private void CheckForF1Window(object state = null)
        {
            var res = IsF1Focussed(WindowHelper.GetActiveWindowTitle());
            F1Focused = res;

            if (F1Focused && !ModulesDisplayed)
            {
                DisplayOverlays();
                ModulesDisplayed = true;
            }
            else if(!F1Focused && ModulesDisplayed)
            {
                HideOverlays();
                ModulesDisplayed = false;
            }
        }
    }

    public class WindowHelper
    {
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        public static string GetActiveWindowTitle()
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

    }
}


// FOCUS BASED CODE (SORTA YUCKY TO HAVE BOTH)
/*
AutomationFocusChangedEventHandler focusHandler = OnFocusChanged;
Automation.AddAutomationFocusChangedEventHandler(focusHandler);

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
    catch { }
}

*/