using System;
using System.Linq;
using System.Windows;
using System.Windows.Markup;

using LSLocalizeHelper.Services;

namespace LSLocalizeHelper.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    #region Constructors

    public MainWindow()
    {
        this.InitializeComponent();

        this.LoadSettings();
        this.SetLocals();

        var canOpenWindow = this.CheckIfWindowCanOpen();

        if (!canOpenWindow)
        {
            this.Close();
        }

        this.LoadMods();
    }

    private bool CheckIfWindowCanOpen()
    {
        return !string.IsNullOrEmpty(SettingsManager.Settings?.ModsPath) || this.ShowSettingsDialog();
    }

    #endregion

    #region Methods

    private void LoadSettings()
    {
        SettingsManager.Load();

        if (SettingsManager.Settings == null) return;

        this.Width = SettingsManager.Settings.WindowWidth;
        this.Height = SettingsManager.Settings.WindowHeight;
        this.Top = SettingsManager.Settings.WindowTop;
        this.Left = SettingsManager.Settings.WindowLeft;
    }

    private void SetLocals()
    {
        // Setzen Sie die Sprache auf Deutsch (Deutschland)
        this.Language = XmlLanguage.GetLanguage("de-DE");

        var oldDict =
          Application.Current.Resources.MergedDictionaries.FirstOrDefault(
                                                                          d =>
                                                                          {
                                                                              return d.Source.OriginalString
                                                                                 == "/Resources.de-DE.xaml";
                                                                          }
                                                                         );

        if (oldDict != null) { Application.Current.Resources.MergedDictionaries.Remove(oldDict); }

        var newDict = new ResourceDictionary { Source = new Uri("/Resources.de-DE.xaml", UriKind.Relative) };
        Application.Current.Resources.MergedDictionaries.Add(newDict);
    }

    private void WindowMain_LocationChanged(
      object sender,
      EventArgs e
    )
    {
        var userSettingsSettings = SettingsManager.Settings;

        if (userSettingsSettings != null)
        {
            userSettingsSettings.WindowTop = this.Top;
            userSettingsSettings.WindowLeft = this.Left;
        }

        SettingsManager.Save();
    }

    private void WindowMain_SizeChanged(
      object sender,
      SizeChangedEventArgs e
    )
    {
        var userSettingsSettings = SettingsManager.Settings;

        if (userSettingsSettings != null)
        {
            userSettingsSettings.WindowWidth = this.Width;
            userSettingsSettings.WindowHeight = this.Height;
        }

        SettingsManager.Save();
    }

    #endregion

    private void CmdShowSettings_Click(
      object sender,
      RoutedEventArgs e
    )
    {
        this.ShowSettingsDialog();
    }

    private void ListBoxMods_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {
        this.LoadXmlFiles();

    }
}