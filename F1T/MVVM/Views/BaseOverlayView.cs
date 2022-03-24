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
    /// <summary>
    /// Abstract class which provides common functionality to all OverlayViews
    /// </summary>
    public abstract class BaseOverlayView : UserControl
    {
        // Relating to the "refresh rate" of the OverlayView
        protected Timer timer;
        protected int currentFrequency;

        public void StopTimer()
        {
            if (timer != null) timer.Dispose();
        }
        protected abstract void UpdateValues(object state = null);

    }
    /// <summary>
    /// Abstract class which provides commmon functionality to all OverlayViews that require a <see cref="BaseModuleViewModel"/> and a <see cref="BaseSettings"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="S"></typeparam>
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

        /// <summary>
        /// Updates the frequency that <see cref="BaseOverlayView.UpdateValues(object)"/> runs if required
        /// </summary>
        public virtual void UpdateTimer()
        {
            if (Model.Settings.Frequency != currentFrequency)
            {
                StopTimer();
                StartTimer();
            }
        }

        public void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // TODO
            // Remove try catch and handle this properly
            // Sometimes when moving the window, and error occurs

            if (Model.Settings.Locked) return;

            try
            {
                // Get window associated with object being clicked
                DependencyObject element = (DependencyObject)sender;
                Window window = Window.GetWindow(element);

                if (e.ChangedButton == MouseButton.Left) window.DragMove();
            }
            catch (Exception) { }
        }
    }
}
