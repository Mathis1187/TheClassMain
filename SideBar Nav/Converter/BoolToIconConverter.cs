using System;
using System.Globalization;
using System.Windows.Data;

namespace TheClassMain.Converter;

public class BoolToIconConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool b)
        {
            return b ? "✅" : "❌";
        }
        return "";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        // Ce n'est pas nécessaire dans ce cas
        throw new NotImplementedException();
    }
}