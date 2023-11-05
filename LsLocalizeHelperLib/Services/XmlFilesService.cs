using LsLocalizeHelperLib.Models;

using DirectoryInfo = Alphaleonis.Win32.Filesystem.DirectoryInfo;
using Path = Alphaleonis.Win32.Filesystem.Path;

namespace LsLocalizeHelperLib.Services;

public class XmlFilesService
{

  public XmlFilesService(string? dataDirFullName = null)
  {
    this.ModsDir = dataDirFullName ?? SettingsManager.Settings?.ModsPath ?? ".";
  }

  public string ModsDir { get; set; }

  #region Properties

  public List<XmlFileModel> Items { get; private set; } = new();

  public ModModel[] Mods { get; set; }

  #endregion

  #region Methods

  public void Load(ModModel[] mods)
  {
    this.Mods = mods;

    this.Items.Clear();

    foreach (var modModel in mods) { this.LoadFiles(modModel); }
  }

  private void LoadFiles(ModModel mod)
  {
    var dirInfo = new DirectoryInfo(Path.Combine(this.ModsDir, mod.Name));

    var localsDir = dirInfo.GetDirectories(searchPattern: "Localization", searchOption: SearchOption.AllDirectories)
                           .FirstOrDefault();

    var metaFiles = dirInfo.GetFiles(searchPattern: "*.xml", searchOption: SearchOption.AllDirectories);

    foreach (var metaFile in metaFiles)
    {
      var shortName = Path.GetRelativePath(startPath: localsDir?.FullName, selectedPath: metaFile.FullName);

      var fileModel = new XmlFileModel(name: shortName, fullPath: metaFile, mod: mod);

      this.Items.Add(fileModel);
    }
  }

  #endregion

}
