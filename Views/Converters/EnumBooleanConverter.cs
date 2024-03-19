using System.Globalization;
using System.Windows.Data;

namespace SmartOffice.Views.Converters;

internal class EnumBooleanConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value != null)
            return value.ToString().Equals(parameter);
        return false;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if ((bool)value)
            return parameter;
        return Binding.DoNothing;
    }
}