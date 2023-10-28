using System.Collections.Generic;
using System.IO;
using System.Linq;

using LSLocalizeHelper.Models;

using DirectoryInfo = Alphaleonis.Win32.Filesystem.DirectoryInfo;
using Path = Alphaleonis.Win32.Filesystem.Path;

namespace LSLocalizeHelper.Services;

public class XmlFilesService
{

  #region Properties

  public List<XmlFileModel> Items { get; private set; } = new();

  public ModModel[] Mods { get; set; }

  #endregion

  #region Methods

  public void Load(ModModel[] mods)
  {
    this.Mods = mods;

    this.Items.Clear();

    foreach (var modModel in mods)
    {
      this.LoadFiles(modModel);
    }
  }

  private void LoadFiles(ModModel mod)
  {
    var dirInfo = new DirectoryInfo(Path.Combine(SettingsManager.Settings?.ModsPath, mod.Name));

    var localsDir = dirInfo.GetDirectories(searchPattern: "Localization", searchOption: SearchOption.AllDirectories)
                           .FirstOrDefault();

    var modWorkFolder = localsDir?.Parent;
    var metaFiles = dirInfo.GetFiles(searchPattern: "*.xml", searchOption: SearchOption.AllDirectories);

    foreach (var metaFile in metaFiles)
    {
      var shortName = Path.GetRelativePath(startPath: localsDir?.FullName, selectedPath: metaFile.FullName);

      var fileModel = new XmlFileModel()
      {
        FullPath = metaFile,
        Name = shortName,
        Mod = mod,
      };

      this.Items.Add(fileModel);
    }
  }

  #endregion

}
