using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace F1T.Themes
{
    /// <summary>
    /// Interaction logic for ToggleButton.xaml
    /// </summary>
    public partial class ToggleButton : UserControl
    {
        Thickness LeftSide = new Thickness(-69, 0, 0, 0);
        Thickness RightSide = new Thickness(0, 0, -69, 0);


        SolidColorBrush Black = new SolidColorBrush(Color.FromRgb(0, 0, 0));
        SolidColorBrush DarkGray = new SolidColorBrush(Color.FromRgb(130, 130, 130));
        SolidColorBrush White = new SolidColorBrush(Color.FromRgb(255, 255, 255));



        public ToggleButton()
        {
            InitializeComponent();
            ColorLogic();
        }



        public static readonly DependencyProperty ToggledProperty =
            DependencyProperty.Register("Toggled", typeof(bool), typeof(ToggleButton),
            new PropertyMetadata(false, ToggledPropertyChanged));

        public bool Toggled
        {
            get { return (bool)GetValue(ToggledProperty); }
            set { SetValue(ToggledProperty, value); }
        }

        private void ToggledPropertyChanged(bool boolean)
        {
            Toggled = boolean;
        }

        private static void ToggledPropertyChanged(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ToggleButton)d).ToggledPropertyChanged((bool)e.NewValue);
        }

        private void ColorLogic()
        {
            if (!Toggled)
            {
                Pill.Margin = RightSide;
                Pill.Fill = DarkGray;
                Overlay.Opacity = 0.5f;
                OnLabel.Foreground = White;
            }
            else
            {
                Pill.Margin = LeftSide;
                Pill.Fill = White;
                Overlay.Opacity = 0.0f;
                OnLabel.Foreground = Black;
            }
        }
        private void Click_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ColorLogic();

            Toggled = !Toggled;
        }
    }
}
