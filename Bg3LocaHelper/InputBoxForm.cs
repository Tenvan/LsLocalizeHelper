using System;
using System.Windows.Forms;

namespace Bg3LocaHelper;

public partial class InputBoxForm : Form
{
  public InputBoxForm(
    string title,
    string textContent = ""
  )
  {
    this.InitializeComponent();
    this.Text              = title;
    this.textBoxInput.Text = textContent;
  }

  public string InputText
  {
    get => this.textBoxInput.Text;
    set => this.textBoxInput.Text = value;
  }

  private void okButton_Click(
    object    sender,
    EventArgs e
  )
  {
    this.DialogResult = DialogResult.OK;
    this.Close();
  }
}