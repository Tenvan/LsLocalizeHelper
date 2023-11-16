using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;

using Google.Api.Gax.Grpc.Rest;
using Google.Api.Gax.ResourceNames;
using Google.Cloud.Translate.V3;

using LsLocalizeHelperLib.Enums;
using LsLocalizeHelperLib.Helper;
using LsLocalizeHelperLib.Models;
using LsLocalizeHelperLib.Services;

using Newtonsoft.Json;

using ReactiveUI;

using RestSharp;

using Clipboard = System.Windows.Forms.Clipboard;

namespace LsLocalizeHelper.Views;

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
    if (!string.IsNullOrEmpty(this.TextBoxTranslated.Text))
    {
      App.SetClipboardText(this.TextBoxTranslated.Text);
    }
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

    if (data is not XmlFileListBoxItem listBoxItem)
    {
      return;
    }

    // clear previous selections for this mod
    var listBoxItemsOfMod = this.ListBoxOriginCurrentFile.Items.Cast<XmlFileListBoxItem>()
                                .Where(n => n.FileModel.Mod.Name == listBoxItem.FileModel.Mod.Name && n != listBoxItem);

    foreach (var xmlFileListBoxItem in listBoxItemsOfMod)
    {
      xmlFileListBoxItem.IsChecked = false;
    }

    this.SaveListBoxOriginCurrentFile();
    this.ReLoadXmlFiles();
  }

  private void ListBoxOriginPrevious_OnChecked(object sender, RoutedEventArgs e)
  {
    var listBox = sender as CheckBox;
    var data = listBox?.DataContext;

    if (data is not XmlFileListBoxItem listBoxItem)
    {
      return;
    }

    // clear previous selections for this mod
    var listBoxItemsOfMod = this.ListBoxOriginPreviousFile.Items.Cast<XmlFileListBoxItem>()
                                .Where(n => n.FileModel.Mod.Name == listBoxItem.FileModel.Mod.Name && n != listBoxItem);

    foreach (var xmlFileListBoxItem in listBoxItemsOfMod)
    {
      xmlFileListBoxItem.IsChecked = false;
    }

    this.SaveListBoxOriginPreviousFile();
    this.ReLoadXmlFiles();
  }

  private void ListBoxTranslated_OnChecked(object sender, RoutedEventArgs e)
  {
    var listBox = sender as CheckBox;
    var data = listBox?.DataContext;

    if (data is not XmlFileListBoxItem listBoxItem)
    {
      return;
    }

    // clear previous selections for this mod
    var listBoxItemsOfMod = this.ListBoxOriginCurrentFile.Items.Cast<XmlFileListBoxItem>()
                                .Where(n => n.FileModel.Mod.Name == listBoxItem.FileModel.Mod.Name && n != listBoxItem);

    foreach (var xmlFileListBoxItem in listBoxItemsOfMod)
    {
      xmlFileListBoxItem.IsChecked = false;
    }

    this.SaveListBoxTranslatedFile();

    this.ReLoadXmlFiles();
  }

  private void SetEvents()
  {
    this.TranslationGrid.Events()
        .SelectionChanged.Throttle(TimeSpan.FromSeconds(0.4))
        .ObserveOn(RxApp.MainThreadScheduler)
        .Select(e => e.AddedItems.Count > 0 ? e.AddedItems[0] as DataRowModel : null)
        .Subscribe(this.CopyToClipboardOnRowChanged);

    this.TranslationGrid.Events()
        .SelectionChanged.Select(e => e.AddedItems.Count > 0 ? e.AddedItems[0] as DataRowModel : null)
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
      content: message,
      actionContent: "OK",
      actionHandler: (e) => { },
      actionArgument: null,
      promote: true,
      neverConsiderToBeDuplicate: true,
      durationOverride: TimeSpan.FromSeconds(duration)
    );
  }

  private void TranslationGrid_OnKeyUp(object sender, KeyEventArgs e)
  {
    switch (e.Key)
    {
      case Key.Delete:
        var item = this.TranslationGrid.SelectedItem as DataRowModel;

        if (item!.Status == TranslationStatus.Deleted)
        {
          LsWorkingDataService.RecalculateStatus(item);
        }
        else
        {
          item!.Status = TranslationStatus.Deleted;
        }

        break;
    }
  }

  private void TranslationGrid_OnSorting(object sender, DataGridSortingEventArgs e)
  {
    // Cancel the built-in sort to enable the custom sort. 
    e.Handled = true;
    var hasShiftKey = Keyboard.Modifiers.HasFlag(ModifierKeys.Shift);
    var hasControlKey = Keyboard.Modifiers.HasFlag(ModifierKeys.Control);

    this.sortingHelper.ColumnClicked(
      column: e.Column.SortMemberPath,
      hasControlKey: hasControlKey,
      hasShiftKey: hasControlKey
    );

    this.sortingHelper.SyncToGrid();

    Console.WriteLine($"count sortingHelper SortDescription: {this.sortingHelper.Sortings.Count}");
    Console.WriteLine($"count translationGrid SortDescription: {this.TranslationGrid.Items.SortDescriptions.Count}");
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

  private async void ButtonTranslateCurrent_MyMemory_OnClick(object sender, RoutedEventArgs e)
  {
    try
    {
      var input = this.CurrentDataRow?.Origin;
      var sl = SettingsManager.Settings?.SourceLanguage;
      var tl = SettingsManager.Settings?.TargetLanguage;

      var requestUrl
        = $"https://translated-mymemory---translation-memory.p.rapidapi.com/get?langpair={sl}%7C{tl}&q={Uri.EscapeDataString(input)}&mt=1&onlyprivate=0&de=a%40b.c";

      var client = new RestClient(requestUrl);
      var request = new RestRequest();

      request.AddHeader(name: "X-RapidAPI-Key", value: SettingsManager.Settings.RapidApiKey);
      request.AddHeader(name: "X-RapidAPI-Host", value: "translated-mymemory---translation-memory.p.rapidapi.com");

      var response = await client.ExecuteAsync(request);

      if (response.IsSuccessful)
      {
        dynamic jsonResponse = JsonConvert.DeserializeObject(response.Content);
        string translated = jsonResponse.responseData.translatedText;

        this.TextBoxTranslated.Text = translated;
      }
      else
      {
        this.ShowToast("API call unsuccessful: " + response.ErrorMessage);
      }
    }
    catch (Exception exception)
    {
      this.ShowToast(exception.Message);
    }
  }

  private async void ButtonTranslateCurrent_Microsoft_OnClick(object sender, RoutedEventArgs e)
  {
    try
    {
      var input = this.CurrentDataRow?.Origin;
      var sl = SettingsManager.Settings?.SourceLanguage;
      var tl = SettingsManager.Settings?.TargetLanguage;

      var requestUrl
        = $"https://microsoft-translator-text.p.rapidapi.com/translate?from={sl}&to%5B0%5D={tl}&api-version=3.0&profanityAction=NoAction&textType=plain";

      var client = new RestClient(requestUrl);
      var request = new RestRequest();

      request.AddHeader(name: "content-type", value: "application/json");
      request.AddHeader(name: "X-RapidAPI-Key", value: SettingsManager.Settings.RapidApiKey);
      request.AddHeader(name: "X-RapidAPI-Host", value: "microsoft-translator-text.p.rapidapi.com");

      var jsonStr = string.Format(format: @"[ {{ ""Text"": ""{0}"" }}]", arg0: input);

      request.AddParameter(
        name: "application/json",
        value: jsonStr,
        type: ParameterType.RequestBody
      );

      var response = await client.ExecuteAsync(request: request, httpMethod: Method.Post);

      if (response.IsSuccessful)
      {
        dynamic jsonResponse = JsonConvert.DeserializeObject(response.Content);
        var responses = jsonResponse[0];
        var translations = responses.translations;
        var translated = translations[0].text;

        this.TextBoxTranslated.Text = translated;
      }
      else
      {
        this.ShowToast("API call unsuccessful: " + response.ErrorMessage);
      }
    }
    catch (Exception exception)
    {
      this.ShowToast(exception.Message);
    }
  }

}
