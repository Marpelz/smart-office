using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SmartOffice.Views.Converters;

public class AddressConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values.Length != 3 || values.Any(v => v == null))
            return DependencyProperty.UnsetValue;

        string street = values[0].ToString();
        string zipcode = values[1].ToString();
        string city = values[2].ToString();

        return $"{street}, {zipcode} {city}";
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}