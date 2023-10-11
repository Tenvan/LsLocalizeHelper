using System;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;

namespace Bg3LocaHelper;

public partial class FormMain : Form
{
  #region Static Methods

  private static string GetCellValue(
    DataGridViewRow row,
    GridColumns     column
  )
  {
    return row.Cells[(int)column].Value.ToString();
  }

  private static XmlNode? SelectNode(
    XmlNode? doc,
    object   keySource,
    object   versionSource
  )
  {
    return doc?.SelectSingleNode($"//content[@contentuid='{keySource}' and @version='{versionSource}']");
  }

  private static void UpdateRowStatus(
    DataGridViewRow row,
    string          newStatus
  )
  {
    row.Cells[(int)GridColumns.Status].Value = newStatus;
  }

  private static void UpdateRowText(
    DataGridViewRow row,
    string          newText
  )
  {
    row.Cells[(int)GridColumns.Text].Value = newText;
  }

  #endregion

  #region Constructors

  public FormMain()
  {
    this.InitializeComponent();
    this.LoadSettings();

    if (!string.IsNullOrWhiteSpace(this.TranslatedFile)) { this.LoadData(); }
  }

  #endregion

  #region Properties

  private XmlDocument OriginCurrentDoc { get; set; }

  private string OriginCurrentFile
  {
    get => this.textBoxOriginCurrentFile.Text;
    set => this.textBoxOriginCurrentFile.Text = value;
  }

  private XmlDocument OriginPreviousDoc { get; set; }

  private string OriginPreviousFile
  {
    get => this.textBoxOriginPreviousFile.Text;
    set => this.textBoxOriginPreviousFile.Text = value;
  }

  private XmlDocument TranslatedDoc { get; set; }

  private string TranslatedFile
  {
    get => this.textBoxTranslatedFile.Text;
    set => this.textBoxTranslatedFile.Text = value;
  }

  #endregion

  #region Methods

  private void buttonCopyFromOrigin_Click(
    object    sender,
    EventArgs e
  )
  {
    Clipboard.SetText(this.textBoxCurrentOriginText.Text);
  }

  private void buttonCopyFromPrevious_Click(
    object    sender,
    EventArgs e
  )
  {
    Clipboard.SetText(this.textBoxPreviousOriginText.Text);
  }

  private void buttonFileOriginCurrent_Click(
    object    sender,
    EventArgs e
  )
  {
    var result = this.openFileDialog.ShowDialog();

    if (result == DialogResult.OK) { this.textBoxOriginCurrentFile.Text = this.openFileDialog.FileName; }
  }

  private void buttonFileOriginPrevious_Click(
    object    sender,
    EventArgs e
  )
  {
    var result = this.openFileDialog.ShowDialog();

    if (result == DialogResult.OK) { this.textBoxOriginPreviousFile.Text = this.openFileDialog.FileName; }
  }

  private void buttonFileTranslated_Click(
    object    sender,
    EventArgs e
  )
  {
    var result = this.openFileDialog.ShowDialog();

    if (result == DialogResult.OK) { this.textBoxTranslatedFile.Text = this.openFileDialog.FileName; }
  }

  private void buttonLoad_Click(
    object    sender,
    EventArgs e
  )
  {
    this.LoadData();
  }

  private void buttonPasteToTranslated_Click(
    object    sender,
    EventArgs e
  )
  {
    var newText = Clipboard.GetText();
    this.textBoxTranslatedText.Text = newText;

    var row       = this.dataGridViewSource.CurrentRow;
    var keySource = row!.Cells[(int)GridColumns.Uuid].Value;
    row!.Cells[(int)GridColumns.Text].Value = newText;

    var sourceNode = this.TranslatedDoc.SelectSingleNode($"//content[@contentuid='{keySource}']");

    if (sourceNode != null)
      sourceNode.InnerText = newText;
  }

  private void buttonSave_Click(
    object    sender,
    EventArgs e
  )
  {
    this.SaveData();
    this.LoadData();
  }

  private void dataGridViewSource_RowEnter(
    object                    sender,
    DataGridViewCellEventArgs e
  )
  {
    var dataIndex = e.RowIndex;
    var row       = (sender as DataGridView)!.Rows[dataIndex];

    var keySource     = row.Cells[(int)GridColumns.Uuid].Value.ToString();
    var versionSource = row.Cells[(int)GridColumns.Version].Value.ToString();
    var textSource    = row.Cells[(int)GridColumns.Text].Value.ToString();

    var translatedNode = this.TranslatedDoc.SelectSingleNode($"//content[@contentuid='{keySource}' and @version='{versionSource}']");

    if (translatedNode != null)
    {
      this.textBoxTranslatedText.Text = textSource;
      Clipboard.SetText(textSource);
    }
    else { this.textBoxTranslatedText.Text = ""; }

    var currentOriginNode = this.OriginCurrentDoc.SelectSingleNode($"//content[@contentuid='{keySource}' and @version='{versionSource}']");
    this.textBoxCurrentOriginText.Text = currentOriginNode != null ? currentOriginNode.InnerText : "";

    var previousOriginNode = this.OriginPreviousDoc.SelectSingleNode($"//content[@contentuid='{keySource}' and @version='{versionSource}']");
    this.textBoxPreviousOriginText.Text = previousOriginNode != null ? previousOriginNode.InnerText : "";
  }

  private void dataGridViewSource_RowPrePaint(
    object                           sender,
    DataGridViewRowPrePaintEventArgs e
  )
  {
    var row                   = ((sender as DataGridView)!).Rows[e.RowIndex];
    var dataGridViewCellStyle = row.DefaultCellStyle;

    var status = row!.Cells[(int)GridColumns.Status].Value;

    switch (status)
    {
      case "translated":

        dataGridViewCellStyle.ForeColor = Color.Green;
        dataGridViewCellStyle.BackColor = Color.White;

        break;

      case "origin":

        dataGridViewCellStyle.ForeColor = Color.DarkRed;
        dataGridViewCellStyle.BackColor = Color.White;

        break;

      case "changed":

        dataGridViewCellStyle.ForeColor = Color.OrangeRed;
        dataGridViewCellStyle.BackColor = Color.White;

        break;

      case "new":

        dataGridViewCellStyle.ForeColor = Color.White;
        dataGridViewCellStyle.BackColor = Color.Blue;

        break;

      case "deleted":

        dataGridViewCellStyle.ForeColor = Color.White;
        dataGridViewCellStyle.BackColor = Color.DarkRed;

        break;
    }
  }

  private void FormMain_FormClosed(
    object              sender,
    FormClosedEventArgs e
  )
  {
    this.SaveSettings();
  }

  private void textBoxTranslatedText_Enter(
    object    sender,
    EventArgs e
  )
  {
    if (this.textBoxTranslatedText.Text == "")
    {
      var row = this.dataGridViewSource.CurrentRow;
      if (row == null) return;

      var keySource     = FormMain.GetCellValue(row, GridColumns.Uuid);
      var versionSource = FormMain.GetCellValue(row, GridColumns.Version);

      var currentNode   = FormMain.SelectNode(this.OriginCurrentDoc, keySource, versionSource);
      this.textBoxTranslatedText.Text = currentNode?.InnerText;
    }
  }

  private void textBoxTranslatedText_Leave(
    object    sender,
    EventArgs e
  )
  {
    var row = this.dataGridViewSource.CurrentRow;

    if (row == null) return;

    var keySource     = FormMain.GetCellValue(row, GridColumns.Uuid);
    var versionSource = FormMain.GetCellValue(row, GridColumns.Version);

    var translatedNode = FormMain.SelectNode(this.TranslatedDoc, keySource, versionSource);
    var currentNode    = FormMain.SelectNode(this.OriginCurrentDoc, keySource, versionSource);
    var previousNode   = FormMain.SelectNode(this.OriginPreviousDoc, keySource, versionSource);

    var newStatus = FormMain.CalculateStatus(translatedNode, currentNode, previousNode);

    var newText = this.textBoxTranslatedText.Text;

    FormMain.UpdateRowText(row, newText);
    FormMain.UpdateRowStatus(row, newStatus);

    if (translatedNode != null)
      translatedNode.InnerText = newText;
    else
    {
      var newNode = FormMain.AddNode(this.TranslatedDoc, keySource, versionSource, newText);
      var status  = FormMain.CalculateStatus(newNode, currentNode, previousNode);
      FormMain.UpdateRowStatus(row, status);
    }
  }

  #endregion
}