using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint_AvaloniaUI.Converters
{
    internal class DoubleToNormalDoubleConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {

            var doubleVal = (double)value!;

            var roundedVal = Math.Round(doubleVal, 1);

            var tenthPart = roundedVal % 1;

            if (tenthPart > 0.2
                && tenthPart < 0.6)
            {
                var missingPart = 0.5 - tenthPart;

                return roundedVal + missingPart;
            }
            else
            {
                return Math.Round(roundedVal);
            }
        }
    }
}
