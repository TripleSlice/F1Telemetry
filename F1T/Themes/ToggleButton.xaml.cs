using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.ComponentModel;

namespace F1T.Themes
{
    /// <summary>
    /// Interaction logic for ToggleButton.xaml
    /// </summary>
    public partial class ToggleButton : UserControl
    {
        // Thickness LeftSide = new Thickness(-69, 0, 0, 0);
        // Thickness RightSide = new Thickness(0, 0, -69, 0);

        SolidColorBrush Black = new SolidColorBrush(Color.FromRgb(0, 0, 0));
        SolidColorBrush DarkGray = new SolidColorBrush(Color.FromRgb(130, 130, 130));
        SolidColorBrush White = new SolidColorBrush(Color.FromRgb(255, 255, 255));

        Duration AnimationDuration;
        DoubleAnimation OnAnimation;
        DoubleAnimation OffAnimation; 

        public ToggleButton()
        {
            InitializeComponent();

            PreviewMouseLeftButtonUp += (sender, args) => OnClick();

            AnimationDuration = new Duration(TimeSpan.FromSeconds(0.5));
            OnAnimation = new DoubleAnimation(-34.5, 34.5, AnimationDuration);
            OffAnimation = new DoubleAnimation(34.5, -34.5, AnimationDuration);
            ColorAndAnimationLogic();
        }



        // Click stuff incase we want to implement Click="Function" in XAML
        // NOTE: Does not work with ISettingView or IOverlayView functions becuase they are 'Extension Methods'
        // not really sure why...
        public static readonly RoutedEvent ClickEvent = EventManager.RegisterRoutedEvent(
        "Click", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ToggleButton));


        public event RoutedEventHandler Click
        {
            add { AddHandler(ClickEvent, value); }
            remove { RemoveHandler(ClickEvent, value); }
        }

        void RaiseClickEvent()
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(ToggleButton.ClickEvent);
            RaiseEvent(newEventArgs);
        }

        void OnClick()
        {
            // THIS MUST BE ABOVE THE OTHER LINE
            Toggled = !Toggled;
            RaiseClickEvent();
        }

        public static readonly DependencyProperty ToggledProperty =
            DependencyProperty.Register("Toggled", typeof(bool), typeof(ToggleButton),
            new PropertyMetadata(false, ToggledPropertyChanged));

        public bool Toggled
        {
            get { return (bool)GetValue(ToggledProperty); }
            set {
                ColorAndAnimationLogic();
                SetValue(ToggledProperty, value);
            }
        }

        private void ToggledPropertyChanged(bool boolean)
        {
            Toggled = boolean;
        }

        private static void ToggledPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ToggleButton)d).ToggledPropertyChanged((bool)e.NewValue);
        }

        private void ColorAndAnimationLogic()
        {
            if (!Toggled)
            {
                //Pill.Margin = RightSide;
                Pill.Fill = DarkGray;
                Overlay.Opacity = 0.5f;
                OnLabel.Foreground = White;

                Pill.RenderTransform = new TranslateTransform(0,0);
                Pill.RenderTransform.BeginAnimation(TranslateTransform.XProperty, OnAnimation);
            }
            else
            {
                //Pill.Margin = LeftSide;
                Pill.Fill = White;
                Overlay.Opacity = 0.0f;
                OnLabel.Foreground = Black;

                Pill.RenderTransform = new TranslateTransform(0,0);
                Pill.RenderTransform.BeginAnimation(TranslateTransform.XProperty, OffAnimation);
            }
        }
    }
}
