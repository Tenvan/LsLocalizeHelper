using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

using CommunityToolkit.Mvvm.Input;

using LSLocalizeHelper.Helper;
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

  private void LoadData()
  {
    LsWorkingDataService.Clear();

    var translatedFiles = this.ListBoxTranslatedFile.SelectedItems.Cast<XmlFileModel>()
                              .ToArray();

    var currentFiles = this.ListBoxOriginCurrentFile.SelectedItems.Cast<XmlFileModel>()
                           .ToArray();

    var previousFiles = this.ListBoxOriginPreviousFile.SelectedItems.Cast<XmlFileModel>()
                            .ToArray();

    LsWorkingDataService.Load(
      translatedFiles: translatedFiles,
      currentFiles: currentFiles,
      previousFiles: previousFiles
    );

    this.TranslationGrid.ItemsSource = LsWorkingDataService.TranslatedItems;
  }

  private void LoadMods()
  {
    this.bg3ModsService.LoadMods();
    this.ProjectItems.Clear();

    foreach (var modModel in this.bg3ModsService.Items) { this.ProjectItems.Add(modModel); }
  }

  [RelayCommand]
  private void LoadXmlData() { this.LoadData(); }

  private void LoadXmlFiles()
  {
    this.OriginPreviousFileItems.Clear();
    this.OriginCurrentFileItems.Clear();
    this.TranslatedFileItems.Clear();

    var selectedMods = this.ListBoxMods.SelectedItems.Cast<ModModel>()
                           .ToArray();

    this.XmlFilesService.Load(selectedMods);

    foreach (var xmlFileModel in this.XmlFilesService.Items)
    {
      this.OriginPreviousFileItems.Add(xmlFileModel);
      this.OriginCurrentFileItems.Add(xmlFileModel);
      this.TranslatedFileItems.Add(xmlFileModel);
    }
  }

  [RelayCommand]
  private void PackMods() { this.DoPackMods(); }

  [RelayCommand]
  private void Refresh() { this.DoRefresh(); }

  [RelayCommand]
  private void SaveXmlData() { MessageBox.Show("R-C109D0A4-5995-4Fda-957A-55Bd3F527043".FromResource()); }

  [RelayCommand]
  private void ShowSettingsDialog()
  {
    var settingsDialog = new SettingsDialog();
    settingsDialog.Owner = this;
    settingsDialog.ShowDialog();
  }

  #endregion

}
