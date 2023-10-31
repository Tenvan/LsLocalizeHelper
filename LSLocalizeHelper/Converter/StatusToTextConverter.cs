using System;
using System.Globalization;
using System.Windows.Data;

using LSLocalizeHelper.Enums;
using LSLocalizeHelper.Helper;

namespace LSLocalizeHelper.Converter;

public class StatusToTextConverter : IValueConverter
{

  public object Convert(object value,
                        Type targetType,
                        object parameter,
                        CultureInfo culture
  )
  {
    // Implement your function here
    var newText = $"{value}";
    return newText.FromResource();
  }

  public object ConvertBack(object value,
                            Type targetType,
                            object parameter,
                            CultureInfo culture
  )
  {
    throw new NotImplementedException();
  }

}
