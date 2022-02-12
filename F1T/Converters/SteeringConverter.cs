using System;
using System.Globalization;
using System.Windows.Data;

namespace F1T.Converters
{
    /// <summary>
    /// Provides methods to convert from a <see cref="float"/> value (-1.0 to 1.0) to a <see cref="int"/> value 
    /// </summary>
    public class SteeringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Steering (-1.0 (full lock left) to 1.0 (full lock right))
            float val = (float)value;
            return Math.Floor(val * 90f);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int val = (int)value;
            return val / 90f;
        }
    }
}

