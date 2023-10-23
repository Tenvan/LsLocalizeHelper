using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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

  public LsWorkingDataService DataService { get; set; }

  #endregion

  #region Methods

  private void ButtonLoad_OnClick(
    object          sender,
    RoutedEventArgs e
  )
  {
    this.DataService = new LsWorkingDataService();

    this.DataService.Load(
                          this.TranslatedFileItems.ToArray(),
                          this.OriginCurrentFileItems.ToArray(),
                          this.OriginPreviousFileItems.ToArray()
                         );

    this.TranslationGrid.ItemsSource = this.DataService.TranslatedItems;
  }

  private void ButtonSave_OnClick(
    object          sender,
    RoutedEventArgs e
  )
  {
    throw new NotImplementedException();
  }

  private void DoOnRowChanged(
    DataRowModel? row
  )
  {
    this.TextBoxTranslated.Text = row?.Text;
    if (row?.Text != null) Clipboard.SetText(row.Text);
  }

  private void LoadMods()
  {
    this.bg3ModsService.LoadMods();
    this.ProjectItems.Clear();

    foreach (var modModel in this.bg3ModsService.Items) { this.ProjectItems.Add(modModel); }
  }

  private void LoadXmlFiles()
  {
    this.OriginPreviousFileItems.Clear();
    this.OriginCurrentFileItems.Clear();
    this.TranslatedFileItems.Clear();
    var selectedValue = this.ListBoxMods.SelectedItems.Cast<ModModel>().ToArray();
    this.XmlFilesService.Load(selectedValue);

    foreach (var xmlFileModel in this.XmlFilesService.Items)
    {
      this.OriginPreviousFileItems.Add(xmlFileModel);
      this.OriginCurrentFileItems.Add(xmlFileModel);
      this.TranslatedFileItems.Add(xmlFileModel);
    }
  }

  private bool ShowSettingsDialog()
  {
    var settingsDialog = new SettingsDialog();
    settingsDialog.ShowDialog();

    return settingsDialog.DialogResult == true;
  }

  #endregion
}