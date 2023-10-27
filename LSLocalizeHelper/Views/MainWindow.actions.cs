using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

using LSLocalizeHelper.Models;
using LSLocalizeHelper.Services;

namespace LSLocalizeHelper.Views;

/// <summary>
/// Methods for doing something.
/// </summary>
public partial class MainWindow
{

  #region Methods

  private void DoOnRowChanged(DataRowModel? row)
  {
    if (row == null || this.CheckBoxAutoClipboard.IsChecked == false)
    {
      return;
    }

    try
    {
      if (!string.IsNullOrEmpty(row.Text))
      {
        Clipboard.SetText(row.Text);
      }

      Console.WriteLine("Copy to Clipboard:" + row?.Text);
      this.TextBoxTranslated.Text = row?.Text;
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex.Message);
    }
  }

  private void DoQuickFilter(TextChangedEventArgs textChangedEventArgs)
  {
    var filterText = this.TextBoxQuickSearch.Text;
    Console.WriteLine($"QuickSearch: {filterText}");
    var filteredData = LsWorkingDataService.FilterData(filterText);
    this.TranslationGrid.ItemsSource = filteredData;

  }

  private void DoRefresh()
  {
    try
    {
      this.BeginUpdating();
      this.LoadMods();
      this.SetListBoxModsSelections();

      this.LoadXmlFiles();
      this.SetListBoxCurrentFileSelections();
      this.SetListBoxPreviousFileSelections();
      this.SetListBoxTranslatedFileSelections();
    }
    finally
    {
      this.EndUpdating();
    }
  }

  private void SaveListBoxMods()
  {
    var selectedValue = this.ListBoxMods.SelectedItems.Cast<ModModel>()
                            .Select(model => model.Name)
                            .ToArray();

    SettingsManager.Settings!.LastMods = selectedValue;
    SettingsManager.Save();
  }

  private void SaveListBoxOriginCurrentFile()
  {
    var selectedValue = this.ListBoxOriginCurrentFile.SelectedItems.Cast<XmlFileModel>()
                            .Select(
                               model => new SelectionModel()
                               {
                                 Name = model.Name,
                                 ModName = model.Mod.Name,
                               }
                             )
                            .ToArray();

    SettingsManager.Settings!.LastOriginsCurrent = selectedValue;
    SettingsManager.Save();
  }

  private void SaveListBoxOriginPreviousFile()
  {
    var selectedValue = this.ListBoxOriginPreviousFile.SelectedItems.Cast<XmlFileModel>()
                            .Select(
                               model => new SelectionModel()
                               {
                                 Name = model.Name,
                                 ModName = model.Mod.Name,
                               }
                             )
                            .ToArray();

    SettingsManager.Settings!.LastOriginsPrevious = selectedValue;
    SettingsManager.Save();
  }

  private void SaveListBoxTranslatedFile()
  {
    var selectedValue = this.ListBoxTranslatedFile.SelectedItems.Cast<XmlFileModel>()
                            .Select(
                               model => new SelectionModel()
                               {
                                 Name = model.Name,
                                 ModName = model.Mod.Name,
                               }
                             )
                            .ToArray();

    SettingsManager.Settings!.LastOriginsTranslated = selectedValue;
    SettingsManager.Save();
  }

  #endregion

  private void DoGroupBoxProjectsOnSizeChanged(SizeChangedEventArgs e)
  {
    if (!e.HeightChanged) { return; }
    SettingsManager.Settings.ProjectHeight = this.RowDefinitionProjects.Height.Value;
    SettingsManager.Save();
  }

  private void DoGroupBoxTranslatioOnSizeChanged(SizeChangedEventArgs e)
  {
    if (!e.HeightChanged) { return; }
    SettingsManager.Settings.TranslationHeight = this.RowDefinitionTranslation.Height.Value;
    SettingsManager.Save();
  }

}
