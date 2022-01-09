using F1T.Core;
using F1T.MVVM.ViewModels;
using F1T.Themes;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace F1T.MVVM.Views
{
    // Stolen from here... Don't really understand how the functions inside ISettingViewHelper are ever executed...
    // https://stackoverflow.com/questions/30322008/default-implementation-of-a-method-for-c-sharp-interfaces
    // Something to do with 'Extension Methods'
    // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/extension-methods
    public interface ISettingView
    {
        // Fields and Methods which must exist on ALL SettingViews
        BaseModuleViewModel Model { get; }
        void OnToggleVisibilityButton_Click(object sender, RoutedEventArgs e);
        void ViewToggleVisibilityButton_Click(object sender, RoutedEventArgs e);
        ToggleButton VisibilityButton { get; }

    }

    public static class ISettingViewHelper
    {
        public static void ToggleVisibilityButton_Click(this ISettingView iSettingView, object sender, RoutedEventArgs e)
        {

            if (iSettingView.Model.Toggled)
            {
                FocusMonitor.HideOverlay(iSettingView.Model);
            }
            else
            {
                FocusMonitor.DisplayOverlay(iSettingView.Model);
            }


            // TODO Applying styling to the button to make it clear if the overlay is toggled or not...
            //iSettingView.VisibilityButton.IsEnabled = false;

            iSettingView.Model.Toggled = !iSettingView.Model.Toggled;
            iSettingView.OnToggleVisibilityButton_Click(sender, e);
        }
    }
}
