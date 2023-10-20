using System;
using System.Windows.Forms;

namespace Bg3LocaHelper;

static class Program
{
  /// <summary>
  /// The main entry point for the application.
  /// </summary>
  [STAThread]
  static void Main()
  {
    try
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new FormMain());
    }
    catch (Exception ex)
    {
      MessageBox.Show(ex.Message);
    }
  }
}