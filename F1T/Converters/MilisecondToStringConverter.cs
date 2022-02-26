using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace F1T.Converters
{
    public class MilisecondToStringConverter : IValueConverter
    {

        string sfmt = "00.###";
        string msfmt = "000";
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            uint val = (uint)value;
            if (val == 0) return "--.--.---";

            var ms = val % 1000;
            val = (val - ms) / 1000;
            var secs = val % 60;
            val = (val - secs) / 60;
            var mins = val % 60;

            return mins + ":" + secs.ToString(sfmt) + "." + ms.ToString(msfmt);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return 0;
        }
    }
}
