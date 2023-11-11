using LsLocalizeHelperLib.Services;

using Xunit.Abstractions;

using DirectoryInfo = Alphaleonis.Win32.Filesystem.DirectoryInfo;
using Path = Alphaleonis.Win32.Filesystem.Path;

namespace LsHelperUnitTests.Tests;

public class FilesServiceTests
{

  #region Static Methods

  private static DirectoryInfo GetTestDataFolder()
  {
    var dirInfo = new DirectoryInfo(".").Parent.Parent.Parent.Parent;
    var testDataFolder = Path.Combine(dirInfo.FullName, "TestingData");
    var test = new DirectoryInfo(testDataFolder);

    return test;
  }

  #endregion

  #region Fields

  private readonly ITestOutputHelper testOutputHelper;

  #endregion

  #region Constructors

  public FilesServiceTests(ITestOutputHelper testOutputHelper) { this.testOutputHelper = testOutputHelper; }

  #endregion

  #region Methods

  [Fact]
  public void ShouldLoadFilesServiceFiles()
  {
    var target = FilesServiceTests.GetFilesService();

    target.Items.Count.Should()
                   .BeGreaterThan(0);
  }

  [Fact]
  public void ShouldGetTranslatedFiles()
  {
    var xmlFilesService = FilesServiceTests.GetFilesService();

    var target = xmlFilesService.Items.Where(
      f => f.Name.ToLower()
            .StartsWith("german\\")
    ).ToArray();

    target.Length.Should()
              .Be(4);
  }

  [Fact]
  public void ShouldGetOriginFiles()
  {
    var xmlFilesService = FilesServiceTests.GetFilesService();

    var target = xmlFilesService.Items.Where(
      f => f.Name.ToLower()
            .StartsWith("english\\")
    ).ToArray();

    target.Length.Should()
              .Be(4);
  }

  private static XmlFilesService GetFilesService()
  {
    var dataDir = FilesServiceTests.GetTestDataFolder();

    var modsService = new LsModsService();
    modsService.LoadMods(dataDir.FullName);

    var xmlFilesService = new XmlFilesService(dataDir.FullName);
    var modModels = modsService.Items.ToArray();

    xmlFilesService.Load(modModels);

    return xmlFilesService;
  }

  #endregion

}
