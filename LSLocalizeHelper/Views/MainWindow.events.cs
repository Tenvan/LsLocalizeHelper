using System;
using System.Globalization;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

using LSLocalizeHelper.Models;
using LSLocalizeHelper.Services;

using ReactiveUI;

namespace LSLocalizeHelper.Views;

/// <summary>
/// Set of all Event Methods
/// </summary>
public partial class MainWindow
{

  #region Methods

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

  private void ComboBoxLanguage_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
  {
    var selectedItem = this.ComboBoxLanguage.SelectedItem as CultureInfo;
    this.SetLocals(selectedItem);
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
        .Subscribe(this.DoOnRowChanged);

    this.ListBoxMods.Events()
        .SelectionChanged.Where(args => !this.IsUpdating)
        .Select(e => e)
        .Subscribe(
           args =>
           {
             this.SaveListBoxMods();
             this.BeginUpdating();
             this.LoadXmlFiles();
             this.SetListBoxCurrentFileSelections();
             this.SetListBoxPreviousFileSelections();
             this.SetListBoxTranslatedFileSelections();
             this.EndUpdating();
           }
         );

    this.ListBoxTranslatedFile.Events()
        .SelectionChanged.Where(args => !this.IsUpdating)
        .Select(e => e)
        .Subscribe(args => this.SaveListBoxTranslatedFile());

    this.ListBoxOriginCurrentFile.Events()
        .SelectionChanged.Where(args => !this.IsUpdating)
        .Select(e => e)
        .Subscribe(args => this.SaveListBoxOriginCurrentFile());

    this.ListBoxOriginPreviousFile.Events()
        .SelectionChanged.Where(args => !this.IsUpdating)
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
