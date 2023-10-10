using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

using Bg3LocaHelper.Properties;

namespace Bg3LocaHelper
{
  public partial class Form1 : Form
  {
    #region Constructors

    public Form1()
    {
      InitializeComponent();
      this.LoadSettings();

      if (!string.IsNullOrWhiteSpace(this.SourceFile)) { this.LoadData(); }
    }

    #endregion

    #region Properties

    public XmlDocument ReferenceDoc { get; set; }

    public XmlDocument SourceDoc { get; set; }

    private string ReferenceFile
    {
      get => this.textBoxReference.Text;
      set => this.textBoxReference.Text = value;
    }

    private string SourceFile
    {
      get => this.textBoxSource.Text;
      set => this.textBoxSource.Text = value;
    }

    #endregion

    #region Methods

    private void buttonCopyReference_Click(
      object    sender,
      EventArgs e
    )
    {
      Clipboard.SetText(this.textBoxReferenceText.Text);
    }

    private void buttonFileReferecnce_Click(
      object    sender,
      EventArgs e
    )
    {
      var result = this.openFileDialogReference.ShowDialog();

      if (result == DialogResult.OK) { this.textBoxReference.Text = this.openFileDialogReference.FileName; }
    }

    private void buttonFileSource_Click(
      object    sender,
      EventArgs e
    )
    {
      var result = this.openFileDialogSource.ShowDialog();

      if (result == DialogResult.OK) { this.textBoxSource.Text = this.openFileDialogSource.FileName; }
    }

    private void buttonLoad_Click(
      object    sender,
      EventArgs e
    )
    {
      this.LoadData();
    }

    private void buttonPaste_Click(
      object    sender,
      EventArgs e
    )
    {
      var newText = Clipboard.GetText();
      this.textBoxSourceText.Text = newText;
      var row       = this.dataGridViewSource.CurrentRow;
      var keySource = row!.Cells[0].Value;
      row!.Cells[2].Value = newText;
      var sourceNode = this.SourceDoc.SelectSingleNode($"//content[@contentuid='{keySource}']");

      if (sourceNode != null)
        sourceNode.InnerText = newText;
    }

    private void buttonSave_Click(
      object    sender,
      EventArgs e
    )
    {
      this.SourceDoc.Save(this.SourceFile);
    }

    private void dataGridViewSource_RowEnter(
      object                    sender,
      DataGridViewCellEventArgs e
    )
    {
      var dataIndex  = e.RowIndex;
      var row        = (sender as DataGridView)!.Rows[dataIndex];
      var textSource = row.Cells[2].Value.ToString();
      this.textBoxSourceText.Text = textSource;
      Clipboard.SetText(this.textBoxSourceText.Text);
      var keySource     = row.Cells[0].Value.ToString();
      var referenceNode = this.ReferenceDoc.SelectSingleNode($"//content[@contentuid='{keySource}']");

      if (referenceNode != null) { this.textBoxReferenceText.Text = referenceNode.InnerText; }
    }

    private void dataGridViewSource_RowPrePaint(
      object                           sender,
      DataGridViewRowPrePaintEventArgs e
    )
    {
      var row                   = ((sender as DataGridView)!).Rows[e.RowIndex];
      var dataGridViewCellStyle = row.DefaultCellStyle;
      var keySource             = row!.Cells[0].Value;
      var referenceNode         = this.ReferenceDoc.SelectSingleNode($"//content[@contentuid='{keySource}']");

      if (referenceNode != null)
      {
        var textReference = referenceNode.InnerText;
        var textSource    = row!.Cells[2].Value.ToString();
        dataGridViewCellStyle.BackColor = textReference != textSource ? Color.Chartreuse : Color.LightCoral;
      }
      else { dataGridViewCellStyle.BackColor = Color.Indigo; }
    }

    private void LoadData()
    {
      try
      {
        if (File.Exists(this.SourceFile))
        {
          this.SourceDoc = new XmlDocument();
          this.SourceDoc.Load(this.SourceFile);
        }

        if (File.Exists(this.ReferenceFile))
        {
          this.ReferenceDoc = new XmlDocument();
          this.ReferenceDoc.Load(this.ReferenceFile);
          var dataSet = new DataSet();
          dataSet.ReadXml(this.SourceFile);
          var dataTable = dataSet.Tables[dataSet.Tables.Count - 1];
          this.dataGridViewSource.DataSource          = dataTable;
          this.dataGridViewSource.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }
      }
      catch (Exception e) { Debug.Write(e.Message); }
    }

    private void LoadSettings()
    {
      this.ReferenceFile = Settings.Default.pathReference;
      this.SourceFile    = Settings.Default.pathSource;
      this.Size          = Settings.Default.windowSize;
      this.Location      = Settings.Default.windowPos;
    }

    private void SaveSettings()
    {
      Settings.Default.pathReference = this.ReferenceFile;
      Settings.Default.pathSource    = this.SourceFile;
      Settings.Default.windowPos     = this.Location;
      Settings.Default.windowSize    = this.Size;
      Settings.Default.Save();
    }

    private void textBoxSourceText_Leave(
      object    sender,
      EventArgs e
    )
    {
      if (this.dataGridViewSource!.CurrentRow == null) return;

      var newText   = (sender as TextBox)!.Text;
      var row       = this.dataGridViewSource.CurrentRow;
      var keySource = row!.Cells[0].Value;
      row!.Cells[2].Value = newText;
      var sourceNode = this.SourceDoc.SelectSingleNode($"//content[@contentuid='{keySource}']");

      if (sourceNode != null)
        sourceNode.InnerText = newText;
    }

    #endregion

    private void Form1_FormClosed(
      object              sender,
      FormClosedEventArgs e
    )
    {
      this.SaveSettings();
    }
  }
}