using System.IO.Compression;
using System.Security.Cryptography;
using System.Xml;

using LsLocalizeHelperLib.Models;

using Directory = Alphaleonis.Win32.Filesystem.Directory;
using DirectoryInfo = Alphaleonis.Win32.Filesystem.DirectoryInfo;
using File = Alphaleonis.Win32.Filesystem.File;
using FileInfo = Alphaleonis.Win32.Filesystem.FileInfo;
using Path = Alphaleonis.Win32.Filesystem.Path;

namespace LsLocalizeHelperLib.Services;

public class LsPackageEngine
{

  #region Static Methods

  public static MetaLsx ReadMeta(string meta,
                                 DateTime? created = null,
                                 KeyValuePair<string, List<string>>? modGroup = null
  )
  {
    // generate info.json section
    var doc = new XmlDocument();
    doc.Load(meta);
    var moduleInfo = doc.SelectSingleNode("//node[@id='ModuleInfo']");

    var metadata = new MetaLsx
    {
      Author = moduleInfo.SelectSingleNode("attribute[@id='Author']")
                        ?.Attributes["value"].InnerText,
      Name = moduleInfo.SelectSingleNode("attribute[@id='Name']")
                      ?.Attributes["value"].InnerText,
      Description = moduleInfo.SelectSingleNode("attribute[@id='Description']")
                             ?.Attributes["value"].InnerText,
      Version = moduleInfo.SelectSingleNode("attribute[@id='Version']")
                         ?.Attributes["value"].InnerText,
      Folder = moduleInfo.SelectSingleNode("attribute[@id='Folder']")
                        ?.Attributes["value"].InnerText,
      UUID = moduleInfo.SelectSingleNode("attribute[@id='UUID']")
                      ?.Attributes["value"].InnerText,
      Created = created,
      Group = modGroup?.Key ?? string.Empty,
      Dependencies = new List<ModuleShortDesc>(),
    };

    var dependencies = doc.SelectSingleNode("//node[@id='Dependencies']");

    if (dependencies != null)
    {
      var moduleDescriptions = dependencies.SelectNodes("node[@id='ModuleShortDesc']");

      foreach (XmlNode moduleDescription in moduleDescriptions)
      {
        var depInfo = new ModuleShortDesc(
          name: moduleDescription.SelectSingleNode("attribute[@id='Name']")
                                 .Attributes["value"].InnerText,
          version: moduleDescription.SelectSingleNode("attribute[@id='Version']")
                                    .Attributes["value"].InnerText,
          folder: moduleDescription.SelectSingleNode("attribute[@id='Folder']")
                                   .Attributes["value"].InnerText,
          uuid: moduleDescription.SelectSingleNode("attribute[@id='UUID']")
                                 .Attributes["value"].InnerText
        );

        metadata.Dependencies.Add(depInfo);
      }
    }

    return metadata;
  }

  /// <summary>
  /// Checks for meta.lsx and asks to create one if one doesn't exist
  /// </summary>
  /// <param name="path">The mod path</param>
  /// <param name="modName">The mod name</param>
  /// <returns>The list of meta.lsx files found</returns>
  private static List<string> CheckAndCreateMeta(string path, string modName)
  {
    // Create meta if it does not exist
    var modsPath = Path.Combine(path, "Mods");
    var pathList = Directory.GetDirectories(modsPath);

    if (pathList.Length == 0)
    {
      var newModsPath = Path.Combine(modsPath, modName);
      Directory.CreateDirectory(newModsPath);

      pathList = new string[]
      {
        newModsPath,
      };
    }

    var metaList = LsPackageEngine.GetMetalsxList(pathList);

    return metaList;
  }

  /// <summary>
  /// Generates a list of meta.lsx files, representing the mods present.
  /// </summary>
  /// <param name="pathlist">The list of directories within the \Mods folder.</param>
  /// <returns></returns>
  private static List<string> GetMetalsxList(string[] pathlist)
  {
    var metaList = new List<string>();

    foreach (var mod in pathlist)
    {
      foreach (var file in Directory.GetFiles(mod))
      {
        if (Path.GetFileName(file)
                .Equals("meta.lsx"))
        {
          metaList.Add(file);
          var modRoot = new FileInfo(file).Directory.Parent.Parent.Parent.FullName;
        }
      }
    }

    // TODO Meta-Datei erzeugen
    if (metaList.Count == 0) { }

    return metaList;
  }

  #endregion

  #region Fields

  private readonly string modPathEngine;

  private string modNameEngine;

  #endregion

  #region Constructors

  public LsPackageEngine(string modPathEngine, string modNameEngine)
  {
    this.modPathEngine = modPathEngine;
    this.modNameEngine = modNameEngine;
  }

  #endregion

  #region Properties

  /// <summary>
  /// Path to the temp directory to use.
  /// </summary>
  private string TempFolder => Path.Combine(this.modPathEngine, "BG3ModPacker");

  #endregion

  #region Methods

  public string? BuildPackage()
  {
    try
    {
      this.CreatePackage();
      this.GenerateInfoJson();
      this.GenerateZip();

      return null;
    }
    catch (Exception ex) { return ex.Message; }
    finally { this.CleanUp(); }
  }

  private void CleanUp() { Directory.Delete(path: this.TempFolder, recursive: true); }

  private void CreatePackage()
  {
    try
    {
      var options = new PackageCreationOptions();
      options.Version = PackageVersion.V18;
      options.Compression = CompressionMethod.LZ4;

      options.Priority = 0;
      var packager = new Packager();

      packager.ProgressUpdate += (status,
                                  numerator,
                                  denominator,
                                  file
                                 ) => Console.WriteLine(status);

      var targetPak = Path.Combine(this.TempFolder, this.modNameEngine + ".pak");
      var workFolder = Path.Combine(this.modPathEngine, "Work");
      
      packager.CreatePackage(packagePath: targetPak, inputPath: workFolder, options: options);
    }
    catch (Exception ex) { Console.WriteLine($"Internal error!{Environment.NewLine}{Environment.NewLine}{ex}"); }
  }

  /// <summary>
  /// Creates the metadata info.json file.
  /// </summary>
  /// <param name="metaList">The list of meta.lsx file paths.</param>
  /// <returns>Whether or not info.json was generated</returns>
  private bool GenerateInfoJson()
  {
    var dirInfo = new DirectoryInfo(this.modPathEngine);

    //GeneralHelper.WriteToConsole(Properties.Resources.DirectoryName, dirName);
    var metaFiles = dirInfo.GetFiles(searchPattern: "meta.lsx", searchOption: SearchOption.AllDirectories);

    if (metaFiles.Length != 1) { throw new Exception("excaptly one meta.lsx must exists"); }

    var metaFile = metaFiles[0].FullName;
    string md5content;

    // calculate md5 hash of .pak(s)
    using (var md5 = MD5.Create())
    {
      var paks = Directory.GetFiles(this.TempFolder);
      var pakCount = 1;

      foreach (var pak in paks)
      {
        var contentBytes = File.ReadAllBytes(pak);

        if (pakCount == paks.Length)
        {
          md5.TransformFinalBlock(inputBuffer: contentBytes, inputOffset: 0, inputCount: contentBytes.Length);
        }
        else
        {
          md5.TransformBlock(
            inputBuffer: contentBytes,
            inputOffset: 0,
            inputCount: contentBytes.Length,
            outputBuffer: contentBytes,
            outputOffset: 0
          );
        }

        pakCount++;
      }

      md5content = BitConverter.ToString(md5.Hash)
                               .Replace(oldValue: "-", newValue: "")
                               .ToLower();
    }

    var info = new InfoJson(mods: new List<MetaLsx>(), md5: md5content);

    var created = DateTime.Now;
    var metadata = LsPackageEngine.ReadMeta(meta: metaFile, created: created);

    info.Mods.Add(metadata);

    if (info.Mods.Count == 0) { return false; }

    var json = JsonConvert.SerializeObject(info);
    File.WriteAllText(path: this.TempFolder + @"\info.json", contents: json);

    return true;
  }

  /// <summary>
  /// Generates a .zip file containing the .pak(s) and info.json (contents of the temp directory)
  /// </summary>
  /// <param name="fullpath">The full path to the directory location to create the .zip.</param>
  /// <param name="name">The name to use for the .zip file.</param>
  private void GenerateZip()
  {
    var archiveFileName = $"{this.modPathEngine}\\{this.modNameEngine}.zip";

    if (File.Exists(archiveFileName)) { File.Delete(archiveFileName); }

    ZipFile.CreateFromDirectory(sourceDirectoryName: this.TempFolder, destinationArchiveFileName: archiveFileName);
  }

  #endregion

}
