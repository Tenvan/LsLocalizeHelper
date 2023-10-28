using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

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

  public ICommand ApplicationExitCommand { get; set; }

  public ICommand ImportNewModCommand { get; set; }

  public ICommand LoadXmlDataCommand { get; set; }

  public ICommand OpenSettingsDialogCommand { get; set; }

  public ICommand PackModsCommand { get; set; }

  public ICommand RefreshCommand { get; set; }

  public ICommand SaveXmlDataCommand { get; set; }

  #endregion

  #region Methods

  private void ApplicatinExit(object? obj) { this.Close(); }

  private void ImportNewMod(object? obj) { this.DoImportMod(); }

  private void InitCommands()
  {
    this.ApplicationExitCommand = new AnotherCommandImplementation(this.ApplicatinExit);
    this.ImportNewModCommand = new AnotherCommandImplementation(this.ImportNewMod);
    this.LoadXmlDataCommand = new AnotherCommandImplementation(this.LoadXmlData);
    this.OpenSettingsDialogCommand = new AnotherCommandImplementation(this.ShowSettingsDialog);
    this.PackModsCommand = new AnotherCommandImplementation(this.PackMods);
    this.RefreshCommand = new AnotherCommandImplementation(this.Refresh);
    this.SaveXmlDataCommand = new AnotherCommandImplementation(this.SaveXmlData);
  }

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

  private void LoadXmlData(object? obj) { this.LoadData(); }

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

  private void PackMods(object? obj) { this.DoPackMods(); }

  private void Refresh(object? obj) { this.DoRefresh(); }

  private void SaveXmlData(object? obj)
  {
    MessageBox.Show("R-C109D0A4-5995-4Fda-957A-55Bd3F527043".FromResource());
  }

  private void ShowSettingsDialog(object? _)
  {
    var settingsDialog = new SettingsDialog();
    settingsDialog.Owner = this;
    settingsDialog.ShowDialog();
  }

  #endregion

}
