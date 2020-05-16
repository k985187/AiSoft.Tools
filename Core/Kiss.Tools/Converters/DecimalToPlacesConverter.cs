using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Kiss.Tools.Converters
{
    public class DecimalToPlacesConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length < 2)
            {
                return "";
            }
            decimal.TryParse(values[0].ToString(), out var price);
            var partition = (int)values[1];
            return price.ToString($"f{partition}");
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return new object[] {DependencyProperty.UnsetValue, DependencyProperty.UnsetValue};
        }
    }
}