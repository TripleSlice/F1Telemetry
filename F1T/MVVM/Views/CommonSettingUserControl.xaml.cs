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
    public partial class CommonSettingUserControl : UserControl
    {
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




        public CommonSettingUserControl()
        {
            InitializeComponent();
            FPSButtonInstance.DataContext = this;
        }
    }
}
