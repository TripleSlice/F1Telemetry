using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace F1T.Converters
{
    /// <summary>
    /// Provides methods to convert from a <see cref="bool"/> to a <see cref="SolidColorBrush"/> (green or red)
    /// </summary>
    public class ToggledColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool val = (bool)value;
            if (val) return new SolidColorBrush(Colors.Green);
            return new SolidColorBrush(Colors.Red);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
