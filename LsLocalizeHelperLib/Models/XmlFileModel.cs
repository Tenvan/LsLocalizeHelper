using ReactiveUI;
using ReactiveUI.Fody.Helpers;

using FileInfo = Alphaleonis.Win32.Filesystem.FileInfo;

namespace LsLocalizeHelperLib.Models;

public class XmlFileModel : ReactiveObject
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
  [Reactive]
  public FileInfo FullPath { get; set; }

  /// <summary>
  /// The Mod which contains this file
  /// </summary>
  [Reactive]
  public ModModel Mod { get; set; }

  /// <summary>
  /// Short Name/Path of Xml-File
  /// </summary>
  [Reactive]
  public string Name { get; set; }

  #endregion

}
