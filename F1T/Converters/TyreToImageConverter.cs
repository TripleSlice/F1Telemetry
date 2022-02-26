using System;
using System.Globalization;
using System.Windows.Data;
using F1T.Structs;

namespace F1T.Converters
{
    public class TyreToImageConverter : IValueConverter
    {

        private static string basePath = "/Images/VisualTyreTypes/";
        
        private static string unknownPath = basePath + "unknown.png";
        private static string softPath = basePath + "soft.png";
        private static string mediumPath = basePath + "medium.png";
        private static string hardPath = basePath + "hard.png";
        private static string wetPath = basePath + "wet.png";
        private static string interPath = basePath + "inter.png";
        private static string superSoftPath = basePath + "supersoft.png";

        private static string emptyPath = basePath + "empty.png";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            VisualTyreCompound val = (VisualTyreCompound)value;

            switch (val)
            {
                case VisualTyreCompound.Soft:
                    return softPath;
                case VisualTyreCompound.Medium:
                    return mediumPath;
                case VisualTyreCompound.Hard:
                    return hardPath;
                case VisualTyreCompound.Inter:
                    return interPath;
                case VisualTyreCompound.Wet:
                    return wetPath;
                case VisualTyreCompound.ClassicDry:
                    return softPath;
                case VisualTyreCompound.ClassicWet:
                    return wetPath;
                case VisualTyreCompound.F2Wet:
                    return wetPath;
                case VisualTyreCompound.F2SuperSoft:
                    return superSoftPath;
                case VisualTyreCompound.F2Soft:
                    return softPath;
                case VisualTyreCompound.F2Medium:
                    return mediumPath;
                case VisualTyreCompound.F2Hard:
                    return hardPath;
                case VisualTyreCompound.None:
                    return emptyPath;
                default:
                    return unknownPath;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
