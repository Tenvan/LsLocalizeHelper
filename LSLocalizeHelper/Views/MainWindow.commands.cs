using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using DynamicData;

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

  private void LoadData()
  {
    LsWorkingDataService.Clear();

    var translatedFiles = this.ListBoxTranslatedFile.SelectedItems.Cast<XmlFileModel>()
                              .ToArray();

    var currentFiles = this.ListBoxOriginCurrentFile.SelectedItems.Cast<XmlFileModel>()
                           .ToArray();

    var previousFiles = this.ListBoxOriginPreviousFile.SelectedItems.Cast<XmlFileModel>()
                            .ToArray();

    LsWorkingDataService.Load(translatedFiles: translatedFiles, currentFiles: currentFiles, previousFiles: previousFiles);

    this.TranslationGrid.ItemsSource = LsWorkingDataService.TranslatedItems;
  }

  private void LoadMods()
  {
    this.bg3ModsService.LoadMods();
    this.ProjectItems.Clear();

    foreach (var modModel in this.bg3ModsService.Items)
    {
      this.ProjectItems.Add(modModel);
    }
  }

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

  private void ShowSettingsDialog(object? _)
  {
    var settingsDialog = new SettingsDialog();
    settingsDialog.ShowDialog();
  }

  #endregion

  private void InitCommands()
  {
    this.OpenSettingsDialogCommand = new AnotherCommandImplementation(this.ShowSettingsDialog);
  }

  private void OpenModPathDialog(object? obj) {  }

  public ICommand OpenSettingsDialogCommand { get; set; }

}
