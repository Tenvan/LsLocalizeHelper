namespace LsLocalizeHelperLib.Models;

public class ModuleShortDesc
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

  public string Folder { get; set; }

  public string? MD5 { get; set; }

  public string Name { get; set; }

  public string UUID { get; set; }

  public string Version { get; set; }

  #endregion

}
