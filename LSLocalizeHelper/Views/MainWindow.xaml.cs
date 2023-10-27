using System;
using System.Linq;
using System.Windows;
using System.Windows.Markup;

using LSLocalizeHelper.Models;
using LSLocalizeHelper.Services;

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

    // var skin = new ResourceDictionary(); 
    // skin.Source = new Uri(@"\Themes\Dark.xaml", UriKind.Absolute); 
    // App.Current.Resources.MergedDictionaries.Add(skin); 

    this.SetLocals(XmlLanguage.GetLanguage("de"));
    var canOpenWindow = this.CheckIfWindowCanOpen();

    if (!canOpenWindow) { this.Close(); }

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

  #region Methods

  public string CalculateRowStatus(DataRowModel row) { return "calculated"; }

  protected void BeginUpdating() { this.IsUpdating = true; }

  protected void EndUpdating() { this.IsUpdating = false; }

  private bool CheckIfWindowCanOpen() =>
    !string.IsNullOrEmpty(SettingsManager.Settings?.ModsPath) || this.ShowSettingsDialog();

  private void SetListBoxModsSelections()
  {
    // Reload selected Mods
    foreach (var lastMod in SettingsManager.Settings.LastMods)
    {
      var item = this.ProjectItems.First(m => m.Name == lastMod);
      this.ListBoxMods.SelectedItems.Add(item);
    }
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

  private void SetLocals(XmlLanguage? locals)
  {
    if (this.LanguageItems.Count == 0)
    {
      this.LanguageItems.Add(XmlLanguage.GetLanguage("de"));
      this.LanguageItems.Add(XmlLanguage.GetLanguage("en"));
      this.LanguageItems.Add(XmlLanguage.GetLanguage("zh"));
    }

    this.Language = locals;
    var lower = locals.IetfLanguageTag;

    var oldDict = Application.Current.Resources.MergedDictionaries.FirstOrDefault(
      d => d.Source != null
           && d.Source.OriginalString.ToLower()
               .Contains($"locals.{lower}")
    );

    if (oldDict != null) { Application.Current.Resources.MergedDictionaries.Remove(oldDict); }

    var newDict = new ResourceDictionary
    {
      Source = new Uri(uriString: $"locals/locals.{locals}.xaml", uriKind: UriKind.Relative),
    };

    Application.Current.Resources.MergedDictionaries.Add(newDict);
  }

  #endregion

}
