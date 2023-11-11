using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

using HtmlAgilityPack;

namespace LsLocalizeHelperLib.Helper;

public static class HtmlHelper
{

  #region Static Methods

  public static bool IsValidHtml(this string html)
  {
    var htmlDoc = new HtmlDocument();
    htmlDoc.LoadHtml(html);

    if (htmlDoc.ParseErrors.Any()) { return false; }

    var lstags = htmlDoc.DocumentNode.SelectNodes("//lstag");

    var isValidHtml = lstags?.All(htmlNode => !htmlNode.HasClosingAttributes) ?? true;

    return isValidHtml;
  }

  private static Inline LineMatch(string? line, string? searchReg, Brush? highlightColor = null)
  {
    var result = new Span();

    if (string.IsNullOrEmpty(searchReg)) { return new Run(line); }

    if (string.IsNullOrEmpty(line)) { return new Run(line); }

    try
    {
      // Erstellen Sie eine Regexpression, die den hervorzuhebenden Text erkennt
      // Zum Beispiel: Alle Wörter, die mit "B" beginnen und mit "g" enden
      var regex = new Regex(
        pattern: searchReg,
        options: RegexOptions.IgnoreCase
                 | RegexOptions.Multiline
                 | RegexOptions.CultureInvariant
                 | RegexOptions.Compiled
      );

      // Finden Sie alle Übereinstimmungen im Text
      var matchesText = regex.Matches(line!);

      switch (matchesText.Count)
      {
        case 0: { return new Run(line); }

        // Fügen Sie den Text vor der ersten Übereinstimmung als normalen Run hinzu
        case > 0 when matchesText[0].Index > 0:
        {
          var substring = line!.Substring(startIndex: 0, length: matchesText[0].Index);
          var s = new Run(substring);
          result.Inlines.Add(s);

          break;
        }
      }

      // Fügen Sie jede Übereinstimmung als hervorgehobenen Run hinzu
      // und fügen Sie den Text zwischen den Übereinstimmungen als normalen Run hinzu
      for (var i = 0;
           i < matchesText.Count;
           i++)
      {
        var matchBlock = new Bold(new Run(matchesText[i].Value));
        var span = new Span(matchBlock);
        span.Foreground = highlightColor ?? Brushes.Red;

        result.Inlines.Add(span);

        if (i >= matchesText.Count - 1) { continue; }

        var substring = line!.Substring(
          startIndex: matchesText[i].Index + matchesText[i].Length,
          length: matchesText[i + 1].Index - (matchesText[i].Index + matchesText[i].Length)
        );

        result.Inlines.Add(new Run(substring));
      }

      // Fügen Sie den restlichen Text nach der letzten Übereinstimmung als normalen Run hinzu
      var lastIndex = matchesText.Count - 1;

      if (lastIndex >= 0
          && matchesText[lastIndex].Index + matchesText[lastIndex].Length < line!.Length)
      {
        var substring = line.Substring(matchesText[lastIndex].Index + matchesText[lastIndex].Length);
        result.Inlines.Add(new Run(substring));
      }
    }
    catch (Exception ex) { result.Inlines.Add(new Run(ex.Message)); }

    return result;
  }

  #endregion

}
