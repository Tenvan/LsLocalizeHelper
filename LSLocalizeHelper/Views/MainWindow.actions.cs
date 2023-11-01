using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml;
using System.Xml.Linq;

using Alphaleonis.Win32.Filesystem;

using LSLocalizeHelper.Enums;
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

  private void CopyToClipboardOnRowChanged(DataRowModel? row)
  {
    if (row == null) { return; }

    try
    {
      if (this.CheckBoxAutoClipboard.IsChecked == true
          && !string.IsNullOrEmpty(row?.Text)) { App.SetClipboardText(row.Text); }
    }
    catch (Exception ex) { Console.WriteLine(ex.Message); }
  }

  private XDocument CreateDocFromItems(IEnumerable<DataRowModel?> dataRowModels)
  {
    var xmlText = FileHelper.LoadFileTemplate("local.xml");
    var result = XDocument.Parse(xmlText);

    var contentList = result.Element("contentList");

    if (contentList == null) throw new Exception("Error on building new xdocument. No contentList found.");

    foreach (var dataRowModel in dataRowModels.OrderBy(r => r.Origin)
                                              .Where(d => d.Status != TranslationStatus.Deleted))
    {
      // contentList.Add(new XComment($"RowStatus on save: {dataRowModel.Status}"));

      contentList.Add(
        new XElement(
          name: "content",
          new XAttribute(name: "contentuid", value: dataRowModel.Uuid),
          new XAttribute(name: "version", value: dataRowModel.Version),
          dataRowModel.Text
        )
      );
    }

    return result;
  }

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

  private void DoLoadData()
  {
    LsWorkingDataService.Clear();

    var translatedFiles = this.GetSelectedTranslated();
    var currentFiles = this.GetSelectedOriginCurrents();
    var previousFiles = this.GetSelectedOriginPrevious();

    LsWorkingDataService.Load(
      translatedFiles: translatedFiles,
      currentFiles: currentFiles,
      previousFiles: previousFiles
    );

    this.TranslationGrid.ItemsSource = LsWorkingDataService.TranslatedItems;
    this.HasDataLoaded = true;
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
    finally
    {
      this.EndUpdating();
      this.IsModified = false;
    }
  }

  private void DoSaveData()
  {
    var mods = this.GetSelectedMods();

    try
    {
      foreach (var mod in mods)
      {
        var message = $"Save Mod: {mod.Name}";
        Console.WriteLine(message);

        var modFiles = this.GetSelectedTranslated()
                           .Where(t => t.Mod.Name == mod.Name);

        foreach (var xmlFileModel in modFiles)
        {
          var translatedItems = LsWorkingDataService.TranslatedItems.Where(t => t?.SourceFile == xmlFileModel)
                                                    .ToArray();

          var fileName = xmlFileModel.FullPath.FullName;
          var tempFileName = $"{fileName}.temp.xml";

          message = $"Save File: {fileName} Items: {translatedItems.Count()}";
          Console.WriteLine(message);

          var doc = this.CreateDocFromItems(translatedItems);
          doc.Save(tempFileName);
          File.Move(sourcePath: tempFileName, destinationPath: fileName, moveOptions: MoveOptions.ReplaceExisting);
        }
      }

      this.ShowToast("R-A97Cc11F-608E-46C9-9903-Eb5562C5Ba85".FromResource());
      this.IsModified = false;
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex);
      this.ShowToast("R-Ca10E94D-B276-49Ff-Bc4A-4A4D87082Ade".FromResource() + ":\n" + ex.Message);
    }
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
                            .Select(
                               xmlFileModel => new SelectionModel(
                                 modName: xmlFileModel.Mod.Name,
                                 name: xmlFileModel.Name
                               )
                             )
                            .ToArray();

    SettingsManager.Settings!.LastOriginsCurrent = selectedValue;
    SettingsManager.Save();
  }

  private void SaveListBoxOriginPreviousFile()
  {
    var selectedValue = this.GetSelectedOriginPrevious()
                            .Select(
                               xmlFileModel => new SelectionModel(
                                 modName: xmlFileModel.Mod.Name,
                                 name: xmlFileModel.Name
                               )
                             )
                            .ToArray();

    SettingsManager.Settings!.LastOriginsPrevious = selectedValue;
    SettingsManager.Save();
  }

  private void SaveListBoxTranslatedFile()
  {
    var selectedValue = this.GetSelectedTranslated()
                            .Select(
                               xmlFileModel => new SelectionModel(
                                 modName: xmlFileModel.Mod.Name,
                                 name: xmlFileModel.Name!
                               )
                             )
                            .ToArray();

    SettingsManager.Settings!.LastOriginsTranslated = selectedValue;
    SettingsManager.Save();
  }

  private void SetTextBoxOnRowChanged(DataRowModel? row)
  {
    this.TextBoxTranslated.Text = row?.Text;
    this.HasCurrentRow = row != null;
  }

  #endregion

}
