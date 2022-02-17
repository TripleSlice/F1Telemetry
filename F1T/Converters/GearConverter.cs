using System;
using System.Globalization;
using System.Windows.Data;

namespace F1T.Converters
{
    /// <summary>
    /// Provides methods to convert from a <see cref="int"/> value to a Gear (R,N,1,2,3,4,5,6,7,8)
    /// </summary>
    public class GearConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string val = (string)value;
            if (val == "-1") return "R";
            if (val == "0") return "N";
            return val;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
