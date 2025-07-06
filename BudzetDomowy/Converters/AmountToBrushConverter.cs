using Avalonia.Data.Converters;
using Avalonia.Media;
using System;
using System.Globalization;

namespace BudzetDomowy.Converters
{
    public class AmountToBrushConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is decimal amount)
            {
                if (amount > 0)
                {
                    return Brushes.LightGreen;
                }
                if (amount < 0)
                { 
                    return Brushes.PaleVioletRed;
                }
            }
            // Domyślny kolor
            return Brushes.White;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}