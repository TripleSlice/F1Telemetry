using F1T.MVVM.ViewModels;
using F1T.MVVM.Views.InputTelemetry;
using F1T.Settings;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace F1T.MVVM.Views
{
    public abstract class BaseOverlayView : UserControl
    {
        protected Timer timer;
        protected int currentFrequency;

        public void StopTimer()
        {
            if (timer != null) timer.Dispose();
        }
        protected abstract void UpdateValues(object state = null);

        public void Window_MouseDown(object sender, MouseButtonEventArgs e)
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
        }
    }
    public abstract class BaseOverlayView<T, S> : BaseOverlayView where T : BaseModuleViewModel<S> where S : BaseSettings
    {
        // protected abstract meaning it must be overridden when we implement this class
        public abstract T Model { get; }

        // Function logic for all OverlayViews
        public virtual void StartTimer()
        {
            timer = new Timer(UpdateValues, null, 0, Model.Settings.Frequency);
            currentFrequency = Model.Settings.Frequency;
        }
    }
}
