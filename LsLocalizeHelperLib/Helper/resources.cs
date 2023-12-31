using System.Windows;

namespace LsLocalizeHelperLib.Helper;

public static class Resources
{

  public static string FromResource(this string key)
  {
      var keyText = "#R:" + key + "#";
    try
    {
      var foundResource = Application.Current.FindResource(key);
      return foundResource?.ToString() ?? keyText;
    }
    catch (Exception ex)
    {
      return keyText;
    }
  }

}
