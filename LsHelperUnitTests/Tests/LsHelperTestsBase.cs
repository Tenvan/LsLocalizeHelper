using LsLocalizeHelperLib.Services;

using DirectoryInfo = Alphaleonis.Win32.Filesystem.DirectoryInfo;
using Path = Alphaleonis.Win32.Filesystem.Path;

namespace LsHelperUnitTests.Tests;

public class LsHelperTestsBase
{

  #region Methods

  protected void LoadTestData()
  {
    var xmlFilesService = this.GetFilesService();

    var translated = xmlFilesService.Items.Where(
                                       f => f.Name.ToLower()
                                             .StartsWith("german\\translated")
                                     )
                                    .ToArray();

    var origins = xmlFilesService.Items.Where(
                                    f => f.Name.ToLower()
                                          .StartsWith("english\\")
                                  )
                                 .ToArray();

    LsWorkingDataService.Load(translatedFiles: translated, currentFiles: origins, previousFiles: origins);
  }

  protected XmlFilesService GetFilesService()
  {
    var dataDir = this.GetTestDataFolder();

    var modsService = new LsModsService();
    modsService.LoadMods(dataDir.FullName);

    var xmlFilesService = new XmlFilesService(dataDir.FullName);
    var modModels = modsService.Items.Where(m => m.Name.StartsWith("10_")).ToArray();

    xmlFilesService.Load(modModels);

    return xmlFilesService;
  }

  protected DirectoryInfo GetTestDataFolder()
  {
    var dirInfo = new DirectoryInfo(".").Parent.Parent.Parent.Parent;
    var testDataFolder = Path.Combine(dirInfo.FullName, "TestingData");
    var test = new DirectoryInfo(testDataFolder);

    return test;
  }

  #endregion

}
