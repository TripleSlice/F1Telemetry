using F1T.MVVM.ViewModels;
using F1T.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace F1T.MVVM.Views
{
    /// <summary>
    /// Interaction logic for CommonSettingUserControl.xaml
    /// </summary>
    /// We make properties available on a CommonSettingUserControl that way they
    /// can be "passed along" to the desired modules ViewModel
    public partial class CommonSettingUserControl : UserControl
    {

        private int _maxScalePercentage = 200;
        public int MaxScalePercentage
        {
            get { return _maxScalePercentage; }
            set { _maxScalePercentage = value; }
        }


        private int _minScalePercentage = 25;
        public int MinScalePercentage
        {
            get { return _minScalePercentage; }
            set { _minScalePercentage = value; }
        }

        // BINDING FOR TOGGLING MODULE
        public static readonly RoutedEvent ToggleModuleEvent = EventManager.RegisterRoutedEvent(
            "ToggleModule", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ToggleButton));

            public event RoutedEventHandler ToggleModule
            {
                add { AddHandler(ToggleModuleEvent, value); }
                remove { RemoveHandler(ToggleModuleEvent, value); }
            }

            private void VisibilityButtonInstance_Click(object sender, RoutedEventArgs e)
            {
                RoutedEventArgs newEventArgs = new RoutedEventArgs(ToggleModuleEvent);
                RaiseEvent(newEventArgs);
            }

        // BINDING FOR OPACITY SLIDER
        public static readonly DependencyProperty OpacitySliderValueProperty =
            DependencyProperty.Register("OpacitySliderValue", typeof(int), typeof(CommonSettingUserControl),
            new PropertyMetadata(0, OpacitySliderValuePropertyChanged));

            public int OpacitySliderValue
            {
                get { return (int)GetValue(OpacitySliderValueProperty); }
                set { SetValue(OpacitySliderValueProperty, value);}
            }

            private void OpacitySliderValuePropertyChanged(int val)
            {
                OpacitySliderValue = val;
            }

            private static void OpacitySliderValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((CommonSettingUserControl)d).OpacitySliderValuePropertyChanged((int)e.NewValue);
            }

        // BINDING FOR SCALE SLIDER
        public static readonly DependencyProperty ScaleSliderValueProperty =
            DependencyProperty.Register("ScaleSliderValue", typeof(int), typeof(CommonSettingUserControl),
            new PropertyMetadata(0, ScaleSliderValuePropertyChanged));

            public int ScaleSliderValue
            {
                get { return (int)GetValue(ScaleSliderValueProperty); }
                set { SetValue(ScaleSliderValueProperty, value); }
            }

            private void ScaleSliderValuePropertyChanged(int val)
            {
                if(MaxScalePercentage < ScaleSliderValue) ScaleSliderValue = MaxScalePercentage;
                else if (MinScalePercentage > ScaleSliderValue) ScaleSliderValue = MinScalePercentage;
                else ScaleSliderValue = val;

                var plusTen = true;
                var plusOne = true;
                var minusOne = true;
                var minusTen = true;

                if(ScaleSliderValue + 10 > MaxScalePercentage) plusTen = false;
                if(ScaleSliderValue + 1 > MaxScalePercentage) plusOne = false;
                if(ScaleSliderValue - 1 < MinScalePercentage) minusOne = false;
                if(ScaleSliderValue - 10 < MinScalePercentage) minusTen = false;

                PlusTenButton.IsEnabled = plusTen;
                MinusTenButton.IsEnabled = minusTen;
                PlusOneButton.IsEnabled = plusOne;
                MinusOneButton.IsEnabled = minusOne;
            }

            private static void ScaleSliderValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((CommonSettingUserControl)d).ScaleSliderValuePropertyChanged((int)e.NewValue);
            }

        // BINDING FOR MAX FPS
        public static readonly DependencyProperty MaxFPSValueProperty =
            DependencyProperty.Register("MaxFPSValue", typeof(int), typeof(CommonSettingUserControl),
            new PropertyMetadata(0, MaxFPSValuePropertyChanged));

            public int MaxFPSValue
            {
                get { return (int)GetValue(MaxFPSValueProperty); }
                set { SetValue(MaxFPSValueProperty, value); }
            }

            private void MaxFPSValuePropertyChanged(int val)
            {
                MaxFPSValue = val;
            }

            private static void MaxFPSValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((CommonSettingUserControl)d).MaxFPSValuePropertyChanged((int)e.NewValue);
            }


        // BINIDNG FOR MIN FPS
        public static readonly DependencyProperty MinFPSValueProperty =
            DependencyProperty.Register("MinFPSValue", typeof(int), typeof(CommonSettingUserControl),
            new PropertyMetadata(0, MinFPSValuePropertyChanged));

            public int MinFPSValue
            {
                get { return (int)GetValue(MinFPSValueProperty); }
                set { SetValue(MinFPSValueProperty, value); }
            }

            private void MinFPSValuePropertyChanged(int val)
            {
                MinFPSValue = val;
            }

            private static void MinFPSValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((CommonSettingUserControl)d).MinFPSValuePropertyChanged((int)e.NewValue);
            }

        // BINDING FOR FPS
        public static readonly DependencyProperty FPSValueProperty =
            DependencyProperty.Register("FPSValue", typeof(int), typeof(CommonSettingUserControl),
            new PropertyMetadata(0, FPSValuePropertyChanged));

            public int FPSValue
            {
                get { return (int)GetValue(FPSValueProperty); }
                set { SetValue(FPSValueProperty, value); }
            }

            private void FPSValuePropertyChanged(int val)
            {
                FPSValue = val;
            }

            private static void FPSValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((CommonSettingUserControl)d).FPSValuePropertyChanged((int)e.NewValue);
            }

        // BINDING FOR LEFT
        public static readonly DependencyProperty LeftProperty =
            DependencyProperty.Register("Left", typeof(int), typeof(CommonSettingUserControl),
            new PropertyMetadata(0, LeftPropertyChanged));

            public int Left
            {
                get { return (int)GetValue(LeftProperty); }
                set { SetValue(LeftProperty, value); }
            }

            private void LeftPropertyChanged(int val)
            {
                Left = val;
            }

            private static void LeftPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((CommonSettingUserControl)d).LeftPropertyChanged((int)e.NewValue);
            }

        // BINDING FOR TOP
        public static readonly DependencyProperty TopProperty =
            DependencyProperty.Register("Top", typeof(int), typeof(CommonSettingUserControl),
            new PropertyMetadata(0, TopPropertyChanged));

            public int Top
            {
                get { return (int)GetValue(TopProperty); }
                set { SetValue(TopProperty, value); }
            }

            private void TopPropertyChanged(int val)
            {
                Top = val;
            }

            private static void TopPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((CommonSettingUserControl)d).TopPropertyChanged((int)e.NewValue);
            }

        // BINDING FOR TOGGLED
        public static readonly DependencyProperty ToggledValueProperty =
            DependencyProperty.Register("ToggledValue", typeof(bool), typeof(CommonSettingUserControl),
            new PropertyMetadata(false, ToggledValuePropertyChanged));

            public bool ToggledValue
            {
                get { return (bool)GetValue(ToggledValueProperty); }
                set { SetValue(ToggledValueProperty, value); }
            }

            private void ToggledValuePropertyChanged(bool boolean)
            {
                ToggledValue = boolean;
            }

            private static void ToggledValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((CommonSettingUserControl)d).ToggledValuePropertyChanged((bool)e.NewValue);
            }

        // BINDING FOR AUTO TOGGLED
        public static readonly DependencyProperty AutoToggledValueProperty =
            DependencyProperty.Register("AutoToggledValue", typeof(bool), typeof(CommonSettingUserControl),
            new PropertyMetadata(false, AutoToggledValuePropertyChanged));

            public bool AutoToggledValue
            {
                get { return (bool)GetValue(AutoToggledValueProperty); }
                set { SetValue(AutoToggledValueProperty, value); }
            }

            private void AutoToggledValuePropertyChanged(bool boolean)
            {
                AutoToggledValue = boolean;
            }

            private static void AutoToggledValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((CommonSettingUserControl)d).AutoToggledValuePropertyChanged((bool)e.NewValue);
            }


        // BINDING FOR LOCKED
        public static readonly DependencyProperty LockedValueProperty =
            DependencyProperty.Register("LockedValue", typeof(bool), typeof(CommonSettingUserControl),
            new PropertyMetadata(false, LockedValuePropertyChanged));

            public bool LockedValue
            {
                get { return (bool)GetValue(LockedValueProperty); }
                set { SetValue(LockedValueProperty, value); }
            }

            private void LockedValuePropertyChanged(bool boolean)
            {
                LockedValue = boolean;
            }

            private static void LockedValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((CommonSettingUserControl)d).LockedValuePropertyChanged((bool)e.NewValue);
            }

        public CommonSettingUserControl()
        {
            InitializeComponent();

            // I don't know why I have to INIT each one of these
            // instead of doing this.Datacontext = this
            // because for whatever reason it does not work
            FPSButtonInstance.DataContext = this;
            ToggleButtonInstance.DataContext = this;
            AutoToggleButtonInstance.DataContext = this;
            OpacitySliderInstance.DataContext = this;
            ScaleSliderInstance.DataContext = this;
            XButtonInstance.DataContext = this;
            YButtonInstance.DataContext = this;
            LockedToggleButtonInstance.DataContext= this;
        }

        private void ScaleButton_Click(object sender, RoutedEventArgs e)
        {
            var val = Int32.Parse(((Button)sender).Tag.ToString());

            ScaleSliderValue += val;
        }
    }
}
