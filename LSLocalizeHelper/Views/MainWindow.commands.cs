using System;
using System.Collections.ObjectModel;
using System.Linq;

using CommunityToolkit.Mvvm.Input;

using LSLocalizeHelper.Models;
using LSLocalizeHelper.Services;

namespace LSLocalizeHelper.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{

  #region Properties

  public ObservableCollection<DataRowModel?> DataGridItems { get; } = LsWorkingDataService.TranslatedItems;

  #endregion

  #region Methods

  [RelayCommand]
  private void ApplicationExit() { this.Close(); }

  [RelayCommand]
  private void ImportNewMod() { this.DoImportMod(); }

  private void LoadMods()
  {
    this.lsModsService.LoadMods();
    this.ProjectItems.Clear();

    foreach (var modModel in this.lsModsService.Items) { this.ProjectItems.Add(new ModModelListBoxItem(modModel)); }
  }

  [RelayCommand]
  private void LoadXmlData()
  {
    LsWorkingDataService.Clear();

    var translatedFiles = this.GetSelectedTranslated();
    var currentFiles = this.GetSelectedOriginCurrents();
    var previousFiles = this.GetSelectedOriginPrevious();

    LsWorkingDataService.Load(
      translatedFiles: translatedFiles,
      currentFiles: currentFiles,
      previousFiles: previousFiles
    );

    this.TranslationGrid.ItemsSource = LsWorkingDataService.TranslatedItems;
  }

  [RelayCommand]
  private void LoadXmlFiles()
  {
    this.OriginPreviousFileItems.Clear();
    this.OriginCurrentFileItems.Clear();
    this.TranslatedFileItems.Clear();

    var selectedMods = this.GetSelectedMods().ToArray();

    this.xmlFilesService.Load(selectedMods);

    foreach (var xmlFileModel in this.xmlFilesService.Items)
    {
      if (xmlFileModel.Name.ToLower()
                      .StartsWith(@"english\"))
      {
        this.OriginPreviousFileItems.Add(new XmlFileListBoxItem(xmlFileModel));
      }

      if (xmlFileModel.Name.ToLower()
                      .StartsWith(@"english\"))
      {
        this.OriginCurrentFileItems.Add(new XmlFileListBoxItem(xmlFileModel));
      }

      if (!xmlFileModel.Name.ToLower()
                       .StartsWith(@"english\")) { this.TranslatedFileItems.Add(new XmlFileListBoxItem(xmlFileModel)); }
    }
  }

  [RelayCommand]
  private void PackMods() { this.DoPackMods(); }

  [RelayCommand]
  private void Refresh() { this.DoRefresh(); }

  private void ReLoadXmlFiles()
  {
    this.LoadXmlFiles();

    this.SetListBoxOriginCurrentFileSelections();
    this.SetListBoxOriginPreviousFileSelections();
    this.SetListBoxTranslatedFileSelections();
  }

  [RelayCommand]
  private void SaveXmlData()
  {
    var mods = this.GetSelectedMods();

    foreach (var mod in mods)
    {
      var message = $"Save Mod: {mod.Name}";
      Console.WriteLine(message);

      // this.ShowToast(message);

      var modFiles = this.GetSelectedTranslated()
                         .Where(t => t.Mod.Name == mod.Name);

      foreach (var xmlFileModel in modFiles)
      {
        var translatedItems = LsWorkingDataService.TranslatedItems.Where(t => t?.SourceFile == xmlFileModel);
        message = $"Save File: {xmlFileModel.Name} Items: {translatedItems.Count()}";
        Console.WriteLine(message);

        // this.ShowToast(message);
      }
    }

    this.IsModified = false;
  }

  [RelayCommand]
  private void ShowSettingsDialog()
  {
    var settingsDialog = new SettingsDialog();
    settingsDialog.Owner = this;
    settingsDialog.ShowDialog();
    this.LoadMods();
    this.ReLoadXmlFiles();
  }

  #endregion

}
