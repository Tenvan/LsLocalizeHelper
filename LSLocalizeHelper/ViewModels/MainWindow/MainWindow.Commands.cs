using System.Windows;
using System.Windows.Input;

using CommunityToolkit.Mvvm.Input;

namespace LSLocalizeHelper;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{
  public ICommand ShowSettings { get; private set; }

  private void InitCommand()
  {
    this.ShowSettings = new RelayCommand(this.ShowSettingsDialog, () => true);
  }

  private void Button_OnClick(
    object          sender,
    RoutedEventArgs e
  )
  {
    this.ShowSettingsDialog();
  }
}