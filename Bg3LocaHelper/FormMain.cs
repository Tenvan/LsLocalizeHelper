using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;

using Alphaleonis.Win32.Filesystem;

using Bg3LocaHelper.Properties;

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

  private static string? GetTextFromSource(
    object sender
  )
  {
    if (sender is not ToolStripMenuItem tmenu)
      return null;

    if (tmenu.Owner is not ContextMenuStrip cmenu)
      return null;

    if (cmenu.SourceControl is not ComboBox combobox)
      return null;

    return combobox.Text;
  }

  private static XmlNode? SelectNode(
    XmlNode? doc,
    object   keySource,
    object   versionSource
  )
  {
    var xpath = versionSource == null
                  ? $"//content[@contentuid='{keySource}']"
                  : $"//content[@contentuid='{keySource}' and @version='{versionSource}']";

    return doc?.SelectSingleNode(xpath);
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

    if (string.IsNullOrWhiteSpace(Settings.Default.pathMods)) { new FormSettings().ShowDialog(); }

    ;

    if (string.IsNullOrWhiteSpace(Settings.Default.pathMods))
    {
      MessageBox.Show("please define first the mods working root folder!");
      Application.Exit();
    }

    this.LoadMods();
    this.LoadSettings();
  }

  #endregion

  #region Properties

  private bool AutoClipbaord => this.checkBoxAutoClipboard.Checked;

  private string CurrentModName
  {
    get => this.comboBoxMods.Text;
  }

  private string OriginCurrentFileFullName => Path.Combine(this.modWorkFolder?.FullName, this.OriginCurrentFile);

  private string OriginPreviousFileFullName => Path.Combine(this.modWorkFolder?.FullName, this.OriginPreviousFile);

  private string TranslatedFileFullName => Path.Combine(this.modWorkFolder?.FullName, this.TranslatedFile);

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
    get => this.comboBoxOriginCurrentFile.Text;
    set => this.comboBoxOriginCurrentFile.Text = value;
  }

  /// <summary>
  /// Full Xml-Data of previous origin localisation file, filled bei <see cref="LoadData"/>.
  /// </summary>
  private XmlDocument? OriginPreviousDoc { get; set; }

  private string OriginPreviousFile
  {
    get => this.comboBoxOriginPreviousFile.Text;
    set => this.comboBoxOriginPreviousFile.Text = value;
  }

  /// <summary>
  /// Full Xml-Data of current own translated localisation file, filled bei <see cref="LoadData"/>.
  /// </summary>
  private XmlDocument? TranslatedDoc { get; set; }

  private string TranslatedFile
  {
    get => this.comboBoxTranslatedFile.Text;
    set => this.comboBoxTranslatedFile.Text = value;
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

  private void buttonRefreshMods_Click(
    object    sender,
    EventArgs e
  )
  {
    this.RefreshMods();
  }

  private void checkBoxAutoClipboard_CheckedChanged(
    object    sender,
    EventArgs e
  ) { }

  private void comboBoxMods_SelectedValueChanged(
    object    sender,
    EventArgs e
  )
  {
    this.LoadXmlFileNames2ComboBoxes(this.comboBoxMods.Text);
  }

  private void comboBoxOriginCurrentFile_SelectedValueChanged(
    object    sender,
    EventArgs e
  )
  {
    Settings.Default.pathOriginCurrent = this.comboBoxOriginCurrentFile.Text;
    Settings.Default.Save();
  }

  private void comboBoxOriginPreviousFile_SelectedValueChanged(
    object    sender,
    EventArgs e
  )
  {
    Settings.Default.pathOriginPrevious = this.comboBoxOriginPreviousFile.Text;
    Settings.Default.Save();
  }

  private void comboBoxTranslatedFile_SelectedValueChanged(
    object    sender,
    EventArgs e
  )
  {
    Settings.Default.pathTranslated = this.comboBoxTranslatedFile.Text;
    Settings.Default.Save();
  }

  private void copyFileToolStripMenuItem_Click(
    object    sender,
    EventArgs e
  )
  {
    var fileName = FormMain.GetTextFromSource(sender);

    if (fileName == null) return;

    var fileHelper = new FileEngine(
                                    Settings.Default.pathMods,
                                    this.comboBoxMods.Text
                                   );

    if (fileHelper.CopyFile(fileName: fileName)) { this.RefreshMods(); }
  }

  private void copyFileToOtherLanguageToolStripMenuItem_Click(
    object    sender,
    EventArgs e
  )
  {
    var fileName = FormMain.GetTextFromSource(sender);

    if (fileName == null) return;

    var fileHelper = new FileEngine(
                                    Settings.Default.pathMods,
                                    this.comboBoxMods.Text
                                   );

    if (fileHelper.CopyFileToNewLanguage(fileName: fileName)) { this.RefreshMods(); }
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
    var cellText   = e.FormattedValue.ToString();
    var isSelected = (e.State & DataGridViewElementStates.Selected) != 0;
    var fontBrush  = isSelected ? new SolidBrush(e.CellStyle.SelectionForeColor) : new SolidBrush(e.CellStyle.ForeColor);
    var pointF     = new PointF(e.CellBounds.X + 2, e.CellBounds.Y + 2);
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
    var xpath         = versionSource == null ? $"//content[@contentuid='{keySource}']" : $"//content[@contentuid='{keySource}' and @version='{versionSource}']";

    var translatedNode =
      this.TranslatedDoc?.SelectSingleNode(xpath);

    if (translatedNode != null)
    {
      this.textBoxTranslatedText.Text = textSource;
      if (this.AutoClipbaord) Clipboard.SetText(textSource);
    }
    else { this.textBoxTranslatedText.Text = ""; }

    var currentOriginNode =
      this.OriginCurrentDoc?.SelectSingleNode(xpath);

    this.textBoxCurrentOriginText.Text = currentOriginNode != null ? currentOriginNode.InnerText : "";

    var previousOriginNode =
      this.OriginPreviousDoc?.SelectSingleNode(xpath);

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

  private void deleteFileToolStripMenuItem_Click(
    object    sender,
    EventArgs e
  )
  {
    var fileName = FormMain.GetTextFromSource(sender);

    if (fileName == null) return;

    var fileHelper = new FileEngine(
                                    Settings.Default.pathMods,
                                    this.comboBoxMods.Text
                                   );

    if (fileHelper.DeleteFile(fileName: fileName)) { this.RefreshMods(); }
  }

  private void exitToolStripMenuItem_Click(
    object    sender,
    EventArgs e
  )
  {
    Application.Exit();
  }

  private void FormMain_FormClosed(
    object              sender,
    FormClosedEventArgs e
  )
  {
    this.SaveSettings();
  }

  private void importModToolStripMenuItem_Click(
    object    sender,
    EventArgs e
  )
  {
    var formImport   = new FormImport();
    var dialogResult = formImport.ShowDialog();

    if (dialogResult != DialogResult.OK
        || !File.Exists(formImport.PakToImport)
        || string.IsNullOrWhiteSpace(formImport.ModName)) return;

    var packer = new UnpackageEngine(
                                     Settings.Default.pathMods,
                                     formImport.PakToImport,
                                     formImport.ModName
                                    );

    packer.ImportPackage();
  }

  private void loadSourceXMLToolStripMenuItem_Click(
    object    sender,
    EventArgs e
  )
  {
    this.LoadData();
  }

  private void packingModToolStripMenuItem_Click(
    object    sender,
    EventArgs e
  )
  {
    this.PackingMod();
  }

  private void RefreshMods()
  {
    this.LoadMods();
    this.LoadXmlFileNames2ComboBoxes(this.comboBoxMods.Text);
    this.LoadSettings();
  }

  private void renameFileToolStripMenuItem_Click(
    object    sender,
    EventArgs e
  )
  {
    var fileName = FormMain.GetTextFromSource(sender);

    if (fileName == null) return;

    var fileHelper = new FileEngine(
                                    Settings.Default.pathMods,
                                    this.comboBoxMods.Text
                                   );

    if (fileHelper.RenameFile(fileName: fileName)) { this.RefreshMods(); }
  }

  private void saveSourceXMLToolStripMenuItem_Click(
    object    sender,
    EventArgs e
  )
  {
    this.SaveData();
  }

  private void settingsToolStripMenuItem_Click(
    object    sender,
    EventArgs e
  )
  {
    new FormSettings().ShowDialog();
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
}