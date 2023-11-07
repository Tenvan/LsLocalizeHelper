using LSLib.LS;

using LsLocalizeHelperLib.Services;

using Xunit.Abstractions;

using Directory = Alphaleonis.Win32.Filesystem.Directory;
using Path = Alphaleonis.Win32.Filesystem.Path;

namespace LsHelperUnitTests.Tests;

public class LsUnpackageEngineTests : LsHelperTestsBase
{

  #region Fields

  private readonly ITestOutputHelper testOutputHelper;

  #endregion

  #region Constructors

  public LsUnpackageEngineTests(ITestOutputHelper testOutputHelper) { this.testOutputHelper = testOutputHelper; }

  #endregion

  #region Methods

  [Fact]
  public void ShouldContainEnglishLocalizationFile()
  {
    var modName = "TestMod-" + Guid.NewGuid();

    var pathMods = this.GetTestExtractDataFolder()
                       .FullName;

    this.CleanDir(Path.Combine(pathMods, modName));

    var pathTestPak = Path.Combine(pathMods, "test.pak");

    var engine = new LsUnpackageEngine(modsPath: pathMods, pakPath: pathTestPak, modName: modName);

    engine.ExtractOriginPackage();

    var localizationPath = Path.Combine(
      pathMods,
      modName,
      "Temp",
      "Localization",
      "English"
    );

    Directory.GetFiles(path: localizationPath, searchPattern: "*.xml")
             .Length.Should()
             .Be(1);

    engine.CheckEnglishLocalization();

    this.CleanDir(Path.Combine(pathMods, modName));
  }

  [Fact]
  public void ShouldExtractPakFile()
  {
    var modName = "TestMod-" + Guid.NewGuid();

    var pathMods = this.GetTestExtractDataFolder()
                       .FullName;

    this.CleanDir(Path.Combine(pathMods, modName));

    var pathTestPak = Path.Combine(pathMods, "test.pak");

    var engine = new LsUnpackageEngine(modsPath: pathMods, pakPath: pathTestPak, modName: modName);

    engine.ExtractOriginPackage();

    var tempPath = Path.Combine(
      pathMods,
      modName,
      "Temp",
      "Mods"
    );

    Directory.Exists(tempPath)
             .Should()
             .BeTrue();

    this.CleanDir(Path.Combine(pathMods, modName));
  }

  [Fact]
  public void ShouldGenerateMetaFile()
  {
    var modName = "TestMod-" + Guid.NewGuid();

    var pathMods = this.GetTestExtractDataFolder()
                       .FullName;

    this.CleanDir(Path.Combine(pathMods, modName));

    var pathTestPak = Path.Combine(pathMods, "test.pak");

    var engine = new LsUnpackageEngine(modsPath: pathMods, pakPath: pathTestPak, modName: modName);

    engine.GenerateMetaLsx(
      author: "Tenvan",
      description: "Test Pak",
      version: new PackedVersion()
      {
        Major = 1,
        Minor = 0,
        Build = 0,
        Revision = 0,
      }
    );

    var pathMeta = Path.Combine(
      pathMods,
      modName,
      "Work",
      "Mods",
      modName,
      "meta.lsx"
    );

    File.Exists(pathMeta)
        .Should()
        .BeTrue();

    this.CleanDir(Path.Combine(pathMods, modName));
  }

  [Fact]
  public void ShouldImportPakFile()
  {
    var modName = "TestMod-" + Guid.NewGuid();

    var pathMods = this.GetTestExtractDataFolder()
                       .FullName;

    this.CleanDir(Path.Combine(pathMods, modName));

    var pathTestPak = Path.Combine(pathMods, "test.pak");

    var engine = new LsUnpackageEngine(modsPath: pathMods, pakPath: pathTestPak, modName: modName);

    engine.ExtractOriginPackage();
    engine.ImportNewPackage("German");

    var englishModPath = Path.Combine(
      pathMods,
      modName,
      "Work",
      "Localization",
      "English"
    );

    Directory.Exists(englishModPath)
             .Should()
             .BeTrue();

    var germanModPath = Path.Combine(
      pathMods,
      modName,
      "Work",
      "Localization",
      "German"
    );

    Directory.Exists(germanModPath)
             .Should()
             .BeTrue();

    var tempPath = Path.Combine(pathMods, modName, "Temp");

    Directory.Exists(tempPath)
             .Should()
             .BeFalse();

    this.CleanDir(Path.Combine(pathMods, modName));
  }

  private void CleanDir(string path)
  {
    if (Directory.Exists(path)) { Directory.Delete(path: path, recursive: true); }
  }

  #endregion

}
