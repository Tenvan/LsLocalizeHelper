using LsLocalizeHelperLib.Services;

using Xunit.Abstractions;

using DirectoryInfo = Alphaleonis.Win32.Filesystem.DirectoryInfo;
using Path = Alphaleonis.Win32.Filesystem.Path;

namespace LsHelperUnitTests.Tests;

public class DataTests
{

  #region Static Methods

  private static XmlFilesService GetFilesService()
  {
    var dataDir = DataTests.GetTestDataFolder();

    var modsService = new LsModsService();
    modsService.LoadMods(dataDir.FullName);

    var xmlFilesService = new XmlFilesService(dataDir.FullName);
    var modModels = modsService.Items.ToArray();

    xmlFilesService.Load(modModels);

    return xmlFilesService;
  }

  private static DirectoryInfo GetTestDataFolder()
  {
    var dirInfo = new DirectoryInfo(".").Parent.Parent.Parent.Parent;
    var testDataFolder = Path.Combine(dirInfo.FullName, "TestingData");
    var test = new DirectoryInfo(testDataFolder);

    return test;
  }

  private static void LoadTestData()
  {
    var xmlFilesService = DataTests.GetFilesService();

    var translated = xmlFilesService.Items.Where(
                                       f => f.Name.ToLower()
                                             .StartsWith("german\\")
                                     )
                                    .ToArray();

    var origins = xmlFilesService.Items.Where(
                                    f => f.Name.ToLower()
                                          .StartsWith("english\\")
                                  )
                                 .ToArray();

    LsWorkingDataService.Load(translatedFiles: translated, currentFiles: origins, previousFiles: origins);
  }

  #endregion

  #region Fields

  private readonly ITestOutputHelper testOutputHelper;

  #endregion

  #region Constructors

  public DataTests(ITestOutputHelper testOutputHelper) { this.testOutputHelper = testOutputHelper; }

  #endregion

  #region Methods

  [Fact]
  public void ShouldLoadData()
  {
    DataTests.LoadTestData();

    LsWorkingDataService.TranslatedItems.Count()
                        .Should()
                        .BeGreaterThan(0);

    LsWorkingDataService.OriginCurrentItems.Count()
                        .Should()
                        .BeGreaterThan(0);

    LsWorkingDataService.OriginPreviousItems.Count()
                        .Should()
                        .BeGreaterThan(0);
  }

  [Fact]
  public void ShouldValidateLoadedData()
  {
    DataTests.LoadTestData();

    LsWorkingDataService.TranslatedItemsWithErrors.Count()
                        .Should()
                        .BeGreaterThan(0);
  }

  #endregion

}
