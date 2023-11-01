using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Input;

using CommunityToolkit.Mvvm.ComponentModel;

using LSLocalizeHelper.Models;
using LSLocalizeHelper.Services;

using Clipboard = System.Windows.Forms.Clipboard;

namespace LSLocalizeHelper.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{

  #region Fields

  private readonly LsModsService lsModsService = new();

  private readonly XmlFilesService xmlFilesService = new();

  private bool hasCurrentRow;

  private bool hasDataLoaded;

  private bool isModified;

  private bool isNotModified = true;

  private string modifiedText = "Saved";

  #endregion

  #region Properties

  public bool HasCurrentRow
  {
    get => this.hasCurrentRow;

    set
    {
      this.SetProperty(ref this.hasCurrentRow, value);
      this.OnPropertyChanged();
    }
  }

  public bool HasDataLoaded
  {
    get => this.hasDataLoaded;

    set
    {
      if (value == this.hasDataLoaded) { return; }

      this.hasDataLoaded = value;
      this.OnPropertyChanged();
    }
  }

  public bool IsModified
  {
    get => this.isModified;

    set
    {
      this.isModified = value;
      this.IsNotModified = !value;
      this.OnPropertyChanged();

      this.ModifiedText = value
                            ? "Modified"
                            : "Saved";
    }
  }

  public bool IsNotModified
  {
    get => this.isNotModified;

    set
    {
      this.isNotModified = value;
      this.OnPropertyChanged();
    }
  }

  public ObservableCollection<CultureInfo> LanguageItems { get; set; } = new();

  public string ModifiedText { get => this.modifiedText; set => this.SetProperty(ref this.modifiedText, value); }

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
