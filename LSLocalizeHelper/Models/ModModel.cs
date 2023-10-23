using Alphaleonis.Win32.Filesystem;

namespace LSLocalizeHelper.Models;

public class ModModel
{
  #region Properties

  public DirectoryInfo Folder { get; set; }

  public string? Name { get; set; }

  public string UUID { get; set; }

  public string Version { get; set; }

  #endregion
}