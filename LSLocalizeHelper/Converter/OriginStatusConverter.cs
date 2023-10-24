using System;
using System.Globalization;
using System.Windows.Data;

namespace LSLocalizeHelper.Converter;

public class OriginStatusConverter : IValueConverter
{
  public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {
    // Implement your function here
    var form = parameter as RelativeSource;
    return "Blub";
  }

  public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
  {
    throw new NotImplementedException();
  }
}
