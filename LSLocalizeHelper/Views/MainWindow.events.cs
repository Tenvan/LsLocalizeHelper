using System;
using System.Globalization;
using System.Linq;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

using LSLocalizeHelper.Enums;
using LSLocalizeHelper.Models;
using LSLocalizeHelper.Services;

using ReactiveUI;

using Clipboard = System.Windows.Forms.Clipboard;

namespace LSLocalizeHelper.Views;

/// <summary>
/// Set of all Event Methods
/// </summary>
public partial class MainWindow
{

  #region Methods

  private void ButtonApplyTranslated_OnClick(object sender, RoutedEventArgs e) { this.DoApplyTranslatedText(); }

  private void ButtonCopyCurrent_OnClick(object sender, RoutedEventArgs e)
  {
    var translatedUid = this.CurrentDataRow?.Uuid;
    var current = LsWorkingDataService.GetCurrentForUid(translatedUid);
    App.SetClipboardText(current?.Text);
  }

  private void ButtonCopyPrevious_OnClick(object sender, RoutedEventArgs e)
  {
    var translatedUid = this.CurrentDataRow?.Uuid;
    var current = LsWorkingDataService.GetPreviousForUid(translatedUid);
    App.SetClipboardText(current?.Text);
  }

  private void ButtonCopyTranslated_OnExecuted(object sender, RoutedEventArgs routedEventArgs)
  {
    if (!string.IsNullOrEmpty(this.TextBoxTranslated.Text)) { App.SetClipboardText(this.TextBoxTranslated.Text); }
  }

  private void ButtonPasteTranslated_OnClick(object sender, RoutedEventArgs e)
  {
    this.TextBoxTranslated.Text = App.GetClipboardText();
    this.DoApplyTranslatedText();
  }

  private void ComboBoxLanguage_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
  {
    var selectedItem = this.ComboBoxLanguage.SelectedItem as CultureInfo;
    this.SetLocals(selectedItem);
  }

  private void ListBoxMods_OnChecked(object sender, RoutedEventArgs e)
  {
    this.SaveListBoxMods();
    this.ReLoadXmlFiles();
  }

  private void ListBoxOriginCurrent_OnChecked(object sender, RoutedEventArgs e)
  {
    var listBox = sender as CheckBox;
    var data = listBox?.DataContext;

    if (data is not XmlFileListBoxItem listBoxItem) { return; }

    // clear previous selections for this mod
    var listBoxItemsOfMod = this.ListBoxOriginCurrentFile.Items.Cast<XmlFileListBoxItem>()
                                .Where(n => n.FileModel.Mod.Name == listBoxItem.FileModel.Mod.Name && n != listBoxItem);

    foreach (var xmlFileListBoxItem in listBoxItemsOfMod) { xmlFileListBoxItem.IsChecked = false; }

    this.SaveListBoxOriginCurrentFile();
    this.ReLoadXmlFiles();
  }

  private void ListBoxOriginPrevious_OnChecked(object sender, RoutedEventArgs e)
  {
    var listBox = sender as CheckBox;
    var data = listBox?.DataContext;

    if (data is not XmlFileListBoxItem listBoxItem) { return; }

    // clear previous selections for this mod
    var listBoxItemsOfMod = this.ListBoxOriginPreviousFile.Items.Cast<XmlFileListBoxItem>()
                                .Where(n => n.FileModel.Mod.Name == listBoxItem.FileModel.Mod.Name && n != listBoxItem);

    foreach (var xmlFileListBoxItem in listBoxItemsOfMod) { xmlFileListBoxItem.IsChecked = false; }

    this.SaveListBoxOriginPreviousFile();
    this.ReLoadXmlFiles();
  }

  private void ListBoxTranslated_OnChecked(object sender, RoutedEventArgs e)
  {
    var listBox = sender as CheckBox;
    var data = listBox?.DataContext;

    if (data is not XmlFileListBoxItem listBoxItem) { return; }

    // clear previous selections for this mod
    var listBoxItemsOfMod = this.ListBoxOriginCurrentFile.Items.Cast<XmlFileListBoxItem>()
                                .Where(n => n.FileModel.Mod.Name == listBoxItem.FileModel.Mod.Name && n != listBoxItem);

    foreach (var xmlFileListBoxItem in listBoxItemsOfMod) { xmlFileListBoxItem.IsChecked = false; }

    this.SaveListBoxTranslatedFile();

    this.ReLoadXmlFiles();
  }

  private void SetEvents()
  {
    this.TranslationGrid.Events()
        .SelectionChanged.Throttle(TimeSpan.FromSeconds(0.4))
        .ObserveOn(RxApp.MainThreadScheduler)
        .Select(
           e => e.AddedItems.Count > 0
                  ? e.AddedItems[0] as DataRowModel
                  : null
         )
        .Subscribe(this.CopyToClipboardOnRowChanged);

    this.TranslationGrid.Events()
        .SelectionChanged.Select(
           e => e.AddedItems.Count > 0
                  ? e.AddedItems[0] as DataRowModel
                  : null
         )
        .Subscribe(this.SetTextBoxOnRowChanged);

    this.ListBoxMods.Events()
        .SelectionChanged.Where(args => !this.IsUpdating && false)
        .Select(e => e)
        .Subscribe(
           args =>
           {
             this.SaveListBoxMods();
             this.BeginUpdating();
             this.ReLoadXmlFiles();
             this.EndUpdating();
           }
         );

    this.ListBoxTranslatedFile.Events()
        .SelectionChanged.Where(args => !this.IsUpdating && false)
        .Select(e => e)
        .Subscribe(args => this.SaveListBoxTranslatedFile());

    this.ListBoxOriginCurrentFile.Events()
        .SelectionChanged.Where(args => !this.IsUpdating && false)
        .Select(e => e)
        .Subscribe(args => this.SaveListBoxOriginCurrentFile());

    this.ListBoxOriginPreviousFile.Events()
        .SelectionChanged.Where(args => !this.IsUpdating && false)
        .Select(e => e)
        .Subscribe(args => this.SaveListBoxOriginPreviousFile());

    this.TextBoxQuickSearch.Events()
        .TextChanged.Throttle(TimeSpan.FromMilliseconds(500))
        .ObserveOn(RxApp.MainThreadScheduler)
        .Subscribe(this.DoQuickFilter);

    this.GroupBoxProjects.Events()
        .SizeChanged.Throttle(TimeSpan.FromMilliseconds(500))
        .ObserveOn(RxApp.MainThreadScheduler)
        .Subscribe(this.DoGroupBoxProjectsOnSizeChanged);

    this.GroupBoxTranslation.Events()
        .SizeChanged.Throttle(TimeSpan.FromMilliseconds(500))
        .ObserveOn(RxApp.MainThreadScheduler)
        .Subscribe(this.DoGroupBoxTranslatioOnSizeChanged);
  }

  private void ShowToast(string message, int duration = 5)
  {
    this.MainSnackbar.MessageQueue?.Enqueue(
      message,
      "OK",
      (e) => { },
      null,
      true,
      true,
      TimeSpan.FromSeconds(duration)
    );
  }

  private void TranslationGrid_OnKeyUp(object sender, KeyEventArgs e)
  {
    Console.WriteLine("KeyUp: " + e.Key);

    switch (e.Key)
    {
      case Key.Delete:
        var item = this.TranslationGrid.SelectedItem as DataRowModel;

        if (item!.Status == TranslationStatus.Deleted) { LsWorkingDataService.RecalculateStatus(item); }
        else { item!.Status = TranslationStatus.Deleted; }

        break;
    }
  }

  private void WindowMain_LocationChanged(object sender, EventArgs e)
  {
    var userSettingsSettings = SettingsManager.Settings;

    if (userSettingsSettings != null)
    {
      userSettingsSettings.WindowTop = this.Top;
      userSettingsSettings.WindowLeft = this.Left;
    }

    SettingsManager.Save();
  }

  private void WindowMain_SizeChanged(object sender, SizeChangedEventArgs e)
  {
    var userSettingsSettings = SettingsManager.Settings;

    if (userSettingsSettings != null)
    {
      userSettingsSettings.WindowWidth = this.Width;
      userSettingsSettings.WindowHeight = this.Height;
    }

    SettingsManager.Save();
  }

  #endregion

}
