using System;
using System.Windows.Forms;

using Bg3LocaHelper.Properties;

namespace Bg3LocaHelper;

public partial class FormSettings : Form
{

  public FormSettings()
  {
    this.InitializeComponent();
    this.LoadSettings();
  }

  private void LoadSettings()
  {
    this.textBoxModsPath.Text = Settings.Default.pathMods;
  }

  private void buttonFileModsPath_Click(object sender, EventArgs e)
  {
    var result = this.folderBrowserDialogModsPath.ShowDialog();

    if (result == DialogResult.OK)
    {
      this.textBoxModsPath.Text = this.folderBrowserDialogModsPath.SelectedPath;
    }
  }

  private void buttonApply_Click(object sender, EventArgs e)
  {
    this.SaveSettings();
    this.Close();
  }

  private void SaveSettings()
  {
    Settings.Default.pathMods = this.textBoxModsPath.Text;
    Settings.Default.Save();
  }

}
