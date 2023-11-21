using System;
using System.Linq;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.Input;

using LsLocalizeHelperLib.Models;

namespace LsLocalizeHelper.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{

  #region Methods

  [RelayCommand]
  private void ApplicationExit() { this.Close(); }

  [RelayCommand]
  private void ImportNewMod() { this.DoImportMod(); }

  private void LoadMods()
  {
    this.lsModsService.LoadMods();
    this.ProjectItems.Clear();

    foreach (var modModel in this.lsModsService.Items)
    {
      this.ProjectItems.Add(new ModModelListBoxItem(modModel));
    }
  }

  [RelayCommand]
  private async void LoadXmlData()
  {
    try
    {
      this.ProgressBarGrid.IsIndeterminate = true;
      this.DoLoadData();
      this.RefreshStatusBar();
      this.RestoreOrder();
    }
    catch (Exception ex)
    {
      this.ShowToast(ex.Message);
    }
    finally
    {
      this.ProgressBarGrid.IsIndeterminate = false;
    }
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
      if (xmlFileModel.Name.ToLower().StartsWith(@"english\"))
      {
        this.OriginPreviousFileItems.Add(new XmlFileListBoxItem(xmlFileModel));
      }

      if (xmlFileModel.Name.ToLower().StartsWith(@"english\"))
      {
        this.OriginCurrentFileItems.Add(new XmlFileListBoxItem(xmlFileModel));
      }

      if (!xmlFileModel.Name.ToLower().StartsWith(@"english\"))
      {
        this.TranslatedFileItems.Add(new XmlFileListBoxItem(xmlFileModel));
      }
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
