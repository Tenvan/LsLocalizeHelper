using System.Windows;

using LsLocalizeHelperLib.Models;

using Directory = Alphaleonis.Win32.Filesystem.Directory;
using File = Alphaleonis.Win32.Filesystem.File;
using Path = Alphaleonis.Win32.Filesystem.Path;

namespace LsLocalizeHelperLib.Services;

public static class SettingsManager
{

  #region Static Fields

  private static readonly string settingsPath;

  #endregion

  #region Static Constructors

  static SettingsManager()
  {
    SettingsManager.settingsPath = SettingsManager.GetLocalFilePath("UserSettings.json");
    SettingsManager.Settings = new UserSettings();
  }

  #endregion

  #region Static Properties

  public static UserSettings? Settings { get; set; }

  #endregion

  #region Static Methods

  public static void Load()
  {
    if (File.Exists(SettingsManager.settingsPath))
    {
      try
      {
        SettingsManager.Settings
          = JsonConvert.DeserializeObject<UserSettings>(File.ReadAllText(SettingsManager.settingsPath));
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
        MessageBox.Show($"Error on loading Usersettings:\n{ex.Message}\nConfig file will be deleted");
        File.Delete(SettingsManager.settingsPath);
        Application.Current.MainWindow.Close();
      }
    }

    Console.WriteLine(JsonConvert.SerializeObject(value: SettingsManager.Settings, formatting: Formatting.Indented));
  }

  public static void Save()
  {
    Directory.CreateDirectory(Path.GetDirectoryName(SettingsManager.settingsPath));
    var json = JsonConvert.SerializeObject(value: SettingsManager.Settings, formatting: Formatting.Indented);
    File.WriteAllText(path: SettingsManager.settingsPath, contents: json);
  }

  private static string GetLocalFilePath(string fileName)
  {
    var appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

    return Path.Combine(appData, "LsLocalizeHelper", fileName);
  }

  #endregion

}
