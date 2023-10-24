using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.Xml;

using Alphaleonis.Win32.Filesystem;

using bg3_modders_multitool.Models;

using LSLib.LS;
using LSLib.LS.Enums;

using LSLocalizeHelper.Models;

using Newtonsoft.Json;

using SearchOption = System.IO.SearchOption;

namespace LSLocalizeHelper.Services;

public class Bg3PackageEngine
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
        var depInfo = new ModuleShortDesc
        {
          Name = moduleDescription.SelectSingleNode("attribute[@id='Name']")
                                  .Attributes["value"].InnerText,
          Version = moduleDescription.SelectSingleNode("attribute[@id='Version']")
                                     .Attributes["value"].InnerText,
          Folder = moduleDescription.SelectSingleNode("attribute[@id='Folder']")
                                    .Attributes["value"].InnerText,
          UUID = moduleDescription.SelectSingleNode("attribute[@id='UUID']")
                                  .Attributes["value"].InnerText,
        };

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

    var metaList = Bg3PackageEngine.GetMetalsxList(pathList);

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

  public Bg3PackageEngine(string modPathEngine, string modNameEngine)
  {
    this.modPathEngine = modPathEngine;
    this.modNameEngine = modNameEngine;
  }

  #endregion

  #region Properties

  /// <summary>
  /// Path to the temp directory to use.
  /// </summary>
  private string TempFolder =>
    Path.Combine(
      Directory.GetParent(this.modPathEngine)
               .FullName,
      "BG3ModPacker"
    );

  #endregion

  #region Methods

  public void BuildPackage()
  {
    this.CreatePackage();
    this.GenerateInfoJson();
    this.GenerateZip();
    this.CleanUp();
  }

  private void CleanUp()
  {
    Directory.Delete(path: this.TempFolder, recursive: true);
  }

  private void CreatePackage()
  {
    try
    {
      var options = new PackageCreationOptions();
      options.Version = PackageVersion.V18;
      options.Compression = CompressionMethod.LZ4;

      // options.FastCompression = false;

      // options.Flags |= PackageFlags.Solid;
      // options.Flags |= PackageFlags.AllowMemoryMapping;
      // options.Flags |= PackageFlags.Preload;
      options.Priority = 0;
      var packager = new Packager();

      packager.ProgressUpdate += (status,
                                  numerator,
                                  denominator,
                                  file
                                 ) => Debug.WriteLine(status);

      var targetPak = Path.Combine(this.TempFolder, this.modNameEngine + ".pak");

      packager.CreatePackage(packagePath: targetPak, inputPath: this.modPathEngine, options: options);
    }
    catch (Exception ex)
    {
      MessageBox.Show(
        text: $"Internal error!{Environment.NewLine}{Environment.NewLine}{ex}",
        caption: "Package Build Failed",
        buttons: MessageBoxButtons.OK,
        icon: MessageBoxIcon.Error
      );
    }
  }

  private string modContentPath => Path.Combine(this.modPathEngine, this.modNameEngine);

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

    if (metaFiles.Length != 1)
    {
      throw new Exception("excaptly one meta.lsx must exists");
    }

    var metaFile = metaFiles[0].FullName;

    var info = new InfoJson
    {
      Mods = new List<MetaLsx>(),
    };

    var created = DateTime.Now;
    var metadata = Bg3PackageEngine.ReadMeta(meta: metaFile, created: created);

    info.Mods.Add(metadata);

    if (info.Mods.Count == 0)
    {
      return false;
    }

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

      info.MD5 = BitConverter.ToString(md5.Hash)
                             .Replace(oldValue: "-", newValue: "")
                             .ToLower();
    }

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
    // save zip next to folder that was dropped
    var parentDir = Directory.GetParent(this.modPathEngine)
                             .FullName;

    var archiveFileName = $"{parentDir}\\{this.modNameEngine}.zip";

    if (File.Exists(archiveFileName))
    {
      File.Delete(archiveFileName);
    }

    ZipFile.CreateFromDirectory(sourceDirectoryName: this.TempFolder, destinationArchiveFileName: archiveFileName);
  }

  #endregion

}
