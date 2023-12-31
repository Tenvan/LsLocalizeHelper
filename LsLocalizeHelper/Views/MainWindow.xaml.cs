﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

using LsLocalizeHelperLib.Helper;
using LsLocalizeHelperLib.Models;
using LsLocalizeHelperLib.Services;

namespace LsLocalizeHelper.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window,
                                  INotifyPropertyChanged
{

  #region Fields

  private FlowDocument originCurrentFlowDoc = new();

  #endregion

  #region Constructors

  public MainWindow()
  {
    this.InitializeComponent();
    this.sortingHelper = new SortingHelper(this.TranslationGrid);

    this.SetLocals(CultureInfo.CurrentCulture);

    this.LoadWindowSettings();

    this.LoadMods();
    this.SetListBoxModsSelections();
    this.ReLoadXmlFiles();

    this.SetEvents();
  }

  #endregion

  #region Properties

  public FlowDocument OriginCurrentFlowDoc
  {
    get => this.originCurrentFlowDoc;

    set
    {
      if (Equals(objA: value, objB: this.originCurrentFlowDoc))
      {
        return;
      }

      this.originCurrentFlowDoc = value;
      this.OnPropertyChanged();
    }
  }

  private DataRowModel? CurrentDataRow
  {
    get
    {
      var currentRow = this.TranslationGrid.SelectedItem as DataRowModel;

      return currentRow;
    }
  }

  #endregion

  #region Methods

  private void BeginUpdating() { this.IsUpdating = true; }

  private void EndUpdating() { this.IsUpdating = false; }

  protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
  {
    this.PropertyChanged?.Invoke(sender: this, e: new PropertyChangedEventArgs(propertyName));
  }

  protected virtual bool SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
  {
    if (EqualityComparer<T>.Default.Equals(x: field, y: value)) return false;

    field = value;
    this.OnPropertyChanged(propertyName);

    return true;
  }

  private void LoadWindowSettings()
  {
    this.Width = SettingsManager.Settings.WindowWidth;
    this.Height = SettingsManager.Settings.WindowHeight;
    this.Top = SettingsManager.Settings.WindowTop;
    this.Left = SettingsManager.Settings.WindowLeft;
    var settingsProjectHeight = (double)SettingsManager.Settings.ProjectHeight;
    this.RowDefinitionProjects.Height = new GridLength(Math.Max(val1: settingsProjectHeight, val2: 100));
    var settingsTranslationHeight = (double)SettingsManager.Settings.TranslationHeight;
    this.RowDefinitionTranslation.Height = new GridLength(Math.Max(val1: settingsTranslationHeight, val2: 100));
  }

  private void SetListBoxModsSelections()
  {
    // Reload selected Mods
    foreach (var lastMod in SettingsManager.Settings.LastMods)
    {
      this.SetModSelectionByName(modName: lastMod, selected: true);
    }
  }

  private void SetListBoxOriginCurrentFileSelections()
  {
    // Reload selected Current Origin files
    foreach (var lastMod in SettingsManager.Settings.LastOriginsCurrent)
    {
      var fileListBoxItem = this.OriginCurrentFileItems.FirstOrDefault(
        listBoxItem => listBoxItem.FileModel.Name == lastMod.Name && listBoxItem.FileModel.Mod.Name == lastMod.ModName
      );

      this.SetOriginCurrentSelection(fileModel: fileListBoxItem, selected: true);
    }
  }

  private void SetListBoxOriginPreviousFileSelections()
  {
    // Reload selected Previous Origin files
    foreach (var lastMod in SettingsManager.Settings.LastOriginsPrevious)
    {
      var item = this.OriginPreviousFileItems.FirstOrDefault(
        listBoxItem => listBoxItem.FileModel?.Name == lastMod.Name && listBoxItem.FileModel.Mod.Name == lastMod.ModName
      );

      this.SetOriginPreviousSelection(fileModel: item, selected: true);
    }
  }

  private void SetListBoxTranslatedFileSelections()
  {
    // Reload selected Previous Origin files
    foreach (var lastMod in SettingsManager.Settings.LastOriginsTranslated)
    {
      var item = this.TranslatedFileItems.FirstOrDefault(
        listBoxItem => listBoxItem.FileModel.Name == lastMod.Name && listBoxItem.FileModel.Mod.Name == lastMod.ModName
      );

      this.SetTranslatedSelection(fileModel: item, selected: true);
    }
  }

  private void SetLocals(CultureInfo culture)
  {
    var languages = new[]
    {
      "de-DE",
      "en-US",
      "zh-CN",
      "zh-TW", };

    if (this.LanguageItems.Count == 0)
    {
      foreach (var language in languages)
      {
        this.LanguageItems.Add(new CultureInfo(language));
      }
    }

    // this.Language = culture;
    var lower = culture.IetfLanguageTag;

    var oldDict = Application.Current.Resources.MergedDictionaries.FirstOrDefault(
      d => d.Source != null && d.Source.OriginalString.ToLower().Contains($"locals.{lower.ToLower()}")
    );

    if (oldDict != null)
    {
      Application.Current.Resources.MergedDictionaries.Remove(oldDict);
    }

    var isKnownLanguage = languages.Where(l => l == culture.IetfLanguageTag).ToArray().Any();

    var source = isKnownLanguage
                   ? new Uri(uriString: $"locals/locals.{culture}.xaml", uriKind: UriKind.Relative)
                   : new Uri(uriString: $"locals/locals.en-US.xaml", uriKind: UriKind.Relative);

    var newDict = new ResourceDictionary
    {
      Source = source,
    };

    Application.Current.Resources.MergedDictionaries.Add(newDict);

    Thread.CurrentThread.CurrentCulture = culture;
    Thread.CurrentThread.CurrentUICulture = culture;
  }

  #endregion

  #region All Other Members

  public event PropertyChangedEventHandler? PropertyChanged;

  #endregion

  private void CheckBoxTranslateAll_OnChecked(object sender, RoutedEventArgs e) { this.TranslateAll = true; }

  private void CheckBoxTranslateAll_OnUnChecked(object sender, RoutedEventArgs e) { this.TranslateAll = false; }

}
