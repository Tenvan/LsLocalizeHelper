using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
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
      filterText,
      RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant | RegexOptions.Compiled
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
    LsWorkingDataService.TranslatedFiles = translatedFiles;
    LsWorkingDataService.CurrentFiles = currentFiles;
    LsWorkingDataService.PreviousFiles = previousFiles;

    foreach (var xmlFileModel in translatedFiles)
    {
      LsWorkingDataService.LoadFiles(xmlFileModel: xmlFileModel, type: FileTypes.Translated);
    }

    foreach (var xmlFileModel in currentFiles)
    {
      LsWorkingDataService.LoadFiles(xmlFileModel: xmlFileModel, type: FileTypes.Current);
    }

    foreach (var xmlFileModel in previousFiles)
    {
      LsWorkingDataService.LoadFiles(xmlFileModel: xmlFileModel, type: FileTypes.Previous);
    }

    LsWorkingDataService.AddOriginTexts();
    LsWorkingDataService.AddNewOriginTexts();
  }

  public static void SetTranslatedForUid(string uid, string newText)
  {
    var dataRow = GetTranslatedForUid(uid);

    if (dataRow != null)
    {
      dataRow.Text = newText;

    }
  }

  private static void AddNewOriginTexts()
  {
    foreach (var originModel in LsWorkingDataService.OriginCurrentItems)
    {
      if (LsWorkingDataService.TranslatedItems.FirstOrDefault(t => t.Uuid == originModel.Uuid) != null) { continue; }

      var rowModel = new DataRowModel()
      {
        Uuid = originModel.Uuid,
        Version = originModel.Version,
        Text = originModel.Text,
        Mod = originModel.Mod,
        SourceFile = originModel.SourceFile,
        Flag = DatSetFlag.NewSet,
      };

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

  private static OriginModel BuildOriginModel(XmlElement node) =>
    new()
    {
      Uuid = node.Attributes["contentuid"]?.InnerText,
      Version = node.Attributes["version"]?.InnerText,
      Text = node.InnerText,
    };

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
          var newRow = LsWorkingDataService.BuildOriginModel(node);
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
                new DataRowModel()
                {
                  Uuid = newRow.Uuid,
                  Version = newRow.Version,
                  Text = newRow.Text,
                  Mod = xmlFileModel.Mod,
                  SourceFile = xmlFileModel,
                  Flag = DatSetFlag.ExistingSet,
                }
              );

              break;
          }
        }
        catch (Exception ex) { MessageBox.Show($"Error during node processing:\n\nError: {ex.Message}"); }
      }
    }
    catch (Exception e)
    {
      MessageBox.Show(
        $"Error during loading of {type}-XML:\n\nFile: {xmlFileModel.FullPath.FullName}\n\nError:{e.Message}"
      );
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
