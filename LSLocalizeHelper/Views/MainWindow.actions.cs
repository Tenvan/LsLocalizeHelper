using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

using LSLocalizeHelper.Helper;
using LSLocalizeHelper.Models;
using LSLocalizeHelper.Services;

namespace LSLocalizeHelper.Views;

/// <summary>
/// Methods for doing something.
/// </summary>
public partial class MainWindow
{

  #region Methods

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

  private void DoImportMod()
  {
    var formImport = new ImportDialog
    {
      Owner = this,
    };

    var result = formImport.ShowDialog();

    switch (result)
    {
      case false when formImport.Error == null:
        return;

      case true:
        this.ShowToast("R-91E83186-18D2-43B6-Bcbd-1A592E5B6341".FromResource());

        break;

      default:
        this.ShowToast($"{"R-2417E08C-347F-4D57-925D-Cefdb628Fc7E".FromResource()}: {formImport.Error}");

        break;
    }
  }

  private void DoOnRowChanged(DataRowModel? row)
  {
    if (row == null) { return; }

    this.TextBoxTranslated.Text = row?.Text;

    try
    {
      if (this.CheckBoxAutoClipboard.IsChecked == true
          && !string.IsNullOrEmpty(row?.Text)) { App.SetClipboardText(row.Text); }
    }
    catch (Exception ex) { Console.WriteLine(ex.Message); }
  }

  private void DoPackMods()
  {
    foreach (var modModel in this.GetSelectedMods())
    {
      var modEngine = new LsPackageEngine(modPathEngine: modModel.Folder.FullName, modNameEngine: modModel.Name!);
      var result = modEngine.BuildPackage();

      if (result == null)
      {
        var message = string.Format(
          format: "R-F2Fcb892-8135-48Ac-Af05-14F0E47034Ab".FromResource(),
          arg0: modModel.Name
        );

        this.ShowToast(message: message, duration: 3);
      }
      else
      {
        var message = string.Format(
          format: "R-C109D0A4-5995-4Fda-957A-55Bd3F527043".FromResource(),
          arg0: modModel.Name,
          arg1: result
        );

        this.ShowToast(message: message, duration: 3);
      }
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
      this.ReLoadXmlFiles();
    }
    finally { this.EndUpdating(); }
  }

  private void SaveListBoxMods()
  {
    var selectedValue = this.GetSelectedMods()
                            .Select(model => model.Name)
                            .ToArray();

    SettingsManager.Settings!.LastMods = selectedValue;
    SettingsManager.Save();
  }

  private void SaveListBoxOriginCurrentFile()
  {
    var selectedValue = this.GetSelectedOriginCurrents()
                            .Select(xmlFileModel => new SelectionModel(modName: xmlFileModel.Mod.Name, name: xmlFileModel.Name))
                            .ToArray();

    SettingsManager.Settings!.LastOriginsCurrent = selectedValue;
    SettingsManager.Save();
  }

  private void SaveListBoxOriginPreviousFile()
  {
    var selectedValue = this.GetSelectedOriginPrevious()
                            .Select(xmlFileModel => new SelectionModel(modName: xmlFileModel.Mod.Name, name: xmlFileModel.Name))
                            .ToArray();

    SettingsManager.Settings!.LastOriginsPrevious = selectedValue;
    SettingsManager.Save();
  }

  private void SaveListBoxTranslatedFile()
  {
    var selectedValue = this.GetSelectedTranslated()
                            .Select(xmlFileModel => new SelectionModel(modName: xmlFileModel.Mod.Name, name: xmlFileModel.Name!))
                            .ToArray();

    SettingsManager.Settings!.LastOriginsTranslated = selectedValue;
    SettingsManager.Save();
  }

  #endregion

}
