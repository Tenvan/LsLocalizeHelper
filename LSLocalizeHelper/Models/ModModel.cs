using Alphaleonis.Win32.Filesystem;

namespace LSLocalizeHelper.Models;

public class ModModel
{

  #region Constructors

  public ModModel(DirectoryInfo folder, string name)
  {
    this.Folder = folder;
    this.Name = name;
  }

  #endregion

  #region Properties

  public DirectoryInfo Folder { get; set; }

  public string Name { get; set; }

  #endregion

}
