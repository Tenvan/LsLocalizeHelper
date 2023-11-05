using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

using LsLocalizeHelperLib.Helper;

namespace LsLocalizeHelperLib.Converter;

public class DocumentQuickSearchMultiConverter : IMultiValueConverter
{

  public object Convert(object[] value,
                        Type targetType,
                        object parameter,
                        CultureInfo culture
  )
  {
    var input = value[0] as string;
    var search = value[1] as string;

    var doc = DocumentHelper.HighlightTextToFlowDocument(text: input, searchReg: search);
    return doc;
  }

  public object[] ConvertBack(object value,
                              Type[] targetTypes,
                              object parameter,
                              CultureInfo culture
  ) =>
    throw new NotImplementedException();

}
