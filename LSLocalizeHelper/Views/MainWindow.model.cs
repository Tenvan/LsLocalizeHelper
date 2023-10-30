using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;

using LSLocalizeHelper.Models;
using LSLocalizeHelper.Services;

namespace LSLocalizeHelper.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{

  #region Fields

  private readonly LsModsService lsModsService = new();

  private readonly XmlFilesService xmlFilesService = new();

  #endregion

  #region Properties

  public bool IsModified { get; set; } = false;

  public ObservableCollection<CultureInfo> LanguageItems { get; set; } = new();

  public ObservableCollection<XmlFileListBoxItem> OriginCurrentFileItems { get; set; } = new();

  public ObservableCollection<XmlFileListBoxItem> OriginPreviousFileItems { get; set; } = new();

  public ObservableCollection<ModModelListBoxItem> ProjectItems { get; set; } = new();

  public ObservableCollection<XmlFileListBoxItem> TranslatedFileItems { get; set; } = new();

  private bool IsUpdating { get; set; } = false;

  #endregion

  #region Methods

  private IEnumerable<ModModel> GetSelectedMods()
  {
    return this.ListBoxMods.Items.Cast<ModModelListBoxItem>()
               .Where(n => n.IsChecked)
               .Select(item => item.Mod);
  }

  private IEnumerable<XmlFileModel> GetSelectedOriginCurrents()
  {
    return this.ListBoxOriginCurrentFile.Items.Cast<XmlFileListBoxItem>()
               .Where(n => n.IsChecked)
               .Select(item => item.FileModel);
  }

  private IEnumerable<XmlFileModel> GetSelectedOriginPrevious()
  {
    return this.ListBoxOriginPreviousFile.Items.Cast<XmlFileListBoxItem>()
               .Where(n => n.IsChecked)
               .Select(item => item.FileModel);
  }

  private IEnumerable<XmlFileModel> GetSelectedTranslated()
  {
    return this.ListBoxTranslatedFile.Items.Cast<XmlFileListBoxItem>()
               .Where(n => n.IsChecked)
               .Select(item => item.FileModel);
  }

  private void SetModSelectionByName(string? modName, bool selected)
  {
    var mod = this.ListBoxMods.Items.Cast<ModModelListBoxItem>()
                  .FirstOrDefault(m => m.Mod.Name == modName);

    if (mod == null) { return; }

    mod.IsChecked = selected;
  }

  private void SetOriginCurrentSelection(XmlFileListBoxItem? fileModel, bool selected)
  {
    var item = this.ListBoxOriginCurrentFile.Items.Cast<XmlFileListBoxItem>()
                   .FirstOrDefault(m => m == fileModel);

    if (item == null) { return; }

    item.IsChecked = selected;
  }

  private void SetOriginPreviousSelection(XmlFileListBoxItem? fileModel, bool selected)
  {
    if (fileModel == null) { return; }

    var item = this.ListBoxOriginPreviousFile.Items.Cast<XmlFileListBoxItem>()
                   .FirstOrDefault(m => m == fileModel);

    if (item == null) { return; }

    item.IsChecked = selected;
  }

  private void SetTranslatedSelection(XmlFileListBoxItem? fileModel, bool selected)
  {
    if (fileModel == null) { return; }

    var item = this.ListBoxTranslatedFile.Items.Cast<XmlFileListBoxItem>()
                   .FirstOrDefault(m => m == fileModel);

    if (item == null) { return; }

    item.IsChecked = selected;
  }

  #endregion

}
