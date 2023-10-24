using System;
using System.Windows.Forms;

using Alphaleonis.Win32.Filesystem;

namespace Bg3LocaHelper;

public partial class FormImport : Form
{

  #region Constructors

  public FormImport()
  {
    this.InitializeComponent();
  }

  #endregion

  #region Properties

  public string ModName => this.textBoxModName.Text;

  public string PakToImport => this.textBoxPakFile.Text;

  #endregion

  #region Methods

  private void buttonOpenFileDialog_Click(object sender, EventArgs e)
  {
    var result = this.openFileDialog.ShowDialog();

    if (result == DialogResult.OK)
    {
      this.textBoxPakFile.Text = this.openFileDialog.FileName;
      this.textBoxModName.Text = Path.GetFileNameWithoutExtension(this.openFileDialog.SafeFileName);
    }
  }

  #endregion

  private void buttonImport_Click(object sender, EventArgs e)
  {
    this.DialogResult = DialogResult.OK;
    this.Close();
  }

}
