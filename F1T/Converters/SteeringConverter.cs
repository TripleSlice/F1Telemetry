using System;
using System.Globalization;
using System.Windows.Data;

namespace F1T.Converters
{
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

