using System;
using System.Globalization;
using System.Linq;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;

using LSLocalizeHelper.Models;
using LSLocalizeHelper.Services;

using ReactiveUI;

namespace LSLocalizeHelper.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{

  #region Constructors

  public MainWindow()
  {
    this.InitializeComponent();
    this.SetLocals();
    var canOpenWindow = this.CheckIfWindowCanOpen();

    if (!canOpenWindow)
    {
      this.Close();
    }

    this.LoadMods();
    this.LoadModsListBox();
    this.LoadXmlFiles();
    this.LoadSettings();

    this.TranslationGrid.Events()
        .SelectionChanged.Throttle(TimeSpan.FromSeconds(0.8))
        .ObserveOn(RxApp.MainThreadScheduler)
        .Select(e => e.AddedItems[0] as DataRowModel)
        .Subscribe(this.DoOnRowChanged);
  }

  #endregion

  #region Methods

  private bool CheckIfWindowCanOpen() =>
    !string.IsNullOrEmpty(SettingsManager.Settings?.ModsPath) || this.ShowSettingsDialog();

  private void CmdShowSettings_Click(object sender, RoutedEventArgs e)
  {
    this.ShowSettingsDialog();
  }

  private void ListBoxMods_SelectionChanged(object sender, SelectionChangedEventArgs e)
  {
    var selectedValue = this.ListBoxMods.SelectedItems.Cast<ModModel>()
                            .Select(model => model.Name)
                            .ToArray();

    SettingsManager.Settings!.LastMods = selectedValue;
    SettingsManager.Save();
    this.LoadXmlFiles();
  }

  private void ListBoxOriginCurrentFile_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
  {
    var selectedValue = this.ListBoxOriginCurrentFile.SelectedItems.Cast<XmlFileModel>()
                            .Select(
                               model => new SelectionModel()
                               {
                                 Name = model.Name,
                                 ModName = model.Mod.Name,
                               }
                             )
                            .ToArray();

    SettingsManager.Settings!.LastOriginsCurrent = selectedValue;
    SettingsManager.Save();
  }

  private void ListBoxOriginPreviousFile_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
  {
    var selectedValue = this.ListBoxOriginPreviousFile.SelectedItems.Cast<XmlFileModel>()
                            .Select(
                               model => new SelectionModel()
                               {
                                 Name = model.Name,
                                 ModName = model.Mod.Name,
                               }
                             )
                            .ToArray();

    SettingsManager.Settings!.LastOriginsPrevious = selectedValue;
    SettingsManager.Save();
  }

  private void ListBoxTranslatedFile_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
  {
    var selectedValue = this.ListBoxTranslatedFile.SelectedItems.Cast<XmlFileModel>()
                            .Select(
                               model => new SelectionModel()
                               {
                                 Name = model.Name,
                                 ModName = model.Mod.Name,
                               }
                             )
                            .ToArray();

    SettingsManager.Settings!.LastOriginsTranslated = selectedValue;
    SettingsManager.Save();
  }

  private void LoadCurrentFileListBox()
  {
    // Reload selected Current Origin files
    this.ListBoxOriginCurrentFile.SelectionChanged -= this.ListBoxOriginCurrentFile_OnSelectionChanged;

    foreach (var lastMod in SettingsManager.Settings.LastOriginsCurrent)
    {
      var item = this.OriginCurrentFileItems.First(
        delegate(XmlFileModel xmlFileModel)
        {
          return xmlFileModel.Name == lastMod.Name && xmlFileModel.Mod.Name == lastMod.ModName;
        }
      );

      this.ListBoxOriginCurrentFile.SelectedItems.Add(item);
    }

    this.ListBoxOriginCurrentFile.SelectionChanged += this.ListBoxOriginCurrentFile_OnSelectionChanged;
  }

  private void LoadModsListBox()
  {
    this.ListBoxMods.SelectionChanged -= this.ListBoxMods_SelectionChanged;

    // Reload selected Mods
    foreach (var lastMod in SettingsManager.Settings.LastMods)
    {
      var item = this.ProjectItems.First(m => m.Name == lastMod);
      this.ListBoxMods.SelectedItems.Add(item);
    }

    this.ListBoxMods.SelectionChanged += this.ListBoxMods_SelectionChanged;
  }

  private void LoadPreviousFileListBox()
  {
    // Reload selected Previous Origin files
    this.ListBoxOriginPreviousFile.SelectionChanged -= this.ListBoxOriginPreviousFile_OnSelectionChanged;

    foreach (var lastMod in SettingsManager.Settings.LastOriginsPrevious)
    {
      var item = this.OriginPreviousFileItems.First(
        xmlFileModel => xmlFileModel.Name == lastMod.Name && xmlFileModel.Mod.Name == lastMod.ModName
      );

      this.ListBoxOriginPreviousFile.SelectedItems.Add(item);
    }

    this.ListBoxOriginPreviousFile.SelectionChanged += this.ListBoxOriginPreviousFile_OnSelectionChanged;
  }

  private void LoadSettings()
  {
    if (SettingsManager.Settings == null)
    {
      return;
    }

    this.LoadWindowSettings();
    this.LoadCurrentFileListBox();
    this.LoadPreviousFileListBox();
    this.LoadTranslatedFileListBox();
  }

  private void LoadTranslatedFileListBox()
  {
    // Reload selected Previous Origin files
    this.ListBoxTranslatedFile.SelectionChanged -= this.ListBoxTranslatedFile_OnSelectionChanged;

    foreach (var lastMod in SettingsManager.Settings.LastOriginsTranslated)
    {
      var item = this.TranslatedFileItems.First(
        xmlFileModel => xmlFileModel.Name == lastMod.Name && xmlFileModel.Mod.Name == lastMod.ModName
      );

      this.ListBoxTranslatedFile.SelectedItems.Add(item);
    }

    this.ListBoxTranslatedFile.SelectionChanged += this.ListBoxTranslatedFile_OnSelectionChanged;
  }

  private void LoadWindowSettings()
  {
    this.Width = SettingsManager.Settings.WindowWidth;
    this.Height = SettingsManager.Settings.WindowHeight;
    this.Top = SettingsManager.Settings.WindowTop;
    this.Left = SettingsManager.Settings.WindowLeft;
  }

  private void SetLocals()
  {
    // Setzen Sie die Sprache auf Deutsch (Deutschland)
    this.Language = XmlLanguage.GetLanguage("de-DE");

    var oldDict = Application.Current.Resources.MergedDictionaries.FirstOrDefault(
      d =>
      {
        return d.Source.OriginalString == "/Resources.de-DE.xaml";
      }
    );

    if (oldDict != null)
    {
      Application.Current.Resources.MergedDictionaries.Remove(oldDict);
    }

    var newDict = new ResourceDictionary
    {
      Source = new Uri(uriString: "/Resources.de-DE.xaml", uriKind: UriKind.Relative),
    };

    Application.Current.Resources.MergedDictionaries.Add(newDict);
  }

  private void WindowMain_LocationChanged(object sender, EventArgs e)
  {
    var userSettingsSettings = SettingsManager.Settings;

    if (userSettingsSettings != null)
    {
      userSettingsSettings.WindowTop = this.Top;
      userSettingsSettings.WindowLeft = this.Left;
    }

    SettingsManager.Save();
  }

  private void WindowMain_SizeChanged(object sender, SizeChangedEventArgs e)
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

  public string CalculateRowStatus(DataRowModel row)
  {
    return "calculated";
  }
}
