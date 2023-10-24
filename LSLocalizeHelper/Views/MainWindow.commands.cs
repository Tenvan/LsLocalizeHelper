using System;
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

  public LsWorkingDataService DataService { get; set; }

  #endregion

  #region Methods

  private void ButtonLoad_OnClick(object sender, RoutedEventArgs e)
  {
    this.DataService = new LsWorkingDataService();

    this.DataService.Load(
      translatedFiles: this.TranslatedFileItems.ToArray(),
      currentFiles: this.OriginCurrentFileItems.ToArray(),
      previousFiles: this.OriginPreviousFileItems.ToArray()
    );

    this.TranslationGrid.ItemsSource = this.DataService.TranslatedItems;
  }

  private void ButtonSave_OnClick(object sender, RoutedEventArgs e)
  {
    throw new NotImplementedException();
  }

  private void DoOnRowChanged(DataRowModel? row)
  {
    Console.WriteLine("Copy to Clipboard:" + row?.Text);
    this.TextBoxTranslated.Text = row?.Text;

    try
    {
      if (row?.Text != null)
      {
        Clipboard.SetText(row.Text);
      }
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex.Message);
    }

    this.SetOriginTexts(row.Uuid);
  }

  private void SetOriginTexts(string uuid)
  {
    var currentOrigin = this.DataService.OriginCurrentItems.First(o => o.Uuid == uuid);
    this.TextBoxCurrentOrigin.Text = currentOrigin.Text;
    var previousOrigin = this.DataService.OriginPreviousItems.First(o => o.Uuid == uuid);
    this.TextBoxPreviousOrigin.Text = previousOrigin.Text;
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

  private bool ShowSettingsDialog()
  {
    var settingsDialog = new SettingsDialog();
    settingsDialog.ShowDialog();

    return settingsDialog.DialogResult == true;
  }

  #endregion

}
