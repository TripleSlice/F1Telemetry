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
    /// <summary>
    /// Provides methods and utilites to see what <see cref="Window"/> is focused
    /// </summary>
    /// This class is highly specific to this application
    /// It would be a good idea to make a more generic FocusMonitor as
    /// this breaks frequently in this application
    public class FocusMonitor
    {
        // A dictionary containing all Modules ViewModels and associated Windows (which Module UserControls are contained)
        public static Dictionary<BaseModuleViewModel, Window> ViewModelAndOverlayWindow = new Dictionary<BaseModuleViewModel, Window>();

        // Flags for states of application
        private static bool F1Focused = false;
        private static bool ModulesDisplayed = false;
        private string LastWindow = "";

        // Prevent from being garbage collected
        private Timer timer;

        /// <summary>
        /// Statically set FocusMonitors ViewModel and OverlayWindow instances
        /// </summary>
        /// <param name="ViewModelAndOverlayWindow"></param>
        public static void SetModelsAndViews(Dictionary<BaseModuleViewModel, Window> ViewModelAndOverlayWindow)
        {
            FocusMonitor.ViewModelAndOverlayWindow = ViewModelAndOverlayWindow;
        }
        /// <summary>
        /// Initializes a new instance of <see cref="FocusMonitor"/> with the ViewModels and UserControls we might want to display
        /// </summary>
        /// <param name="ViewModelAndOverlayUserControl"></param>
        public FocusMonitor(Dictionary<BaseModuleViewModel, UserControl> ViewModelAndOverlayUserControl)
        {
            // We are taking our ViewModels and UserControls
            // And assigning all the UserControls to a "OverlayWindow"
            // OverlayWindow is responsible for actually holding the UserControl
            // and has some generic functionilty which all OverlayViews would want
            Dictionary<BaseModuleViewModel, Window> ViewModelAndContainerWindows = new Dictionary<BaseModuleViewModel, Window>();

            foreach (KeyValuePair<BaseModuleViewModel, UserControl> entry in ViewModelAndOverlayUserControl)
            {
                // Create our Window instance and store it
                Window display = new OverlayContainer(entry.Value, entry.Key);
                ViewModelAndContainerWindows.Add(entry.Key, display);
            }

            // Statically set our Windows
            SetModelsAndViews(ViewModelAndContainerWindows);
            // TIMER BASED
            // Check to see if a Window of interest is open on a timer
            timer = new Timer(CheckActiveWindow, null, 0, 1000);
        }

        /// <summary>
        /// <para> Performs the <see cref="Window.Hide"/> method and sets <see cref="Window.Topmost"/> to False on the <see cref="Window"/>. </para>
        /// <para> Sets <see cref="BaseModuleViewModel.BaseModuleViewModel"/> to False on the <see cref="BaseModuleViewModel"/> specified. </para>
        /// </summary>
        /// <param name="View"></param>
        /// <param name="Model"></param>
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

        /// <summary>
        /// <para> Performs the <see cref="Window.Show"/> method and sets <see cref="Window.Topmost"/> to True on the <see cref="Window"/>. </para>
        /// <para> Sets <see cref="BaseModuleViewModel.BaseModuleViewModel"/> to True on the <see cref="BaseModuleViewModel"/> specified. </para>
        /// </summary>
        /// <param name="View"></param>
        /// <param name="Model"></param>
        private static void PerformDisplayAction(Window View, BaseModuleViewModel Model)
        {
            Application.Current.Dispatcher.BeginInvoke(
                DispatcherPriority.Normal,
                new Action(() => {
                    View.Show();
                    // I know this seems pointless... But it drags it to the front and then
                    // makes it so others can go ontop
                    View.Topmost = true;
                    View.Topmost = false;
                    Model.OverlayVisible = true;
                }));
        }

        /// <summary>
        /// Toggles the <see cref="Window.Topmost"/> to True and then False on the <see cref="Window"/>.
        /// </summary>
        /// <param name="View"></param>
        private static void PerformDisplayActionRefresh(Window View)
        {
            Application.Current.Dispatcher.BeginInvoke(
                DispatcherPriority.Normal,
                new Action(() => {
                    // I know this seems pointless...
                    // But it drags it to the front and then makes it so others can go ontop
                    View.Topmost = true;
                    View.Topmost = false;
                }));
        }

        /// <summary>
        /// Hides the Overlay corresponding with the specified <see cref="BaseModuleViewModel"/>
        /// </summary>
        /// <param name="Model"></param>
        public static void HideOverlay(BaseModuleViewModel Model)
        {
            Window View;
            ViewModelAndOverlayWindow.TryGetValue(Model, out View);

            if (View == null) return;

            PerformHideAction(View, Model);
        }
        /// <summary>
        /// Displays the Overlay corresponding with the specified <see cref="BaseModuleViewModel"/>
        /// </summary>
        /// <param name="Model"></param>
        public static void DisplayOverlay(BaseModuleViewModel Model)
        {
            Window View;
            ViewModelAndOverlayWindow.TryGetValue(Model, out View);

            if (View == null) return;

            PerformDisplayAction(View, Model);
        }
        /// <summary>
        /// Calls <see cref="PerformDisplayActionRefresh(Window)"/> on all <see cref="Window"/> located in <see cref="FocusMonitor.ViewModelAndOverlayWindow"/>
        /// </summary>
        private void DisplayOverlaysRefresh()
        {
            foreach (KeyValuePair<BaseModuleViewModel, Window> entry in ViewModelAndOverlayWindow)
            {
                Window View = entry.Value;
                PerformDisplayActionRefresh(View);
            }
        }
        /// <summary>
        /// Calls <see cref="PerformDisplayAction(Window, BaseModuleViewModel)"/> on all <see cref="Window"/> located in <see cref="FocusMonitor.ViewModelAndOverlayWindow"/> if applicable
        /// </summary>
        private void DisplayOverlays()
        {
            foreach (KeyValuePair<BaseModuleViewModel, Window> entry in ViewModelAndOverlayWindow)
            {
                BaseModuleViewModel Model = entry.Key;
                Window View = entry.Value;

                if (Model.Toggled && !Model.OverlayVisible) PerformDisplayAction(View, Model);
            }
        }
        /// <summary>
        /// Calls <see cref="PerformHideAction(Window, BaseModuleViewModel)"/> on all <see cref="Window"/> located in <see cref="FocusMonitor.ViewModelAndOverlayWindow"/>
        /// </summary>
        private void HideOverlays()
        {
            foreach (KeyValuePair<BaseModuleViewModel, Window> entry in ViewModelAndOverlayWindow)
            {
                BaseModuleViewModel Model = entry.Key;
                Window View = entry.Value;

                if (Model.OverlayVisible) PerformHideAction(View, Model);
            }
        }

        /// <summary>
        /// Returns a <see cref="Boolean"/> if a F1 related application is focused
        /// </summary>
        /// <param name="currWindowName"></param>
        /// <returns></returns>
        private bool IsF1Focussed(string currWindowName)
        {
            if (currWindowName == null) return false;
            return (currWindowName.StartsWith("F1 2021") || currWindowName.StartsWith("F1_2021") || currWindowName.StartsWith("F1T"));
        }

        /// <summary>
        /// Checks the active window and performs logic depending on which window is active
        /// </summary>
        /// <param name="state"></param>
        private void CheckActiveWindow(object state = null)
        {
            var CurrWindow = WindowHelper.GetActiveWindowTitle();
            var res = IsF1Focussed(CurrWindow);
            F1Focused = res;

            // This is here incase we switch from one F1 related window to the next
            // It would disapear, but we make it re-appear :)
            if(F1Focused && CurrWindow != LastWindow)
            {
                DisplayOverlaysRefresh();
            }else if (F1Focused && !ModulesDisplayed)
            {
                DisplayOverlays();
                ModulesDisplayed = true;
            }
            else if(!F1Focused && ModulesDisplayed)
            {
                HideOverlays();
                ModulesDisplayed = false;
            }

            LastWindow = WindowHelper.GetActiveWindowTitle();
        }
    }

    /// <summary>
    /// Contains methods and functionalty to get the title of the active <see cref="Window"/>
    /// </summary>
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

            if (GetWindowText(handle, Buff, nChars) > 0) return Buff.ToString();
            return null;
        }
    }
}


// FOCUS BASED CODE (SORTA YUCKY TO HAVE BOTH)
// Here incase need to switch
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