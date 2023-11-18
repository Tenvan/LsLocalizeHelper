using ReactiveUI;
using ReactiveUI.Fody.Helpers;

using DirectoryInfo = Alphaleonis.Win32.Filesystem.DirectoryInfo;

namespace LsLocalizeHelperLib.Models;

public class ModModel : ReactiveObject
{

  #region Constructors

  public ModModel(DirectoryInfo folder, string name)
  {
    this.Folder = folder;
    this.Name = name;
  }

  #endregion

  #region Properties

  [Reactive]
  public DirectoryInfo Folder { get; set; }

  [Reactive]
  public string Name { get; set; }

  #endregion

}
