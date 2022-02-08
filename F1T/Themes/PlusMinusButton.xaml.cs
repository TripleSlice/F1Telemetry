using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace F1T.Themes
{
    /// <summary>
    /// Interaction logic for PlusMinusButton.xaml
    /// </summary>
    public partial class PlusMinusButton : UserControl
    {

        SolidColorBrush DarkRed = new SolidColorBrush(Color.FromRgb(71, 0, 0));

        public PlusMinusButton()
        {
            InitializeComponent();
            DisplayTextBox.DataContext = this;
        }




        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(int), typeof(PlusMinusButton),
            new PropertyMetadata(0, ValuePropertyChanged));

        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        private void ValuePropertyChanged(int n)
        {
            Value = n;
        }

        private static void ValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((PlusMinusButton)d).ValuePropertyChanged((int)e.NewValue);
        }



        public static readonly DependencyProperty MaxProperty =
            DependencyProperty.Register("Max", typeof(int), typeof(PlusMinusButton),
            new PropertyMetadata(int.MaxValue, MaxPropertyChanged));

        public int Max
        {
            get { return (int)GetValue(MaxProperty); }
            set { SetValue(MaxProperty, value); }
        }

        private void MaxPropertyChanged(int n)
        {
            Max = n;
        }

        private static void MaxPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((PlusMinusButton)d).MaxPropertyChanged((int)e.NewValue);
        }



        public static readonly DependencyProperty MinProperty =
            DependencyProperty.Register("Min", typeof(int), typeof(PlusMinusButton),
            new PropertyMetadata(0, MinPropertyChanged));

        public int Min
        {
            get { return (int)GetValue(MinProperty); }
            set { SetValue(MinProperty, value); }
        }

        private void MinPropertyChanged(int n)
        {
            Min = n;
        }

        private static void MinPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((PlusMinusButton)d).MinPropertyChanged((int)e.NewValue);
        }


        private async void ValueDownButton_Click(object sender, RoutedEventArgs e)
        {
            if (Value - 1 < Min)
            {
                ValueDownBorder.Background = DarkRed;
                await Task.Delay(100);
                ValueDownBorder.Background = Brushes.Transparent;
                return;
            }
            Value--;

            ValueDownBorder.Background = Brushes.Gray;
            await Task.Delay(100);
            ValueDownBorder.Background = Brushes.Transparent;
        }

        private async void ValueUpButton_Click(object sender, RoutedEventArgs e)
        {
            if (Value + 1 > Max)
            {
                ValueUpBorder.Background = DarkRed;
                await Task.Delay(100);
                ValueUpBorder.Background = Brushes.Transparent;
                return;
            }

            Value++;

            ValueUpBorder.Background = Brushes.Gray;
            await Task.Delay(100);
            ValueUpBorder.Background = Brushes.Transparent;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.-]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void DisplayTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var text = DisplayTextBox.Text;
            int num = 0;

            try
            {
                num = Int32.Parse(text);
            }
            catch (Exception)
            {
                Value = Min;
                DisplayTextBox.Text = Min.ToString();
                return;
            }

            if(num < Min)
            {
                if(Min < 0)
                {
                    num = 0;
                }
                else
                {
                    num = Min;
                }

            }else if(num > Max)
            {
                num = Max;
            }

            Value = num;
        }

        private void DisplayTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                DisplayTextBox.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        private void DisplayTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(DisplayTextBox.Text.Length >= 4)
            {
                DisplayTextBox.FontSize = 13;
            }
            else
            {
                DisplayTextBox.FontSize = 17;
            }
        }
    }
}
