using System.Globalization;
using System.Windows.Data;

using LsLocalizeHelperLib.Helper;

namespace LsLocalizeHelperLib.Converter;

public class TextFromResourceConverter : IValueConverter
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
