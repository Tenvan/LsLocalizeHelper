using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Xml;

using Bg3LocaHelper.Properties;

namespace Bg3LocaHelper;

partial class FormMain
{
  #region Static Methods

  private static XmlElement AddNode(
    XmlDocument doc,
    string      key,
    string      version,
    string?     text
  )
  {
    var newNode = doc.CreateElement("content");

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

  private static void DeleteNode(
    XmlDocument? doc,
    string   key,
    string   version
  )
  {
    var deletedNode = FormMain.SelectNode(doc, key, version);
    if (deletedNode != null) doc?.DocumentElement?.RemoveChild(deletedNode);
  }

  #endregion

  #region Methods

  private void LoadData()
  {
    try
    {
      var dataSet = new DataSet();
      var table   = new DataTable();

      table.Columns.Add("Status", typeof(string));
      table.Columns.Add("UUID", typeof(string));
      table.Columns.Add("Version", typeof(int));
      table.Columns.Add("Text", typeof(string));

      if (File.Exists(this.TranslatedFile))
      {
        this.TranslatedDoc = new XmlDocument();
        this.TranslatedDoc.Load(this.TranslatedFile);
      }

      if (File.Exists(this.OriginPreviousFile))
      {
        this.OriginCurrentDoc = new XmlDocument();
        this.OriginCurrentDoc.Load(this.OriginCurrentFile);
      }

      if (File.Exists(this.OriginCurrentFile))
      {
        this.OriginPreviousDoc = new XmlDocument();
        this.OriginPreviousDoc.Load(this.OriginPreviousFile);
      }

      var translatedNodes = this.TranslatedDoc?.SelectNodes($"//content");
      var currentNodes    = this.OriginCurrentDoc?.SelectNodes($"//content");

      // Check for translated and deleted Nodes
      if (translatedNodes != null)
        foreach (XmlElement translatedNode in translatedNodes)
        {
          var uid     = translatedNode.Attributes["contentuid"].InnerText;
          var version = translatedNode.Attributes["version"].InnerText;

          var currentNode  = this.OriginCurrentDoc?.SelectSingleNode($"//content[@contentuid='{uid}' and @version='{version}']");
          var previousNode = this.OriginPreviousDoc?.SelectSingleNode($"//content[@contentuid='{uid}' and @version='{version}']");

          var status = FormMain.CalculateStatus(translatedNode, currentNode, previousNode);

          if (status == "deleted") { FormMain.DeleteNode(this.TranslatedDoc, uid, version); }

          object[] row = { status, uid, version, translatedNode.InnerText };
          table.Rows.Add(row);

        }

      // Check for new Nodes
      if (currentNodes != null)
        foreach (XmlElement currentNode in currentNodes)
        {
          var uid     = currentNode.Attributes["contentuid"].InnerText;
          var version = currentNode.Attributes["version"].InnerText;
          var text    = currentNode.InnerText;

          var translatedNode = this.TranslatedDoc?.SelectSingleNode($"//content[@contentuid='{uid}' and @version='{version}']");
          var previousNode   = this.OriginPreviousDoc?.SelectSingleNode($"//content[@contentuid='{uid}' and @version='{version}']");

          var status = FormMain.CalculateStatus(translatedNode, currentNode, previousNode);

          if (status == "new")
          {
            object[] row = { status, uid, version, text };
            table.Rows.Add(row);
          }
        }

      dataSet.Tables.Add(table);

      this.dataGridViewSource.DataSource          = table;
      this.dataGridViewSource.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
    }
    catch (Exception e) { Debug.Write(e.Message); }
  }

  private void LoadSettings()
  {
    try
    {
      this.OriginCurrentFile  = Settings.Default.pathOriginCurrent;
      this.OriginPreviousFile = Settings.Default.pathOriginPrevious;
      this.TranslatedFile     = Settings.Default.pathTranslated;
      this.Size               = Settings.Default.windowSize;
      this.Location           = Settings.Default.windowPos;
    }
    finally { }
  }

  private void SaveData() { this.TranslatedDoc.Save(this.TranslatedFile); }

  private void SaveSettings()
  {
    Settings.Default.pathOriginCurrent  = this.OriginCurrentFile;
    Settings.Default.pathOriginPrevious = this.OriginPreviousFile;
    Settings.Default.pathTranslated     = this.TranslatedFile;
    Settings.Default.windowPos          = this.Location;
    Settings.Default.windowSize         = this.Size;
    Settings.Default.Save();
  }

  #endregion
}

internal enum GridColumns
{
  Status = 0,

  Uuid = 1,

  Version = 2,

  Text = 3
}