using System;
using System.Globalization;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;

using LSLocalizeHelper.Enums;
using LSLocalizeHelper.Models;
using LSLocalizeHelper.Services;

namespace LSLocalizeHelper.Converter;

public class TextQuickSearchMultiConverter : IMultiValueConverter
{

  public object Convert(object[] value,
                        Type targetType,
                        object parameter,
                        CultureInfo culture
  )
  {
    var input = value[0] as string;

    if (input == null)
    {
      return null;
    }

    var escapedXml = SecurityElement.Escape(input);

    var search = value[1] as string;

    if (string.IsNullOrEmpty(search))
    {
      return escapedXml;
    }

    var newText = new StringBuilder();

    try
    {
      // Erstellen Sie eine Regexpression, die den hervorzuhebenden Text erkennt
      // Zum Beispiel: Alle Wörter, die mit "B" beginnen und mit "g" enden
      var regex = new Regex(
        search,
        RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant | RegexOptions.Compiled
      );

      // Finden Sie alle Übereinstimmungen im Text
      var matches = regex.Matches(escapedXml);

      if (matches.Count == 0)
      {
        return escapedXml;
      }

      // Fügen Sie den Text vor der ersten Übereinstimmung als normalen Run hinzu
      if (matches.Count > 0
          && matches[0].Index > 0)
      {
        newText.Append(escapedXml.Substring(0, matches[0].Index));
      }

      // Fügen Sie jede Übereinstimmung als hervorgehobenen Run hinzu
      // und fügen Sie den Text zwischen den Übereinstimmungen als normalen Run hinzu
      for (var i = 0;
           i < matches.Count;
           i++)
      {
        newText.Append($"<Bold>{matches[i].Value}</Bold>");

        if (i < matches.Count - 1)
        {
          newText.Append(
            escapedXml.Substring(
              matches[i].Index + matches[i].Length,
              matches[i + 1].Index - (matches[i].Index + matches[i].Length)
            )
          );
        }
      }

      // Fügen Sie den restlichen Text nach der letzten Übereinstimmung als normalen Run hinzu
      if (matches.Count > 0
          && matches[^1].Index + matches[^1].Length < escapedXml.Length)
      {
        newText.Append(escapedXml.Substring(matches[^1].Index + matches[^1].Length));
      }
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex.Message);

      return escapedXml;
    }

    return newText.ToString();
  }

  public object[] ConvertBack(object value,
                              Type[] targetTypes,
                              object parameter,
                              CultureInfo culture
  ) =>
    throw new NotImplementedException();

}
