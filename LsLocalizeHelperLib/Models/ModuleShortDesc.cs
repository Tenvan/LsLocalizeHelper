using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace LsLocalizeHelperLib.Models;

public class ModuleShortDesc : ReactiveObject
{

  #region Constructors

  public ModuleShortDesc(string name,
                         string folder,
                         string version,
                         string uuid
  )
  {
    this.Name = name;
    this.Folder = folder;
    this.Version = version;
    this.UUID = uuid;
  }

  #endregion

  #region Properties

  [Reactive]
  public string Folder { get; set; }

  [Reactive]
  public string? MD5 { get; set; }

  [Reactive]
  public string Name { get; set; }

  [Reactive]
  public string UUID { get; set; }

  [Reactive]
  public string Version { get; set; }

  #endregion

}
