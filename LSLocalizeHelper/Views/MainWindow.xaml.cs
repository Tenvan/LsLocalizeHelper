using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;

using LSLocalizeHelper.Models;
using LSLocalizeHelper.Services;

namespace LSLocalizeHelper.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window, INotifyPropertyChanged
{

  #region Constructors

  public MainWindow()
  {
    this.InitializeComponent();

    this.SetLocals(CultureInfo.CurrentCulture);

    this.LoadWindowSettings();

    this.LoadMods();
    this.SetListBoxModsSelections();

    this.LoadXmlFiles();
    this.SetListBoxCurrentFileSelections();
    this.SetListBoxPreviousFileSelections();
    this.SetListBoxTranslatedFileSelections();

    this.SetEvents();
  }

  #endregion

  #region Properties

  private DataRowModel? CurrentDataRow
  {
    get
    {
      var currentRow = this.TranslationGrid.SelectedItem as DataRowModel;

      return currentRow;
    }
  }

  #endregion

  #region Methods

  protected void BeginUpdating() { this.IsUpdating = true; }

  protected void EndUpdating() { this.IsUpdating = false; }

  protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
  {
    this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
  }

  protected virtual bool SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
  {
    if (EqualityComparer<T>.Default.Equals(field, value)) return false;

    field = value;
    this.OnPropertyChanged(propertyName);

    return true;
  }

  private void ButtonApplyTranslated_OnClick(object sender, RoutedEventArgs e)
  {
    var newText = this.TextBoxTranslated.Text;
    LsWorkingDataService.SetTranslatedForUid(this.CurrentDataRow?.Uuid, newText);
  }

  private void LoadWindowSettings()
  {
    this.Width = SettingsManager.Settings.WindowWidth;
    this.Height = SettingsManager.Settings.WindowHeight;
    this.Top = SettingsManager.Settings.WindowTop;
    this.Left = SettingsManager.Settings.WindowLeft;
    var settingsProjectHeight = (double)SettingsManager.Settings.ProjectHeight;
    this.RowDefinitionProjects.Height = new GridLength(Math.Max(settingsProjectHeight, 100));
    var settingsTranslationHeight = (double)SettingsManager.Settings.TranslationHeight;
    this.RowDefinitionTranslation.Height = new GridLength(Math.Max(settingsTranslationHeight, 100));
  }

  private void SetListBoxCurrentFileSelections()
  {
    // Reload selected Current Origin files
    foreach (var lastMod in SettingsManager.Settings.LastOriginsCurrent)
    {
      var item = this.OriginCurrentFileItems.FirstOrDefault(
        xmlFileModel => xmlFileModel.Name == lastMod.Name && xmlFileModel.Mod.Name == lastMod.ModName
      );

      this.ListBoxOriginCurrentFile.SelectedItems.Add(item);
    }
  }

  private void SetListBoxModsSelections()
  {
    // Reload selected Mods
    foreach (var lastMod in SettingsManager.Settings.LastMods)
    {
      var item = this.ProjectItems.FirstOrDefault(m => m.Name == lastMod);

      if (item != null) { this.ListBoxMods.SelectedItems.Add(item); }
    }
  }

  private void SetListBoxPreviousFileSelections()
  {
    // Reload selected Previous Origin files
    foreach (var lastMod in SettingsManager.Settings.LastOriginsPrevious)
    {
      var item = this.OriginPreviousFileItems.FirstOrDefault(
        xmlFileModel => xmlFileModel.Name == lastMod.Name && xmlFileModel.Mod.Name == lastMod.ModName
      );

      this.ListBoxOriginPreviousFile.SelectedItems.Add(item);
    }
  }

  private void SetListBoxTranslatedFileSelections()
  {
    // Reload selected Previous Origin files
    foreach (var lastMod in SettingsManager.Settings.LastOriginsTranslated)
    {
      var item = this.TranslatedFileItems.FirstOrDefault(
        xmlFileModel => xmlFileModel.Name == lastMod.Name && xmlFileModel.Mod.Name == lastMod.ModName
      );

      this.ListBoxTranslatedFile.SelectedItems.Add(item);
    }
  }

  private void SetLocals(CultureInfo culture)
  {
    if (this.LanguageItems.Count == 0)
    {
      this.LanguageItems.Add(new CultureInfo("de-DE"));
      this.LanguageItems.Add(new CultureInfo("en-US"));
      this.LanguageItems.Add(new CultureInfo("zh-Hans"));
      this.LanguageItems.Add(new CultureInfo("zh-Hant"));
    }

    // this.Language = culture;
    var lower = culture.IetfLanguageTag;

    var oldDict = Application.Current.Resources.MergedDictionaries.FirstOrDefault(
      d => d.Source != null
           && d.Source.OriginalString.ToLower()
               .Contains($"locals.{lower.ToLower()}")
    );

    if (oldDict != null) { Application.Current.Resources.MergedDictionaries.Remove(oldDict); }

    var newDict = new ResourceDictionary
    {
      Source = new Uri(uriString: $"locals/locals.{culture}.xaml", uriKind: UriKind.Relative),
    };

    Application.Current.Resources.MergedDictionaries.Add(newDict);

    Thread.CurrentThread.CurrentCulture = culture;
    Thread.CurrentThread.CurrentUICulture = culture;
  }

  #endregion

  #region All Other Members

  public event PropertyChangedEventHandler? PropertyChanged;

  #endregion

}
