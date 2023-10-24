using System;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

using Alphaleonis.Win32.Filesystem;

using Bg3LocaHelper;

using LSLib.LS;
using LSLib.LS.Enums;

using SearchOption = System.IO.SearchOption;

namespace LSLocalizeHelper.Services;

public class Bg3UnpackageEngine
{

  #region Constructors

  public Bg3UnpackageEngine(string modsPath, string pakPath, string modName)
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

  public void ImportPackage()
  {
    try
    {
      this.PrepareFolder();
      this.UncompressPackage();
      this.PrepareMod();
      this.PrepareMeta();
      this.CleanUp();
      MessageBox.Show($"mod {this.ModName} successfully imported ind folder:\n{this.ModFolder}");
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex);
      MessageBox.Show($"error on import:\n{ex.Message}");
    }
  }

  private void CleanUp()
  {
    Directory.Delete(path: this.TempFolder, recursive: true);
  }

  private void GenerateMetaLsx(string metaPath,
                               string author,
                               string description,
                               PackedVersion version
  )
  {
    if (string.IsNullOrEmpty(author)
        || string.IsNullOrEmpty(description))
    {
      return;
    }

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

    xml.Save(metaPath);
  }

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

    var metaFile = Path.Combine(
      this.ModWorkFolder,
      "Mods",
      this.ModName,
      "meta.lsx"
    );

    this.GenerateMetaLsx(
      metaPath: metaFile,
      author: "Tenvan",
      description: "new translation",
      version: version
    );
  }

  private void PrepareMod()
  {
    var tempDir = new DirectoryInfo(this.TempFolder);
    var localsDir = tempDir.GetDirectories(searchPattern: "Localization", searchOption: SearchOption.AllDirectories);

    foreach (var dir in localsDir)
    {
      var localsTargetPath = Path.Combine(this.ModWorkFolder, "Localization");
      Directory.Copy(sourcePath: dir.FullName, destinationPath: localsTargetPath);
    }

    var modsDir = Path.Combine(this.ModWorkFolder, "Mods");
    Directory.CreateDirectory(modsDir);
  }

  private void UncompressPackage()
  {
    try
    {
      var options = new PackageCreationOptions();
      options.Version = PackageVersion.V18;
      options.Compression = CompressionMethod.LZ4;
      options.Priority = 0;
      var packager = new Packager();

      packager.UncompressPackage(packagePath: this.PakPath, outputPath: this.TempFolder);
    }
    catch (Exception ex)
    {
      MessageBox.Show(
        text: $"Internal error!{Environment.NewLine}{Environment.NewLine}{ex}",
        caption: "Package Uncompress Failed",
        buttons: MessageBoxButtons.OK,
        icon: MessageBoxIcon.Error
      );
    }
  }

  #endregion

}
