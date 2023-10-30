using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;

using LSLocalizeHelper.Enums;
using LSLocalizeHelper.Models;

namespace LSLocalizeHelper.Services;

public static class LsWorkingDataService
{

  #region Static Properties

  public static IEnumerable<XmlFileModel> CurrentFiles { get; set; }

  public static ObservableCollection<OriginModel> OriginCurrentItems { get; set; } = new();

  public static ObservableCollection<OriginModel> OriginPreviousItems { get; set; } = new();

  public static IEnumerable<XmlFileModel> PreviousFiles { get; set; }

  public static IEnumerable<XmlFileModel> TranslatedFiles { get; set; }

  public static ObservableCollection<DataRowModel?> TranslatedItems { get; set; } = new();

  #endregion

  #region Static Methods

  public static void Clear()
  {
    LsWorkingDataService.OriginCurrentItems.Clear();
    LsWorkingDataService.OriginPreviousItems.Clear();
    LsWorkingDataService.TranslatedItems.Clear();
  }

  public static ObservableCollection<DataRowModel> FilterData(string filterText)
  {
    var regex = new Regex(
      pattern: filterText,
      options: RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant | RegexOptions.Compiled
    );

    var dataRowModels = LsWorkingDataService.TranslatedItems.Where(
      t =>
      {
        var matchTranslated = regex.Matches(t.Text ?? "");
        var matchOrigin = regex.Matches(t.Origin ?? "");
        var matchPrevious = regex.Matches(t.Previous ?? "");

        return matchTranslated.Count > 0 || matchOrigin.Count > 0 || matchPrevious.Count > 0;
      }
    );

    return new ObservableCollection<DataRowModel>(dataRowModels);
  }

  public static OriginModel? GetCurrentForUid(string? uuid)
  {
    return LsWorkingDataService.OriginCurrentItems.FirstOrDefault(o => o.Uuid == uuid);
  }

  public static OriginModel? GetPreviousForUid(string? uuid)
  {
    return LsWorkingDataService.OriginPreviousItems.FirstOrDefault(o => o.Uuid == uuid);
  }

  public static DataRowModel? GetTranslatedForUid(string? uuid)
  {
    return LsWorkingDataService.TranslatedItems.FirstOrDefault(o => o.Uuid == uuid);
  }

  public static void Load(IEnumerable<XmlFileModel> translatedFiles,
                          IEnumerable<XmlFileModel> currentFiles,
                          IEnumerable<XmlFileModel> previousFiles
  )
  {
    var translatedFilesList = translatedFiles.ToList();
    LsWorkingDataService.TranslatedFiles = translatedFilesList;
    var currentFilesList = currentFiles.ToList();
    LsWorkingDataService.CurrentFiles = currentFilesList;
    var previousFilesList = previousFiles.ToList();
    LsWorkingDataService.PreviousFiles = previousFilesList;

    foreach (var xmlFileModel in currentFilesList)
    {
      LsWorkingDataService.LoadFiles(xmlFileModel: xmlFileModel, type: FileTypes.Current);
    }

    foreach (var xmlFileModel in previousFilesList)
    {
      LsWorkingDataService.LoadFiles(xmlFileModel: xmlFileModel, type: FileTypes.Previous);
    }

    foreach (var xmlFileModel in translatedFilesList)
    {
      LsWorkingDataService.LoadFiles(xmlFileModel: xmlFileModel, type: FileTypes.Translated);
    }

    LsWorkingDataService.AddOriginTexts();
    LsWorkingDataService.AddNewOriginTexts();
  }

  public static void SetTranslatedForUid(string uid, string newText)
  {
    var dataRow = GetTranslatedForUid(uid);

    if (dataRow != null) { dataRow.Text = newText; }
  }

  private static void AddNewOriginTexts()
  {
    foreach (var originModel in LsWorkingDataService.OriginCurrentItems)
    {
      if (LsWorkingDataService.TranslatedItems.FirstOrDefault(t => t.Uuid == originModel.Uuid) != null) { continue; }

      var rowModel = new DataRowModel(
        uuid: originModel.Uuid,
        version: originModel.Version,
        text: originModel.Text,
        mod: originModel.Mod,
        sourceFile: originModel.SourceFile,
        flag: DatSetFlag.NewSet,
        status: TranslationStatus.OriginStatus
      );

      var matchToTranslatedFile = LsWorkingDataService.MatchToTranslatedFile(rowModel);

      if (matchToTranslatedFile != null) { LsWorkingDataService.TranslatedItems.Add(matchToTranslatedFile); }
    }
  }

  private static void AddOriginTexts()
  {
    foreach (var dataRowModel in LsWorkingDataService.TranslatedItems)
    {
      var currentUid = LsWorkingDataService.GetCurrentForUid(dataRowModel.Uuid);
      var previousUid = LsWorkingDataService.GetPreviousForUid(dataRowModel.Uuid);
      dataRowModel.Origin = currentUid?.Text;
      dataRowModel.Previous = previousUid?.Text;
    }
  }

  private static OriginModel BuildOriginModel(XmlElement node, XmlFileModel source) =>
    new(
      mod: source.Mod,
      sourceFile: source,
      text: node.InnerText,
      uuid: node.Attributes["contentuid"]?.InnerText ?? string.Empty,
      version: node.Attributes["version"]?.InnerText ?? string.Empty
    );

  private static void LoadFiles(XmlFileModel xmlFileModel, FileTypes type)
  {
    if (!xmlFileModel.FullPath!.Exists) { return; }

    try
    {
      var doc = new XmlDocument();
      doc.Load(xmlFileModel.FullPath.FullName);
      var nodes = doc.SelectNodes("//content");

      if (nodes == null) { return; }

      foreach (XmlElement node in nodes)
      {
        try
        {
          var newRow = LsWorkingDataService.BuildOriginModel(node: node, source: xmlFileModel);

          if (string.IsNullOrEmpty(newRow.Uuid)) { continue; }

          newRow.Mod = xmlFileModel.Mod;
          newRow.SourceFile = xmlFileModel;

          switch (type)
          {
            case FileTypes.Current:

              LsWorkingDataService.OriginCurrentItems.Add(newRow);

              break;

            case FileTypes.Previous:

              LsWorkingDataService.OriginPreviousItems.Add(newRow);

              break;

            case FileTypes.Translated:

              LsWorkingDataService.TranslatedItems.Add(
                new DataRowModel(
                  text: newRow.Text,
                  flag: DatSetFlag.ExistingSet,
                  mod: xmlFileModel.Mod,
                  sourceFile: xmlFileModel,
                  status: TranslationStatus.TranslatedStatus,
                  uuid: newRow.Uuid,
                  version: newRow.Version
                )
              );

              break;
          }
        }
        catch (Exception ex)
        {
          var message = $"Error during node processing:\n\nError: {ex.Message}";
          Console.WriteLine(message);

          throw new Exception(message);
        }
      }
    }
    catch (Exception e)
    {
      var message
        = $"Error during loading of {type}-XML:\n\nFile: {xmlFileModel.FullPath.FullName}\n\nError:{e.Message}";

      Console.WriteLine(message);

      throw new Exception(message);
    }
  }

  private static DataRowModel? MatchToTranslatedFile(DataRowModel rowModel)
  {
    var translateFile = LsWorkingDataService.TranslatedFiles.FirstOrDefault(t => t.Mod == rowModel.Mod);

    if (translateFile != null) { rowModel.SourceFile = translateFile; }
    else { rowModel = null; }

    return rowModel;
  }

  #endregion

}
