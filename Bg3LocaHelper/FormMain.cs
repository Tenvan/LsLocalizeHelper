using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;

using Alphaleonis.Win32.Filesystem;

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

  #endregion

  #region Constructors

  public FormMain()
  {
    this.InitializeComponent();
    this.LoadSettings();
  }

  #endregion

  #region Properties

  private bool AutoClipbaord => this.checkBoxAutoClipboard.Checked;

  /// <summary>
  /// Full Data Store
  /// Contains full loaded Data without Filtering, filled bei <see cref="LoadData"/>.
  /// </summary>
  private DataTable DataTable { get; set; }

  /// <summary>
  /// Full Xml-Data of current origin localisation file, filled bei <see cref="LoadData"/>.
  /// </summary>
  private XmlDocument? OriginCurrentDoc { get; set; }

  private string OriginCurrentFile
  {
    get => this.textBoxOriginCurrentFile.Text;
    set => this.textBoxOriginCurrentFile.Text = value;
  }

  /// <summary>
  /// Full Xml-Data of previous origin localisation file, filled bei <see cref="LoadData"/>.
  /// </summary>
  private XmlDocument? OriginPreviousDoc { get; set; }

  private string OriginPreviousFile
  {
    get => this.textBoxOriginPreviousFile.Text;
    set => this.textBoxOriginPreviousFile.Text = value;
  }

  /// <summary>
  /// Full Xml-Data of current own translated localisation file, filled bei <see cref="LoadData"/>.
  /// </summary>
  private XmlDocument? TranslatedDoc { get; set; }

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

    this.SaveSettings();
  }

  private void buttonFileOriginPrevious_Click(
    object    sender,
    EventArgs e
  )
  {
    var result = this.openFileDialog.ShowDialog();

    if (result == DialogResult.OK) { this.textBoxOriginPreviousFile.Text = this.openFileDialog.FileName; }

    this.SaveSettings();
  }

  private void buttonFileTranslated_Click(
    object    sender,
    EventArgs e
  )
  {
    var result = this.openFileDialog.ShowDialog();

    if (result == DialogResult.OK) { this.textBoxTranslatedFile.Text = this.openFileDialog.FileName; }

    this.SaveSettings();
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
    var sourceNode = this.TranslatedDoc?.SelectSingleNode($"//content[@contentuid='{keySource}']");

    if (sourceNode != null)
      sourceNode.InnerText = newText;

    this.UpdateDataText(row, newText);
    this.UpdateRowStatus();
    this.RecalcRowsAndColumnSizesHeights();
  }

  private void buttonSave_Click(
    object    sender,
    EventArgs e
  )
  {
    this.SaveData();
    this.SaveLoca();
    this.LoadData();
  }

  private void dataGridViewSource_CellPainting(
    object                            sender,
    DataGridViewCellPaintingEventArgs e
  )
  {
    if (e is
        not
        {
          RowIndex   : >= 0,
          ColumnIndex: (int)GridColumns.Text or (int)GridColumns.Uuid or (int)GridColumns.Origin
        }) return;

    if (e.CellStyle?.Font == null
        || e.FormattedValue == null) return;

    e.Handled = true;
    e.PaintBackground(e.CellBounds, true);
    var cellText = e.FormattedValue.ToString();
    var isSelected = (e.State & DataGridViewElementStates.Selected) != 0;
    var fontBrush = isSelected ? new SolidBrush(e.CellStyle.SelectionForeColor) : new SolidBrush(e.CellStyle.ForeColor);
    var pointF = new PointF(e.CellBounds.X + 2, e.CellBounds.Y + 2);
    e.Graphics.DrawString(cellText, e.CellStyle.Font, fontBrush, pointF);
  }

  private void dataGridViewSource_RowEnter(
    object                    sender,
    DataGridViewCellEventArgs e
  )
  {
    var dataIndex     = e.RowIndex;
    var row           = (sender as DataGridView)!.Rows[dataIndex];
    var keySource     = row.Cells[(int)GridColumns.Uuid].Value.ToString();
    var versionSource = row.Cells[(int)GridColumns.Version].Value.ToString();
    var textSource    = row.Cells[(int)GridColumns.Text].Value.ToString();

    var translatedNode =
      this.TranslatedDoc?.SelectSingleNode($"//content[@contentuid='{keySource}' and @version='{versionSource}']");

    if (translatedNode != null)
    {
      this.textBoxTranslatedText.Text = textSource;
      if (this.AutoClipbaord) Clipboard.SetText(textSource);
    }
    else { this.textBoxTranslatedText.Text = ""; }

    var currentOriginNode =
      this.OriginCurrentDoc?.SelectSingleNode($"//content[@contentuid='{keySource}' and @version='{versionSource}']");

    this.textBoxCurrentOriginText.Text = currentOriginNode != null ? currentOriginNode.InnerText : "";

    var previousOriginNode =
      this.OriginPreviousDoc?.SelectSingleNode($"//content[@contentuid='{keySource}' and @version='{versionSource}']");

    this.textBoxPreviousOriginText.Text = previousOriginNode != null ? previousOriginNode.InnerText : "";
  }

  private void dataGridViewSource_RowPrePaint(
    object                           sender,
    DataGridViewRowPrePaintEventArgs e
  )
  {
    var row                   = ((sender as DataGridView)!).Rows[e.RowIndex];
    var dataGridViewCellStyle = row.DefaultCellStyle;
    var status                = row!.Cells[(int)GridColumns.Status].Value;

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

  private void textBoxFile_Validated(
    object    sender,
    EventArgs e
  )
  {
    this.SaveSettings();
  }

  private void textBoxFilter_TextChanged(
    object    sender,
    EventArgs e
  )
  {
    this.FilterData();
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

    var keySource      = FormMain.GetCellValue(row, GridColumns.Uuid);
    var versionSource  = FormMain.GetCellValue(row, GridColumns.Version);
    var translatedNode = FormMain.SelectNode(this.TranslatedDoc, keySource, versionSource);
    var newText        = this.textBoxTranslatedText.Text;

    if (translatedNode != null)
    {
      this.UpdateDataText(row, newText);
      translatedNode.InnerText = newText;
    }
    else { FormMain.AddNode(this.TranslatedDoc, keySource, versionSource, newText); }

    this.UpdateRowStatus();
    this.RecalcRowsAndColumnSizesHeights();
  }

  private void UpdateDataText(
    DataGridViewRow row,
    string          newText
  )
  {
    row.Cells[(int)GridColumns.Text].Value = newText;

    foreach (DataRow dataRow in this.DataTable.Rows)
    {
      var dataUid     = dataRow[(int)GridColumns.Uuid].ToString();
      var rowUid      = row.Cells[(int)GridColumns.Uuid].FormattedValue?.ToString();
      var dataVersion = dataRow[(int)GridColumns.Version].ToString();
      var rowVersion  = row.Cells[(int)GridColumns.Version].FormattedValue?.ToString();

      if (dataUid != rowUid
          || dataVersion != rowVersion) continue;

      dataRow[(int)GridColumns.Text] = newText;

      break;
    }
  }

  #endregion

  private void buttonPakMod_Click(
    object    sender,
    EventArgs e
  )
  {
    var parent = Directory.GetParent(
                                     Directory.GetParent(Directory.GetParent(this.textBoxTranslatedFile.Text).FullName)
                                              .FullName
                                    );

    var pakSource = parent.FullName;

    var packer = new PackageEngine( Directory.GetParent(parent.FullName).FullName, parent.Name);
    packer.BuildPackage();
  }
}