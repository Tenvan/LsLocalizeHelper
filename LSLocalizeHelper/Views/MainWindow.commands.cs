using System.Collections.ObjectModel;
using System.Linq;

using CommunityToolkit.Mvvm.Input;

using LsLocalizeHelperLib.Models;
using LsLocalizeHelperLib.Services;

namespace LsLocalizeHelper.Views;

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
  private void LoadXmlData() { this.DoLoadData(); }

  [RelayCommand]
  private void LoadXmlFiles()
  {
    this.OriginPreviousFileItems.Clear();
    this.OriginCurrentFileItems.Clear();
    this.TranslatedFileItems.Clear();

    var selectedMods = this.GetSelectedMods()
                           .ToArray();

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
    this.DoSaveData();
    this.DoLoadData();
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
