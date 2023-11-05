using LsLocalizeHelperLib.Services;

using Xunit.Abstractions;

using DirectoryInfo = Alphaleonis.Win32.Filesystem.DirectoryInfo;
using Path = Alphaleonis.Win32.Filesystem.Path;

namespace LsHelperUnitTests.Tests;

public class ModsServiceTests
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

  public ModsServiceTests(ITestOutputHelper testOutputHelper) { this.testOutputHelper = testOutputHelper; }

  #endregion

  #region Methods

  [Fact]
  public void ShouldFindTestDataFolder()
  {
    var target = ModsServiceTests.GetTestDataFolder();

    target.Exists.Should()
        .BeTrue();
  }

  [Fact]
  public void ShouldLoadModsService()
  {
    var dataDir = ModsServiceTests.GetTestDataFolder();
    var target = new LsModsService();
    target.LoadMods(dataDir.FullName);

    target.Items.Count.Should()
               .Be(2);

    var modModels = target.Items.ToArray();

    modModels.Length.Should()
             .Be(2);
  }

  #endregion

}
