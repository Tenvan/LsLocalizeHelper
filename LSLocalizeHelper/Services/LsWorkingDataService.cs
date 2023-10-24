using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using System.Xml;

using LSLocalizeHelper.Models;

namespace LSLocalizeHelper.Services;

public class LsWorkingDataService
{

  #region Properties

  public ObservableCollection<OriginModel> OriginCurrentItems { get; set; } = new();

  public ObservableCollection<OriginModel> OriginPreviousItems { get; set; } = new();

  public ObservableCollection<DataRowModel> TranslatedItems { get; set; } = new();

  #endregion

  #region Methods

  public void Load(IEnumerable<XmlFileModel> translatedFiles,
                   IEnumerable<XmlFileModel> currentFiles,
                   IEnumerable<XmlFileModel> previousFiles
  )
  {
    foreach (var xmlFileModel in translatedFiles)
    {
      this.LoadFiles(xmlFileModel: xmlFileModel, type: FileTypes.Translated);
    }

    foreach (var xmlFileModel in currentFiles)
    {
      this.LoadFiles(xmlFileModel: xmlFileModel, type: FileTypes.Current);
    }

    foreach (var xmlFileModel in previousFiles)
    {
      this.LoadFiles(xmlFileModel: xmlFileModel, type: FileTypes.Previous);
    }
  }

  private OriginModel BuildOriginModel(XmlElement node) =>
    new()
    {
      Uuid = node.Attributes["contentuid"]?.InnerText,
      Version = node.Attributes["version"]?.InnerText,
      Text = node.InnerText,
    };

  private bool LoadFiles(XmlFileModel xmlFileModel, FileTypes type)
  {
    if (!xmlFileModel.FullPath!.Exists)
    {
      return false;
    }

    try
    {
      var doc = new XmlDocument();
      doc.Load(xmlFileModel.FullPath.FullName);
      var nodes = doc.SelectNodes("//content");

      if (nodes == null)
      {
        return true;
      }

      foreach (XmlElement node in nodes)
      {
        try
        {
          var newRow = this.BuildOriginModel(node);

          switch (type)
          {
            case FileTypes.Current:

              this.OriginCurrentItems.Add(newRow);

              break;

            case FileTypes.Previous:

              this.OriginPreviousItems.Add(newRow);

              break;

            case FileTypes.Translated:

              this.TranslatedItems.Add(
                new DataRowModel()
                {
                  Status = TranslationStatus.origin,
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

          return false;
        }
      }

      return true;
    }
    catch (Exception e)
    {
      MessageBox.Show(
        $"Error during loading of {type}-XML:\n\nFile: {xmlFileModel.FullPath.FullName}\n\nError:{e.Message}"
      );
    }

    return false;
  }

  #endregion

}
