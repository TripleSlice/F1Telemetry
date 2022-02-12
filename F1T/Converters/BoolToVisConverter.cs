using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace F1T.Converters
{

    /// <summary>
    /// Provides methods to convert from a <see cref="Boolean"/> value to a <see cref="Visibility"/> value
    /// </summary>
    public class BoolToVisConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool val = (bool)value;
            if (val) return Visibility.Visible;    
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility vis = (Visibility)value;
            if (vis == Visibility.Visible) return true;
            return false;
        }
    }
}
