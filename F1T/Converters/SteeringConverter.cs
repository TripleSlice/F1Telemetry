using System;
using System.Globalization;
using System.Windows.Data;

using F1T.MVVM.ViewModels;

namespace F1T.Converters
{
    /// <summary>
    /// Provides methods to convert from a <see cref="float"/> value (-1.0 to 1.0) to a <see cref="int"/> value 
    /// </summary>
    public class SteeringConverter : IValueConverter
    {
        private InputTelemetryViewModel inputTelemetryViewModel = InputTelemetryViewModel.GetInstance();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Steering (-1.0 (full lock left) to 1.0 (full lock right))
            return Math.Floor((float)value * inputTelemetryViewModel.Settings.RotationAngle/2);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)value / (float)parameter;
        }
    }
}

