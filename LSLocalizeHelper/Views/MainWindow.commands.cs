using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

using LSLocalizeHelper.Models;
using LSLocalizeHelper.Services;
using LSLocalizeHelper.Views.Settings;

namespace LSLocalizeHelper.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{

  #region Properties

  public ObservableCollection<DataRowModel> DataGridItems { get; } = LsWorkingDataService.TranslatedItems;

  #endregion

  #region Methods

  private void ButtonLoad_OnClick(object sender, RoutedEventArgs e)
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

  private void ButtonSave_OnClick(object sender, RoutedEventArgs e)
  {
    throw new NotImplementedException();
  }

  private void DoOnRowChanged(DataRowModel? row)
  {
    if (row != null)
    {
      return;
    }

    try
    {
      if (row?.Text != null)
      {
        Clipboard.SetText(row.Text);
      }

      Console.WriteLine("Copy to Clipboard:" + row?.Text);
      this.TextBoxTranslated.Text = row?.Text;
      this.SetOriginTexts(row!.Uuid);
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex.Message);
    }
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

    var selectedValue = this.ListBoxMods.SelectedItems.Cast<ModModel>()
                            .ToArray();

    this.XmlFilesService.Load(selectedValue);

    foreach (var xmlFileModel in this.XmlFilesService.Items)
    {
      this.OriginPreviousFileItems.Add(xmlFileModel);
      this.OriginCurrentFileItems.Add(xmlFileModel);
      this.TranslatedFileItems.Add(xmlFileModel);
    }
  }

  private void SetOriginTexts(string uuid)
  {
    var currentOrigin = LsWorkingDataService.OriginCurrentItems.First(o => o.Uuid == uuid);
    this.TextBoxCurrentOrigin.Text = currentOrigin.Text;
    var previousOrigin = LsWorkingDataService.OriginPreviousItems.First(o => o.Uuid == uuid);
    this.TextBoxPreviousOrigin.Text = previousOrigin.Text;
  }

  private bool ShowSettingsDialog()
  {
    var settingsDialog = new SettingsDialog();
    settingsDialog.ShowDialog();

    return settingsDialog.DialogResult == true;
  }

  #endregion

}
