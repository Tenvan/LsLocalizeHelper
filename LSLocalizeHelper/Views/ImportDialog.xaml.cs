using System.Windows;
using System.Windows.Forms;

using Alphaleonis.Win32.Filesystem;

using LSLocalizeHelper.Services;

namespace LSLocalizeHelper.Views;

public partial class ImportDialog : Window
{

  #region Constructors

  public ImportDialog() { InitializeComponent(); }

  #endregion

  #region Properties

  public string? Error { get; set; }

  #endregion

  #region Methods

  private void ButtonAbort_OnClick(object sender, RoutedEventArgs e) { this.Close(); }

  private void ButtonBrowseFolder_OnClick(object sender, RoutedEventArgs e)
  {
    var dialog = new OpenFileDialog();
    dialog.DefaultExt = ".pak";
    dialog.Filter = "Mod Package (.pak)|*.pak";
    var result = dialog.ShowDialog();

    if (result != System.Windows.Forms.DialogResult.OK) { return; }

    this.TextBoxPakFile.Text = dialog.FileName;
    this.TextBoxModName.Text = Path.GetFileNameWithoutExtension(dialog.SafeFileName);
  }

  private void ButtonImport_OnClick(object sender, RoutedEventArgs e)
  {
    try
    {
      var packer = new Bg3UnpackageEngine(
        modsPath: SettingsManager.Settings.ModsPath,
        pakPath: this.TextBoxPakFile.Text,
        modName: this.TextBoxModName.Text
      );

      var result = packer.ImportPackage();
      this.Error = result;
      this.DialogResult = string.IsNullOrEmpty(result);
    }
    finally { this.Close(); }
  }

  #endregion

}
