using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

using LSLocalizeHelper.Helper;
using LSLocalizeHelper.Services;

namespace LSLocalizeHelper.Views;

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

    this.ApplySettingsCommand = new AnotherCommandImplementation(this.ApplySettings);
    this.AbortSettingsCommand = new AnotherCommandImplementation(this.AbortSettings);
    this.BrowseFolderCommand = new AnotherCommandImplementation(this.BrowseFolder);
  }

  #endregion

  #region Properties

  public ICommand AbortSettingsCommand { get; }

  public ICommand ApplySettingsCommand { get; }

  public ICommand BrowseFolderCommand { get; }

  #endregion

  #region Methods

  private void AbortSettings(object? _)
  {
    this.DialogResult = false;
    this.Close();
  }

  private void ApplySettings(object? _)
  {
    SettingsManager.Settings!.ModsPath = this.TextBoxModsPath.Text;
    SettingsManager.Save();
    this.DialogResult = true;
    this.Close();
  }

  private void BrowseFolder(object? _)
  {
    var dialog = new FolderBrowserDialog();
    var result = dialog.ShowDialog();

    if (result == System.Windows.Forms.DialogResult.OK) { this.TextBoxModsPath.Text = dialog.SelectedPath; }
  }

  #endregion

}
