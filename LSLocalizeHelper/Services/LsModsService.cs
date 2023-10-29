using System.Collections.Generic;
using System.IO;

using LSLocalizeHelper.Models;

using DirectoryInfo = Alphaleonis.Win32.Filesystem.DirectoryInfo;

namespace LSLocalizeHelper.Services;

public class LsModsService
{

  #region Fields

  public List<ModModel> Items = new();

  #endregion

  #region Methods

  public void LoadMods()
  {
    var settingsModsPath = SettingsManager.Settings?.ModsPath;

    if (string.IsNullOrWhiteSpace(settingsModsPath)) { return; }

    var dirInfo = new DirectoryInfo(settingsModsPath);

    if (!dirInfo.Exists) { return; }

    var metaFiles = dirInfo.GetFiles(searchPattern: "meta.lsx", searchOption: SearchOption.AllDirectories);
    this.Items.Clear();

    foreach (var metaFile in metaFiles)
    {
      var mod = new ModModel(
        folder: metaFile.Directory?.Parent?.Parent?.Parent!,
        name: metaFile.Directory?.Parent?.Parent?.Parent.Name!
      );

      this.Items.Add(mod);
    }
  }

  #endregion

}
