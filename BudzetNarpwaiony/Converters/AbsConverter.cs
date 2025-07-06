using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace BudzetNarpwaiony.Converters
{
    public class AbsConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is decimal d)
            {
                return Math.Abs(d);
            }
            return 0;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}