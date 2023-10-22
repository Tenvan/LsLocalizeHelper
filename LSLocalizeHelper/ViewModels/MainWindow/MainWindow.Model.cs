using System.Collections.Generic;
using System.Windows.Controls;

namespace LSLocalizeHelper;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{
  #region Properties

  public List<Control> ProjectItems { get; set; } = new()
                                                    {
                                                      new CheckBox() { Content = "Project 1" },
                                                      new CheckBox() { Content = "Project 2" },
                                                      new CheckBox() { Content = "Project 3" }
                                                    };

  #endregion
}