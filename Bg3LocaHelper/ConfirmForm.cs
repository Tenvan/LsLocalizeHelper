using System;
using System.Windows.Forms;

namespace Bg3LocaHelper;

public partial class ConfirmForm : Form
{
  public ConfirmForm(
    string text
  )
  {
    InitializeComponent();
    this.labelText.Text = text;
  }

  private void buttonYes_Click(
    object    sender,
    EventArgs e
  )
  {
    this.DialogResult = DialogResult.Yes;
    this.Close();
  }

  private void buttonNo_Click(
    object    sender,
    EventArgs e
  )
  {
    this.DialogResult = DialogResult.No;
    this.Close();
  }
}