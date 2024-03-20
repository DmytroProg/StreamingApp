using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace StreamingApp.WPF;

[ValueConversion(typeof(bool), typeof(HorizontalAlignment))]
public class BooleanHorizontalAlignmentConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var isRight = (bool)value;
        return isRight ? HorizontalAlignment.Right : HorizontalAlignment.Left;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return null;
    }
}
