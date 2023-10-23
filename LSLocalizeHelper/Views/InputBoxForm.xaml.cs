using System.Windows;

namespace LSLocalizeHelper.Views;

public partial class InputBoxForm : Window
{
  public InputBoxForm() { InitializeComponent(); }

  public InputBoxForm(
    string setNewFileName,
    string newFileName
  )
  {
    throw new System.NotImplementedException();
  }

  public string InputText { get; set; }
}