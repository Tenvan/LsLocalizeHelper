using System;
using System.Windows;

using LSLocalizeHelper.Services;

using ShowMeTheXAML;

namespace LSLocalizeHelper;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{

  public App() { SettingsManager.Load(); }

  protected override void OnStartup(StartupEventArgs e)
  {
    XamlDisplay.Init();
    base.OnStartup(e);
  }

  public static void SetClipboardText(string? text)
  {
    if (text == null) { return; }

    Clipboard.SetText(text);
    Console.WriteLine("copy to clipboard:\r\n" + text);
  }

  public static string GetClipboardText()
  {
    var text = Clipboard.GetText();
    Console.WriteLine("get from clipboard:\r\n" + text);

    return text;
  }

}
