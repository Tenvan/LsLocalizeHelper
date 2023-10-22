using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

using LSLocalizeHelper.Models;
using LSLocalizeHelper.Services;
using LSLocalizeHelper.Views.Settings;

namespace LSLocalizeHelper;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
  #region Fields

  private readonly SettingsManager<UserSettings> UserSettings;

  #endregion

  #region Constructors

  public MainWindow()
  {
    this.InitializeComponent();
    this.UserSettings = new SettingsManager<UserSettings>();
    this.LoadSettings();
    this.SetLocals();
    this.InitCommand();
  }

  #endregion

  #region Methods

  private void LoadSettings()
  {
    this.UserSettings.Load();

    if (this.UserSettings.Settings == null) return;

    this.Width  = this.UserSettings.Settings.WindowWidth;
    this.Height = this.UserSettings.Settings.WindowHeight;
    this.Top    = this.UserSettings.Settings.WindowTop;
    this.Left   = this.UserSettings.Settings.WindowLeft;
  }

  private void SetLocals()
  {
    // Setzen Sie die Sprache auf Deutsch (Deutschland)
    this.Language = XmlLanguage.GetLanguage("de-DE");
    var oldDict = Application.Current.Resources.MergedDictionaries.FirstOrDefault(d => { return d.Source.OriginalString == "/Resources.de-DE.xaml"; });

    if (oldDict != null) { Application.Current.Resources.MergedDictionaries.Remove(oldDict); }

    var newDict = new ResourceDictionary { Source = new Uri("/Resources.de-DE.xaml", UriKind.Relative) };
    Application.Current.Resources.MergedDictionaries.Add(newDict);
  }

  private void WindowMain_LocationChanged(
    object    sender,
    EventArgs e
  )
  {
    var userSettingsSettings = this.UserSettings.Settings;

    if (userSettingsSettings != null)
    {
      userSettingsSettings.WindowTop  = this.Top;
      userSettingsSettings.WindowLeft = this.Left;
    }

    this.UserSettings.Save();
  }

  private void WindowMain_SizeChanged(
    object               sender,
    SizeChangedEventArgs e
  )
  {
    var userSettingsSettings = this.UserSettings.Settings;

    if (userSettingsSettings != null)
    {
      userSettingsSettings.WindowWidth  = this.Width;
      userSettingsSettings.WindowHeight = this.Height;
    }

    this.UserSettings.Save();
  }

  #endregion

  private void ShowSettingsDialog()
  {
    var settingsDialog = new SettingsDialog();
    settingsDialog.ShowDialog();
  }

    private void CmdShowSettings_Click(object sender, RoutedEventArgs e)
    {
        this.ShowSettingsDialog();
    }
}