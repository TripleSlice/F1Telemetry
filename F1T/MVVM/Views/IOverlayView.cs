using F1T.Core;
using F1T.MVVM.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace F1T.MVVM.Views
{
    public interface IOverlayView
    {
        // Fields and Methods which must exist on ALL SettingViews
        BaseModuleViewModel Model { get; }
        void OnWindow_MouseDown(object sender, MouseButtonEventArgs e);
    }

    public static class IOverlayViewHelper
    {
        public static void Window_MouseDown(this IOverlayView iOverlayView, object sender, MouseButtonEventArgs e)
        {
            try
            {
                // Get window associated with object being clicked
                DependencyObject element = (DependencyObject)sender;
                Window window = Window.GetWindow(element);

                if (e.ChangedButton == MouseButton.Left)
                    window.DragMove();
            }
            catch (Exception) { }

            iOverlayView.OnWindow_MouseDown(sender, e);
        }
    }
}
