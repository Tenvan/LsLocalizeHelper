using Alphaleonis.Win32.Filesystem;

namespace LSLocalizeHelper.Models;

public class XmlFileModel
{
  /// <summary>
  /// Short Name/Path of Xml-File
  /// </summary>
  public string? Name { get; set; }

  /// <summary>
  /// Full path info for Xml-File
  /// </summary>
  public FileInfo? FullPath { get; set; }

  /// <summary>
  /// The Mod which contains this file
  /// </summary>
  public ModModel Mod { get; set; }
}