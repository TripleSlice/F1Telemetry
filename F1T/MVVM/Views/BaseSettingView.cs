using F1T.Core;
using F1T.MVVM.ViewModels;
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
    public abstract class BaseSettingView<T> : UserControl where T : BaseModuleViewModel
    {
        // protected abstract meaning it must be overridden when we implement this class
        public abstract T Model { get; }
        protected abstract ToggleButton VisibilityButton { get; }
        protected abstract Slider OpacitySlider { get; }


        // Function logic for all SettingViews
        public void ToggleVisibilityButton_Click(object sender, RoutedEventArgs e)
        {
            if (Model.Toggled) { FocusMonitor.HideOverlay(Model); }
            else { FocusMonitor.DisplayOverlay(Model); }

            Model.Toggled = !Model.Toggled;
        }
    }
}
