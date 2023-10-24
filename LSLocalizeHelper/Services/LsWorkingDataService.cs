using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

using LSLocalizeHelper.Enums;
using LSLocalizeHelper.Models;

namespace LSLocalizeHelper.Services;

public static class LsWorkingDataService
{

  #region Static Properties

  public static ObservableCollection<OriginModel> OriginCurrentItems { get; set; } = new();

  public static ObservableCollection<OriginModel> OriginPreviousItems { get; set; } = new();

  public static ObservableCollection<DataRowModel> TranslatedItems { get; set; } = new();

  #endregion

  #region Static Methods

  public static void Clear()
  {
    LsWorkingDataService.OriginCurrentItems.Clear();
    LsWorkingDataService.OriginPreviousItems.Clear();
    LsWorkingDataService.TranslatedItems.Clear();
  }

  public static OriginModel? GetCurrentForUid(string uuid)
  {
    var result = LsWorkingDataService.OriginCurrentItems.FirstOrDefault(o => o.Uuid == uuid);

    return result;
  }

  public static OriginModel? GetPreviousForUid(string uuid)
  {
    var result = LsWorkingDataService.OriginPreviousItems.FirstOrDefault(o => o.Uuid == uuid);

    return result;
  }

  public static DataRowModel? GetTranslatedForUid(string uuid)
  {
    var result = LsWorkingDataService.TranslatedItems.FirstOrDefault(o => o.Uuid == uuid);

    return result;
  }

  public static void Load(IEnumerable<XmlFileModel> translatedFiles,
                          IEnumerable<XmlFileModel> currentFiles,
                          IEnumerable<XmlFileModel> previousFiles
  )
  {
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
    if (!xmlFileModel.FullPath!.Exists)
    {
      return;
    }

    try
    {
      var doc = new XmlDocument();
      doc.Load(xmlFileModel.FullPath.FullName);
      var nodes = doc.SelectNodes("//content");

      if (nodes == null)
      {
        return;
      }

      foreach (XmlElement node in nodes)
      {
        try
        {
          var newRow = LsWorkingDataService.BuildOriginModel(node);

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
                  Status = TranslationStatus.OriginStatus,
                  Uuid = newRow.Uuid,
                  Version = newRow.Version,
                  Text = newRow.Text,
                  Mod = xmlFileModel.Mod,
                  Source = xmlFileModel,
                }
              );

              break;
          }
        }
        catch (Exception ex)
        {
          MessageBox.Show($"Error during node processing:\n\nError: {ex.Message}");
        }
      }
    }
    catch (Exception e)
    {
      MessageBox.Show(
        $"Error during loading of {type}-XML:\n\nFile: {xmlFileModel.FullPath.FullName}\n\nError:{e.Message}"
      );
    }
  }

  #endregion

}
