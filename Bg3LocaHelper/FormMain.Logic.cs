using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

using Bg3LocaHelper.Properties;

using LSLib.LS;

using DirectoryInfo = Alphaleonis.Win32.Filesystem.DirectoryInfo;
using File = Alphaleonis.Win32.Filesystem.File;
using Path = Alphaleonis.Win32.Filesystem.Path;

namespace Bg3LocaHelper;

partial class FormMain
{
  #region Static Methods

  private static XmlElement AddNode(
    XmlDocument? doc,
    string       key,
    string       version,
    string?      text
  )
  {
    var newNode = doc?.CreateElement("content");
    newNode.SetAttribute("contentuid", key);
    newNode.SetAttribute("version", version);
    newNode.InnerText = text;
    var nodes = doc.SelectSingleNode($"//contentList");
    nodes!.AppendChild(newNode);

    return newNode;
  }

  private static string CalculateStatus(
    XmlNode? translatedNode,
    XmlNode? currentNode,
    XmlNode? previousNode
  )
  {
    if (translatedNode == null
        && currentNode != null) { return "new"; }

    if (translatedNode != null
        && currentNode == null) { return "deleted"; }

    if (translatedNode?.InnerText == currentNode?.InnerText) { return currentNode?.InnerText != previousNode?.InnerText && previousNode != null ? "changed" : "origin"; }

    return "translated";
  }

  private static DataTable CreateTable()
  {
    var table = new DataTable();
    table.Columns.Add("Status", typeof(string));
    table.Columns.Add("UUID", typeof(string));
    table.Columns.Add("Version", typeof(int));
    table.Columns.Add("Text", typeof(string));
    table.Columns.Add("Origin", typeof(string));

    return table;
  }

  private static void DeleteNode(
    XmlDocument? doc,
    string       key,
    string       version
  )
  {
    var deletedNode = FormMain.SelectNode(doc, key, version);
    if (deletedNode != null) doc?.DocumentElement?.RemoveChild(deletedNode);
  }

  #endregion

  #region Fields

  private DirectoryInfo? modFolder;

  private DirectoryInfo? modWorkFolder;

  #endregion

  #region Methods

  private void FilterData()
  {
    try
    {
      var searchText = this.textBoxFilter.Text.ToLower();

      if (string.IsNullOrWhiteSpace(searchText))
      {
        this.dataGridViewSource.DataSource = this.DataTable;
        this.RecalcRowsAndColumnSizesHeights();

        return;
      }

      var filteredData = FormMain.CreateTable();

      foreach (DataRow obj in this.DataTable.Rows)
      {
        var rowText    = obj[(int)GridColumns.Text].ToString().ToLower();
        var rowKey     = obj[(int)GridColumns.Uuid].ToString();
        var rowVersion = obj[(int)GridColumns.Version].ToString();
        var rowUid     = rowKey.ToLower();
        var rowOrigin  = obj[(int)GridColumns.Origin].ToString().ToLower();

        if (string.IsNullOrEmpty(searchText)
            || rowText.Contains(searchText)
            || rowUid.Contains(searchText)
            || rowOrigin.Contains(searchText)
           )
        {
          var xpath = rowVersion == null ? $"//content[@contentuid='{rowKey}']" : $"//content[@contentuid='{rowKey}' and @version='{rowVersion}']";

          var translatedNode =
            this.TranslatedDoc?.SelectSingleNode(xpath);

          var currentNode =
            this.OriginCurrentDoc?.SelectSingleNode(xpath);

          var previousNode =
            this.OriginPreviousDoc?.SelectSingleNode(xpath);

          var status = CalculateStatus(translatedNode, currentNode, previousNode);

          object[] row =
          {
            status,
            rowKey,
            rowVersion,
            obj[(int)GridColumns.Text].ToString(),
            obj[(int)GridColumns.Origin].ToString()
          };

          filteredData.Rows.Add(row);
        }
      }

      this.dataGridViewSource.DataSource = filteredData;
      this.RecalcRowsAndColumnSizesHeights();
    }
    catch (Exception e) { Debug.Write(e.Message); }
  }

  private void LoadData()
  {
    try
    {
      this.dataGridViewSource.DataSource = null;
      this.TranslatedDoc                 = null;
      this.OriginCurrentDoc              = null;
      this.OriginPreviousDoc             = null;
      var table = FormMain.CreateTable();

      if (File.Exists(this.TranslatedFileFullName))
      {
        try
        {
          this.TranslatedDoc = new XmlDocument();
          this.TranslatedDoc.Load(this.TranslatedFileFullName);
        }
        catch (Exception e)
        {
          throw new Exception(
                              $"Error on Loading Translated-XML:\n\nFile: {this.TranslatedFileFullName}\n\nError:{e.Message}"
                             );
        }
      }

      if (File.Exists(this.OriginCurrentFileFullName))
      {
        try
        {
          this.OriginCurrentDoc = new XmlDocument();
          this.OriginCurrentDoc.Load(this.OriginCurrentFileFullName);
        }
        catch (Exception e)
        {
          throw new Exception(
                              $"Error on Loading Current-XML:\n\nFile: {this.OriginCurrentFileFullName}\n\nError:{e.Message}"
                             );
        }
      }

      if (File.Exists(this.OriginPreviousFileFullName))
      {
        try
        {
          this.OriginPreviousDoc = new XmlDocument();
          this.OriginPreviousDoc.Load(this.OriginPreviousFileFullName);
        }
        catch (Exception e)
        {
          throw new Exception(
                              $"Error on Loading Previous-XML:\n\nFile: {this.OriginPreviousFileFullName}\n\nError:{e.Message}"
                             );
        }
      }

      var translatedNodes = this.TranslatedDoc?.SelectNodes($"//content");
      var currentNodes    = this.OriginCurrentDoc?.SelectNodes($"//content");

      // Check for translated and deleted Nodes
      if (translatedNodes != null)
        foreach (XmlElement translatedNode in translatedNodes)
        {
          var uid     = translatedNode.Attributes["contentuid"].InnerText;
          var version = translatedNode.Attributes["version"]?.InnerText;
          var xpath   = version == null ? $"//content[@contentuid='{uid}']" : $"//content[@contentuid='{uid}' and @version='{version}']";

          var currentNode =
            this.OriginCurrentDoc?.SelectSingleNode(xpath);

          var previousNode =
            this.OriginPreviousDoc?.SelectSingleNode(xpath);

          var status = FormMain.CalculateStatus(translatedNode, currentNode, previousNode);

          if (status == "deleted") { FormMain.DeleteNode(this.TranslatedDoc, uid, version); }

          object[] row = { status, uid, version, translatedNode.InnerText, currentNode?.InnerText ?? "" };
          table.Rows.Add(row);
        }

      // Check for new Nodes
      if (currentNodes != null)
        foreach (XmlElement currentNode in currentNodes)
        {
          var uid     = currentNode.Attributes["contentuid"].InnerText;
          var version = currentNode.Attributes["version"]?.InnerText;
          var text    = currentNode.InnerText;
          var xpath   = version == null ? $"//content[@contentuid='{uid}']" : $"//content[@contentuid='{uid}' and @version='{version}']";

          var translatedNode =
            this.TranslatedDoc?.SelectSingleNode(xpath);

          var previousNode =
            this.OriginPreviousDoc?.SelectSingleNode(xpath);

          var status = FormMain.CalculateStatus(translatedNode, currentNode, previousNode);

          if (status == "new")
          {
            object[] row = { status, uid, version, text };
            table.Rows.Add(row);
          }
        }

      this.DataTable                     = table;
      this.dataGridViewSource.DataSource = table;
      this.RecalcRowsAndColumnSizesHeights();
    }
    catch (Exception e) { MessageBox.Show(e.Message); }
  }

  private void LoadMods()
  {
    this.comboBoxMods.Items.Clear();

    if (string.IsNullOrWhiteSpace(Settings.Default.pathMods)) return;

    var dirInfo = new DirectoryInfo(Settings.Default.pathMods);

    if (!dirInfo.Exists) return;

    //GeneralHelper.WriteToConsole(Properties.Resources.DirectoryName, dirName);
    var metaFiles = dirInfo.GetFiles("meta.lsx", SearchOption.AllDirectories);

    foreach (var metaFile in metaFiles)
    {
      var modName = metaFile.Directory?.Parent?.Parent?.Parent.Name;
      if (modName != null) this.comboBoxMods.Items.Add(modName);
    }
  }

  private void LoadSettings()
  {
    try
    {
      this.comboBoxMods.Text  = Settings.Default.lastMod;
      this.OriginCurrentFile  = Settings.Default.pathOriginCurrent;
      this.OriginPreviousFile = Settings.Default.pathOriginPrevious;
      this.TranslatedFile     = Settings.Default.pathTranslated;
      this.Size               = Settings.Default.windowSize;
      this.Location           = Settings.Default.windowPos;
    }
    finally { }
  }

  private void LoadXmlFileNames2ComboBoxes(
    string modName
  )
  {
    this.comboBoxOriginPreviousFile.Items.Clear();
    this.comboBoxOriginCurrentFile.Items.Clear();
    this.comboBoxTranslatedFile.Items.Clear();
    var dirInfo   = new DirectoryInfo(Path.Combine(Settings.Default.pathMods, modName));
    var localsDir = dirInfo.GetDirectories("Localization", SearchOption.AllDirectories).FirstOrDefault();
    this.modWorkFolder = localsDir?.Parent;
    var metaFiles = dirInfo.GetFiles("*.xml", SearchOption.AllDirectories);

    foreach (var metaFile in metaFiles)
    {
      var fullName  = metaFile.FullName;
      var shortName = Path.GetRelativePath(this.modWorkFolder?.FullName, metaFile.FullName);
      this.comboBoxOriginPreviousFile.Items.Add(shortName);
      this.comboBoxOriginCurrentFile.Items.Add(shortName);
      this.comboBoxTranslatedFile.Items.Add(shortName);
    }
  }

  private void PackingMod()
  {
    this.SaveLoca();
    var packer = new PackageEngine(this.modWorkFolder.FullName, this.CurrentModName);
    packer.BuildPackage();
    MessageBox.Show("Mod package zip created successfully.");
  }

  private void RecalcRowsAndColumnSizesHeights()
  {
    // Zeilenhöhen berechnen
    foreach (DataGridViewRow row in this.dataGridViewSource.Rows)
    {
      foreach (DataGridViewCell cell in row.Cells)
      {
        var formattedValue = cell.FormattedValue?.ToString();

        if (string.IsNullOrEmpty(formattedValue)) continue;

        var size  = TextRenderer.MeasureText(formattedValue, this.dataGridViewSource.DefaultCellStyle.Font);
        var lines = formattedValue?.Split('\n').Length ?? 1;
        row.Height = Math.Max(row.Height, lines * this.dataGridViewSource.DefaultCellStyle.Font.Height + 2);
      }
    }

    // Spaltenbreiten berechnen
    this.dataGridViewSource.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

    foreach (DataGridViewColumn col in this.dataGridViewSource.Columns)
    {
      col.Width = TextRenderer.MeasureText(col.Name, this.dataGridViewSource.DefaultCellStyle.Font).Width + 5;

      foreach (DataGridViewRow row in this.dataGridViewSource.Rows)
      {
        var formattedValue = row.Cells[col.Index].FormattedValue?.ToString();

        if (string.IsNullOrEmpty(formattedValue)) continue;

        var size = TextRenderer.MeasureText(formattedValue, this.dataGridViewSource.DefaultCellStyle.Font);
        col.Width = Math.Max(col.Width, size.Width + 5);
      }
    }

    this.dataGridViewSource.Refresh();
  }

  private void SaveData()
  {
    this.TranslatedDoc?.Save(this.TranslatedFileFullName);
    this.SaveLoca();
    this.LoadData();
    MessageBox.Show("Data saved successfully.");
  }

  private void SaveLoca()
  {
    var resource       = LocaUtils.Load(this.TranslatedFileFullName);
    var locaOutputPath = Path.ChangeExtension(this.TranslatedFileFullName, "loca");
    var format         = LocaUtils.ExtensionToFileFormat(locaOutputPath);
    LocaUtils.Save(resource, locaOutputPath, format);
  }

  private void UpdateRowStatus()
  {
    var row = this.dataGridViewSource.CurrentRow;

    if (row == null) return;

    var keySource      = FormMain.GetCellValue(row, GridColumns.Uuid);
    var versionSource  = FormMain.GetCellValue(row, GridColumns.Version);
    var translatedNode = FormMain.SelectNode(this.TranslatedDoc, keySource, versionSource);
    var currentNode    = FormMain.SelectNode(this.OriginCurrentDoc, keySource, versionSource);
    var previousNode   = FormMain.SelectNode(this.OriginPreviousDoc, keySource, versionSource);
    var newStatus      = FormMain.CalculateStatus(translatedNode, currentNode, previousNode);
    FormMain.UpdateRowStatus(row, newStatus);
  }

  #endregion
}

internal enum GridColumns
{
  Status = 0,

  Uuid = 1,

  Version = 2,

  Text = 3,

  Origin = 4,
}