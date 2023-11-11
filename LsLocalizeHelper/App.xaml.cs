using System;
using System.Windows;

using LsLocalizeHelperLib.Services;

namespace LsLocalizeHelper;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{

  public App() { SettingsManager.Load(); }

  public static void SetClipboardText(string? text)
  {
    if (text == null) { return; }
    Clipboard.SetText(text);
  }

  public static string GetClipboardText()
  {
    var text = Clipboard.GetText();
    return text;
  }

}
