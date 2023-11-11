using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Xml;

using DynamicData;

using LsLocalizeHelperLib.Enums;
using LsLocalizeHelperLib.Helper;
using LsLocalizeHelperLib.Models;

namespace LsLocalizeHelperLib.Services;

public static class LsWorkingDataService
{

  #region Static Properties

  public static ObservableCollection<XmlFileModel> CurrentFiles { get; set; } = new();

  public static ObservableCollection<OriginModel> OriginCurrentItems { get; set; } = new();

  public static ObservableCollection<OriginModel> OriginPreviousItems { get; set; } = new();

  public static ObservableCollection<XmlFileModel> PreviousFiles { get; set; } = new();

  public static ObservableCollection<XmlFileModel> TranslatedFiles { get; set; } = new();

  public static ObservableCollection<DataRowModel?> TranslatedItems { get; set; } = new();

  #endregion

  #region Static Methods

  public static void Clear()
  {
    LsWorkingDataService.CurrentFiles.Clear();
    LsWorkingDataService.OriginCurrentItems.Clear();
    LsWorkingDataService.OriginPreviousItems.Clear();
    LsWorkingDataService.PreviousFiles.Clear();
    LsWorkingDataService.TranslatedFiles.Clear();
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
    LsWorkingDataService.TranslatedFiles.Clear();
    var xmlFileModels = translatedFiles.ToList();
    LsWorkingDataService.TranslatedFiles.AddRange(xmlFileModels);
    
    LsWorkingDataService.CurrentFiles.Clear();
    var fileModels = currentFiles.ToList();
    LsWorkingDataService.CurrentFiles.AddRange(fileModels);
    
    LsWorkingDataService.PreviousFiles.Clear();
    var enumerable = previousFiles.ToList();
    LsWorkingDataService.PreviousFiles.AddRange(enumerable);

    foreach (var xmlFileModel in fileModels)
    {
      LsWorkingDataService.LoadFiles(xmlFileModel: xmlFileModel, type: FileTypes.Current);
    }

    foreach (var xmlFileModel in enumerable)
    {
      LsWorkingDataService.LoadFiles(xmlFileModel: xmlFileModel, type: FileTypes.Previous);
    }

    foreach (var xmlFileModel in xmlFileModels)
    {
      LsWorkingDataService.LoadFiles(xmlFileModel: xmlFileModel, type: FileTypes.Translated);
    }

    LsWorkingDataService.AddOriginTexts();
    LsWorkingDataService.AddNewOriginTexts();
    LsWorkingDataService.SearchDuplicates();
    LsWorkingDataService.ValidateLsTags();
  }

  public static void RecalculateStatus(DataRowModel? dataRowModel)
  {
    var currentUid = LsWorkingDataService.GetCurrentForUid(dataRowModel!.Uuid);
    var previousUid = LsWorkingDataService.GetPreviousForUid(dataRowModel.Uuid);

    if (currentUid == null)
    {
      dataRowModel.Status = TranslationStatus.Deleted;

      return;
    }

    dataRowModel.Origin = currentUid?.Text;
    dataRowModel.Previous = previousUid?.Text;

    dataRowModel.Status = dataRowModel.Text.Equals(dataRowModel.Origin)
                            ? TranslationStatus.Origin
                            : TranslationStatus.Translated;

    dataRowModel.OriginStatus
      = dataRowModel.Origin.Equals(dataRowModel.Previous) || string.IsNullOrEmpty(dataRowModel.Previous)
          ? TranslationStatus.Origin
          : TranslationStatus.Updated;
  }

  public static bool SetTranslatedForUid(string uid, string newText)
  {
    var dataRow = LsWorkingDataService.GetTranslatedForUid(uid);

    if (dataRow == null) { return false; }

    dataRow.Text = newText;

    LsWorkingDataService.RecalculateStatus(dataRow);
    LsWorkingDataService.ValidateLsTagForRow(dataRow);

    return true;
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
        status: TranslationStatus.NewAndOrigin
      )
      {
        Origin = originModel.Text,
      };

      var matchToTranslatedFile = LsWorkingDataService.MatchToTranslatedFile(rowModel);

      if (matchToTranslatedFile != null) { LsWorkingDataService.TranslatedItems.Add(matchToTranslatedFile); }
    }
  }

  private static void AddOriginTexts()
  {
    foreach (var dataRowModel in LsWorkingDataService.TranslatedItems)
    {
      LsWorkingDataService.RecalculateStatus(dataRowModel);
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
                  status: TranslationStatus.Origin,
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
    catch (Exception ex)
    {
      var message
        = $"Error during loading of {type}-XML:\n\nFile: {xmlFileModel.FullPath.FullName}\n\nError:{ex.Message}";

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

  private static void SearchDuplicates()
  {
    var duplicates = LsWorkingDataService.TranslatedItems.GroupBy(x => (string)x.Uuid)
                                         .Where(g => g.Count() > 1)
                                         .Select(g => g.Key)
                                         .ToList();

    foreach (var duplicate in duplicates)
    {
      var items = LsWorkingDataService.TranslatedItems.Where(o => o.Uuid == duplicate);

      foreach (var dataRowModel in items) { dataRowModel!.Flag = DatSetFlag.DuplicateSet; }
    }
  }

  private static void ValidateLsTags()
  {
    foreach (var dataRowModel in LsWorkingDataService.TranslatedItems) { LsWorkingDataService.ValidateLsTagForRow(dataRowModel); }
  }

  private static void ValidateLsTagForRow(DataRowModel? dataRowModel)
  {
    var isNotDuplicateFlag = dataRowModel?.Flag != DatSetFlag.DuplicateSet;
    var isHtmlValid = dataRowModel?.Text.IsValidHtml() ?? true;

    if (isNotDuplicateFlag)
    {
      dataRowModel!.Flag = isHtmlValid ? DatSetFlag.ExistingSet : DatSetFlag.LsTagError;
    }
  }
  #endregion

}
