using System.Xml.Linq;

using LsLocalizeHelperLib.Helper;

using Directory = Alphaleonis.Win32.Filesystem.Directory;
using DirectoryInfo = Alphaleonis.Win32.Filesystem.DirectoryInfo;
using Path = Alphaleonis.Win32.Filesystem.Path;

namespace LsLocalizeHelperLib.Services;

public class LsUnpackageEngine
{

  #region Constructors

  public LsUnpackageEngine(string modsPath, string pakPath, string modName)
  {
    this.ModsPath = modsPath;
    this.PakPath = pakPath;
    this.ModName = modName;
  }

  #endregion

  #region Properties

  public string ModName { get; }

  public string ModsPath { get; }

  public string PakPath { get; }

  private string ModFolder => Path.Combine(this.ModsPath, this.ModName);

  private string ModWorkFolder => Path.Combine(this.ModFolder, "Work");

  /// <summary>
  /// Path to the temp directory to use.
  /// </summary>
  private string TempFolder => Path.Combine(this.ModsPath, this.ModName, "Temp");

  #endregion

  #region Methods

  public void CheckEnglishLocalization()
  {
    var englishLocalizationPath = Path.Combine(this.TempFolder, "Localization", "English");

    if (Directory.GetFiles(path: englishLocalizationPath, searchPattern: "*.xml")
                 .Length
        > 0) { return; }

    throw new Exception("R-6D51Eefc-7825-40A6-A4C1-69D7D9F4261F".FromResource());
  }

  public string? ExtractOriginPackage()
  {
    try
    {
      this.PrepareFolder();
      this.UncompressPackage();
      this.CheckEnglishLocalization();

      return null;
    }
    catch (Exception ex) { return ex.Message; }
  }

  public void GenerateMetaLsx(string author, string description, PackedVersion version)
  {
    if (string.IsNullOrEmpty(author)
        || string.IsNullOrEmpty(description)) { return; }

    var xmlText = FileHelper.LoadFileTemplate("meta.lsx");
    var xml = XDocument.Parse(xmlText);

    xml.Descendants("attribute")
       .Single(n => n.Attribute("id")!.Value == "Author")
       .Attribute("value")!.Value = author;

    xml.Descendants("attribute")
       .Single(n => n.Attribute("id")!.Value == "Description")
       .Attribute("value")!.Value = description;

    xml.Descendants("attribute")
       .Single(n => n.Attribute("id")!.Value == "Folder")
       .Attribute("value")!.Value = this.ModName;

    xml.Descendants("attribute")
       .Single(n => n.Attribute("id")!.Value == "Name")
       .Attribute("value")!.Value = this.ModName;

    xml.Descendants("attribute")
       .Single(n => n.Attribute("id")!.Value == "UUID")
       .Attribute("value")!.Value = Guid.NewGuid()
                                        .ToString();

    xml.Descendants("attribute")
       .Where(n => n.Attribute("id")!.Value == "Version")
       .ToList()
       .ForEach(
          n =>
          {
            n.Attribute("value")!.Value = version.ToVersion64()
                                                 .ToString();
          }
        );

    var metaFile = Path.Combine(
      this.ModWorkFolder,
      "Mods",
      this.ModName,
      "meta.lsx"
    );

    Directory.CreateDirectory(Path.GetDirectoryName(metaFile));

    xml.Save(metaFile);
  }

  public void ImportNewPackage(string language)
  {
    try
    {
      this.PrepareMod(language);
      this.PrepareMeta();
    }
    finally { this.CleanUp(); }
  }

  private void CleanUp() { Directory.Delete(path: this.TempFolder, recursive: true); }

  private void PrepareFolder()
  {
    var dirMod = Directory.CreateDirectory(this.ModFolder);
    dirMod.Delete(true);
    dirMod.Create();
    var dirModTemp = Directory.CreateDirectory(this.TempFolder);
    dirModTemp.Create();
  }

  private void PrepareMeta()
  {
    var version = new PackedVersion()
    {
      Major = 1,
      Minor = 0,
      Build = 0,
      Revision = 0,
    };

    Directory.CreateDirectory(Path.Combine(this.ModWorkFolder, "Mods", this.ModName));

    this.GenerateMetaLsx(author: "Tenvan", description: "new translation", version: version);
  }

  private void PrepareMod(string language)
  {
    var tempDir = new DirectoryInfo(Path.Combine(this.TempFolder, "Localization", "English"));

    var localsTargetPath = Path.Combine(this.ModWorkFolder, "Localization");
    Directory.Copy(sourcePath: tempDir.FullName, destinationPath: Path.Combine(localsTargetPath, "English"));
    Directory.Copy(sourcePath: tempDir.FullName, destinationPath: Path.Combine(localsTargetPath, language));

    // var modsDir = Path.Combine(this.ModWorkFolder, "Mods");
    // Directory.CreateDirectory(modsDir);
  }

  private void UncompressPackage()
  {
    try
    {
      var options = new PackageCreationOptions
      {
        Version = PackageVersion.V18,
        Compression = CompressionMethod.LZ4,
        Priority = 0,
      };

      var packager = new Packager();

      packager.UncompressPackage(packagePath: this.PakPath, outputPath: this.TempFolder);
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Internal error!{Environment.NewLine}{Environment.NewLine}{ex}");

      throw;
    }
  }

  #endregion

}
