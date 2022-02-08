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

namespace F1T.Themes
{
    /// <summary>
    /// Interaction logic for SettingLabel.xaml
    /// </summary>
    public partial class SettingLabel : UserControl
    {
        public SettingLabel()
        {
            InitializeComponent();
            this.DataContext = this;
        }



        public static readonly DependencyProperty TooltipContentProperty =
            DependencyProperty.Register("TooltipContent", typeof(string), typeof(SettingLabel),
            new PropertyMetadata("", TooltipContentPropertyChanged));

        public string TooltipContent
        {
            get { return (string)GetValue(TooltipContentProperty); }
            set {
                CreateTooltip();
                SetValue(TooltipContentProperty, value); 
            }
        }

        private void TooltipContentPropertyChanged(string str)
        {
            TooltipContent = str;
        }

        private static void TooltipContentPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((SettingLabel)d).TooltipContentPropertyChanged((string)e.NewValue);
        }

        private void CreateTooltip()
        {
            if(TooltipContent.Length > 0)
            {
                // Create the tool tip
                ToolTip toolTip = new ToolTip { Content = TooltipContent };
                toolTip.Background = new SolidColorBrush(Colors.Black) { Opacity = 0.8 };
                toolTip.FontSize = 11;
                toolTip.FontFamily = new FontFamily("/Fonts/#Poppins");
                toolTip.Foreground = Brushes.White;
                toolTip.BorderThickness = new Thickness(0);



                // Dont know how to do the offset... dont really care...
                //toolTip.HorizontalOffset = 100;
                //toolTip.VerticalOffset = 100;

                // Create the "?" indicating there is a tooltip
                Border qMarkBorder = new Border();
                qMarkBorder.Background = Brushes.Gray;
                qMarkBorder.Width = 12;
                qMarkBorder.Height = 12;
                qMarkBorder.CornerRadius = new CornerRadius(6, 6, 6, 6);

                TextBlock qMark = new TextBlock();
                qMark.Text = "?";
                qMark.HorizontalAlignment = HorizontalAlignment.Center;
                qMark.VerticalAlignment = VerticalAlignment.Center;
                qMark.FontSize = 10;
                qMark.FontWeight = FontWeight.FromOpenTypeWeight(500);
                qMark.Margin = new Thickness(0, -1, 0, 0);

                qMarkBorder.Child = qMark;
                StackPanel.Children.Add(qMarkBorder);


                StackPanel.ToolTip = toolTip;
            }
        }


        public static readonly DependencyProperty LabelContentProperty =
            DependencyProperty.Register("LabelContent", typeof(string), typeof(SettingLabel),
            new PropertyMetadata("", LabelContentPropertyChanged));

        public string LabelContent
        {
            get { return (string)GetValue(LabelContentProperty); }
            set { SetValue(LabelContentProperty, value);  }
        }

        private void LabelContentPropertyChanged(string str)
        {
            LabelContent = str;
        }

        private static void LabelContentPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((SettingLabel)d).LabelContentPropertyChanged((string)e.NewValue);
        }

    }
}
