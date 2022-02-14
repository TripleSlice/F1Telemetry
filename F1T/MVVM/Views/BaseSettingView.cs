using F1T.Core;
using F1T.MVVM.ViewModels;
using F1T.Settings;
using F1T.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace F1T.MVVM.Views
{
    /// <summary>
    /// Abstract class which provides common functionality to all SettingViews
    /// </summary>
    public abstract class BaseSettingView : UserControl
    {

    }
    /// <summary>
    /// Abstract class which provides commmon functionality to all SettingViews that require a <see cref="BaseModuleViewModel"/> and a <see cref="BaseSettings"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="S"></typeparam>
    public abstract class BaseSettingView<T, S> : BaseSettingView where T : BaseModuleViewModel<S> where S : BaseSettings
    {
        // ViewModel for each 
        public abstract T Model { get; }

        // Function logic for all SettingViews
        public void ToggleVisibilityButton_Click(object sender, RoutedEventArgs e)
        {
            if (Model.Settings.Toggled) FocusMonitor.HideOverlay(Model); 
            else FocusMonitor.DisplayOverlay(Model); 

            Model.Settings.Toggled = !Model.Settings.Toggled;
            Model.Toggled = !Model.Toggled;
        }
    }
}
