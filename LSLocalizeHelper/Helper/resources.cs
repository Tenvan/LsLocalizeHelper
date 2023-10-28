using System.Windows;

namespace LSLocalizeHelper.Helper;

public static class resources
{
  public static string FromResource(this string key) =>
    Application.Current.FindResource(key)
              ?.ToString()
    ?? string.Empty;

}
