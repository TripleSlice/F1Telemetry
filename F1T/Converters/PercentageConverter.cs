using System;
using System.Globalization;
using System.Windows.Data;

namespace F1T.Converters
{
    /// <summary>
    /// Provides methods to convert from a <see cref="float"/> value (percentage) to a <see cref="int"/> value 
    /// </summary>
    public class PercentageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            float val = (float)value;
            return Math.Floor(val * 100f);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int val = (int)value;
            return val / 100f;
        }
    }
}
