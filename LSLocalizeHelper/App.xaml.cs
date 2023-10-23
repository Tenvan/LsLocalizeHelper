using System.Windows;

using LSLocalizeHelper.Services;

namespace LSLocalizeHelper
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            // Thread.CurrentThread.CurrentUICulture = new CultureInfo("de-DE");
            SettingsManager.Load();
        }
    }
}
