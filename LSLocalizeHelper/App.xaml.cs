using System.Windows;

using LSLocalizeHelper.Services;

using ShowMeTheXAML;

namespace LSLocalizeHelper;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{

  public App()
  {
    SettingsManager.Load();
    
  }

  protected override void OnStartup(StartupEventArgs e)
  {
    XamlDisplay.Init();
    base.OnStartup(e);
  }

}
