using System.Windows;
using System.Windows.Forms;

using LsLocalizeHelperLib.Services;

namespace LsLocalizeHelper.Views;

/// <summary>
/// Interaktionslogik für Settings.xaml
/// </summary>
public partial class SettingsDialog : Window
{

  #region Constructors

  public SettingsDialog()
  {
    this.InitializeComponent();
    this.TextBoxModsPath.Text = SettingsManager.Settings?.ModsPath;
  }

  #endregion

  #region Methods

  private void AbortSettings()
  {
    this.DialogResult = false;
    this.Close();
  }

  private void ApplySettings()
  {
    SettingsManager.Settings!.ModsPath = this.TextBoxModsPath.Text;
    SettingsManager.Save();
    this.DialogResult = true;
    this.Close();
  }

  private void BrowseFolder()
  {
    var dialog = new FolderBrowserDialog();
    var result = dialog.ShowDialog();

    if (result == System.Windows.Forms.DialogResult.OK) { this.TextBoxModsPath.Text = dialog.SelectedPath; }
  }

  private void ButtonAbort_OnClick(object sender, RoutedEventArgs e) { this.AbortSettings(); }

  private void ButtonApply_OnClick(object sender, RoutedEventArgs e) { this.ApplySettings(); }

  private void ButtonBrowseFolder_OnClick(object sender, RoutedEventArgs e) { this.BrowseFolder(); }

  #endregion

}
