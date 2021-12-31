using F1T.Core;
using F1T.MVVM.ViewModels;
using System.Windows;

namespace F1T.MVVM.Views
{
    public interface ISettingView
    {
        // Fields and Methods which must exist on ALL SettingViews
        BaseModuleViewModel Model { get; }
        void OnToggleVisibilityButton_Click(object sender, RoutedEventArgs e);
    }

    public static class ISettingViewHelper
    {
        public static void ToggleVisibilityButton_Click(this ISettingView iSettingView, object sender, RoutedEventArgs e)
        {

            // TODO This logic is wrong, that and the FocusMonitor logic
            // desired behaviour is when the button is pressed
            // the overlay is only shown when the game is up...
            if (iSettingView.Model.Toggled)
            {
                FocusMonitor.HideOverlay(iSettingView.Model);
            }
            else
            {
                FocusMonitor.DisplayOverlay(iSettingView.Model);
            }

            iSettingView.Model.OverlayVisible = !iSettingView.Model.OverlayVisible;
            iSettingView.Model.Toggled = !iSettingView.Model.Toggled;

            iSettingView.OnToggleVisibilityButton_Click(sender, e);
        }
    }
}
