using Alphaleonis.Win32.Filesystem;

namespace LSLocalizeHelper.Models;

public class XmlFileModel
{

  #region Constructors

  public XmlFileModel(string name, FileInfo fullPath, ModModel mod)
  {
    this.FullPath = fullPath;
    this.Mod = mod;
    this.Name = name;
  }

  #endregion

  #region Properties

  /// <summary>
  /// Full path info for Xml-File
  /// </summary>
  public FileInfo FullPath { get; set; }

  /// <summary>
  /// The Mod which contains this file
  /// </summary>
  public ModModel Mod { get; set; }

  /// <summary>
  /// Short Name/Path of Xml-File
  /// </summary>
  public string Name { get; set; }

  #endregion

}
