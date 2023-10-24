using System;
using System.Globalization;
using System.Windows.Data;

using LSLocalizeHelper.Enums;
using LSLocalizeHelper.Models;
using LSLocalizeHelper.Services;

namespace LSLocalizeHelper.Converter;

public class OriginStatusConverter : IValueConverter
{
  public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {
    // Implement your function here
    var translatedData = value as DataRowModel;

    var currentOrigin  = LsWorkingDataService.GetCurrentForUid(translatedData!.Uuid);
    var previousOrigin  = LsWorkingDataService.GetPreviousForUid(translatedData!.Uuid);

    // var result  = translatedData.Status;

    var result = CalculateStatus(translatedData, currentOrigin, previousOrigin);

    return result;
  }

  private TranslationStatus CalculateStatus(DataRowModel? translatedNode, OriginModel? currentNode, OriginModel? previousNode)
  {
    var isTranslatedNodeNull = translatedNode == null;
    var isCurrentNodeNull    = currentNode == null;

    if (isTranslatedNodeNull && !isCurrentNodeNull)
    {
      return TranslationStatus.NewStatus;
    }

    if (!isTranslatedNodeNull && isCurrentNodeNull)
    {
      return TranslationStatus.DeletedStatus;
    }

    if (translatedNode?.Text == currentNode?.Text)
    {
      var wasTextUpdated = currentNode?.Text != previousNode?.Text && previousNode != null;
      return wasTextUpdated ? TranslationStatus.UpdatedStatus : TranslationStatus.OriginStatus;
    }

    return TranslationStatus.TranslatedStatus;
  }
  public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
  {
    throw new NotImplementedException();
  }
}
