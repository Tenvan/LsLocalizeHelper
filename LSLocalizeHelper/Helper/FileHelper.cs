using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

using Alphaleonis.Win32.Filesystem;

using BrendanGrant.Helpers.FileAssociation;

using LSLib.LS;
using LSLib.LS.Enums;

using Newtonsoft.Json;

namespace Bg3LocaHelper;

public static class FileHelper
{

  public static readonly string[] ConvertableLsxResources =
  {
    ".lsf",
    ".lsb",
    ".lsbs",
    ".lsbc",
    ".lsfx",
  };

  public static readonly string[] MustRenameLsxResources =
  {
    ".lsbs",
    ".lsbc",
  };

  public static string UnpackedDataPath => $"{Directory.GetCurrentDirectory()}\\UnpackedData";

  public static string UnpackedModsPath => $"{Directory.GetCurrentDirectory()}\\UnpackedMods";

  /// <summary>
  /// List of all known file types used
  /// </summary>
  public static readonly string[] FileTypes =
  {
    ".anc",
    ".anm",
    ".ann",
    ".bin",
    ".bk2",
    ".bnk",
    ".bshd",
    ".clc",
    ".clm",
    ".cln",
    ".cur",
    ".dae",
    ".dat",
    ".data",
    ".dds",
    ".div",
    ".fbx",
    ".ffxactor",
    ".ffxbones",
    ".ffxanim",
    ".gamescript",
    ".gr2",
    ".gtp",
    ".gts",
    ".itemscript",
    ".jpg",
    ".json",
    ".khn",
    ".loca",
    ".lsb",
    ".lsbc",
    ".lsbs",
    ".lsf",
    ".lsfx",
    ".lsj",
    ".lsx",
    ".metal",
    ".ogg",
    ".osi",
    ".patch",
    ".png",
    ".psd",
    ".shd",
    ".tga",
    ".tmpl",
    ".ttf",
    ".txt",
    ".wav",
    ".wem",
    ".xaml",
    ".xml",
  };

  /// <summary>
  /// Converts the given file to .lsx type resource in-place
  /// </summary>
  /// <param name="file">The file to convert.</param>
  /// <param name="extension">The extension to convert to.</param>
  /// <param name="newPath">The new path to use.</param>
  /// <returns>The new file path.</returns>
  public static string Convert(string file, string extension, string newPath = null)
  {
    if (File.Exists(newPath))
    {
      return newPath;
    }

    var originalExtension = Path.GetExtension(file);

    var newFile = string.IsNullOrEmpty(originalExtension)
                    ? $"{file}.{extension}"
                    : file.Replace(oldValue: originalExtension, newValue: $"{originalExtension}.{extension}");

    if (File.Exists(FileHelper.GetPath(newFile)))
    {
      return newFile;
    }

    var isConvertableToLsx = FileHelper.CanConvertToLsx(file) || FileHelper.CanConvertToLsx(newPath);
    var isConvertableToXml = originalExtension.Contains("loca") && extension == "xml";
    var isConvertableToLoca = originalExtension.Contains("xml") && extension == "loca";
    var isConvertableToLsj = originalExtension.Contains("lsx") && extension == "lsj";

    string path;

    // TODO - clean up logic here; should accept directory designation rather than using GetPath

    if (string.IsNullOrEmpty(newPath))
    {
      path = FileHelper.GetPath(file);
      newPath = FileHelper.GetPath(newFile);
    }
    else
    {
      path = file;
    }

    if (!File.Exists(newPath))
    {
      if (isConvertableToLsx || isConvertableToLsj)
      {
        if (FileHelper.MustRenameLsxResources.Contains(originalExtension))
        {
          var renamedPath = Path.ChangeExtension(path: path, extension: originalExtension + ".lsf");
          File.Move(sourcePath: path, destinationPath: renamedPath);
          path = renamedPath;
        }

        var conversionParams = ResourceConversionParameters.FromGameVersion(Game.BaldursGate3);

        try
        {
          if (File.GetSize(path) == 0)
          {
            newFile = file;
          }
          else
          {
            var resource = ResourceUtils.LoadResource(
              inputPath: path,
              loadParams: ResourceLoadParameters.FromGameVersion(Game.BaldursGate3)
            );

            ResourceUtils.SaveResource(resource: resource, outputPath: newPath, conversionParams: conversionParams);
          }
        }
        catch (Exception ex)
        {
          // Larian decided to rename the .lsx to .lsbs instead of properly LSF encoding them
          // These are invalid .lsbs/.lsbc files if this error pops up
          if (!FileHelper.IsSpecialLSFSignature(ex.Message))
          {
            // GeneralHelper.WriteToConsole(Properties.Resources.FailedToConvertResource, extension, file.Replace(Directory.GetCurrentDirectory(), string.Empty), ex.Message.Replace(Directory.GetCurrentDirectory(), string.Empty));
          }

          newFile = file;
        }

        if (FileHelper.MustRenameLsxResources.Contains(originalExtension))
        {
          File.Move(sourcePath: path, destinationPath: Path.ChangeExtension(path: path, extension: ""));
        }
      }
      else if (isConvertableToXml)
      {
        using (var fs = File.Open(path: file, mode: System.IO.FileMode.Open))
        {
          try
          {
            var resource = LocaUtils.Load(stream: fs, format: LocaFormat.Loca);
            LocaUtils.Save(resource: resource, outputPath: newPath, format: LocaFormat.Xml);
          }
          catch (Exception ex)
          {
            // GeneralHelper.WriteToConsole(Properties.Resources.FailedToConvertResource, extension, file.Replace(Directory.GetCurrentDirectory(), string.Empty), ex.Message.Replace(Directory.GetCurrentDirectory(), string.Empty));
            throw new Exception(ex.Message);
          }
        }
      }
      else if (isConvertableToLoca)
      {
        using (var fs = File.Open(path: file, mode: System.IO.FileMode.Open))
        {
          try
          {
            var resource = LocaUtils.Load(stream: fs, format: LocaFormat.Xml);
            LocaUtils.Save(resource: resource, outputPath: newPath, format: LocaFormat.Loca);
          }
          catch (Exception ex)
          {
            // GeneralHelper.WriteToConsole(Properties.Resources.FailedToConvertResource, extension, file.Replace(Directory.GetCurrentDirectory(), string.Empty), ex.Message.Replace(Directory.GetCurrentDirectory(), string.Empty));
            throw new Exception(ex.Message);
          }
        }
      }
    }

    return isConvertableToLsx || isConvertableToXml || isConvertableToLoca || isConvertableToLsj
             ? newFile
             : file;
  }

  /// <summary>
  /// Checks to see if the file is convertable to lsx.
  /// </summary>
  /// <param name="file">The file to check.</param>
  /// <returns>Whether or not the file is convertable.</returns>
  public static bool CanConvertToLsx(string file)
  {
    if (string.IsNullOrEmpty(file))
    {
      return false;
    }

    var extension = Path.GetExtension(file);

    return FileHelper.ConvertableLsxResources.Contains(extension);
  }

  /// <summary>
  /// Checks if file has specific LSF signature indicating that it is actually LSX instead of the extension they have, ie lsbc
  /// </summary>
  /// <param name="message">The error message to check</param>
  /// <returns>Whether or not the file is actually lsx</returns>
  public static bool IsSpecialLSFSignature(string message) =>
    message == "Invalid LSF signature; expected 464F534C, got 200A0D7B";

  /// <summary>
  /// Checks if the file is a convertable file
  /// </summary>
  /// <param name="file">The file to check</param>
  /// <returns>Whether or not the file is convertable</returns>
  public static bool IsConvertable(string file)
  {
    var originalExtension = Path.GetExtension(file);
    var isConvertableToLsx = FileHelper.CanConvertToLsx(file);
    var isConvertableToXml = originalExtension.Contains("loca");

    return isConvertableToLsx || isConvertableToXml;
  }

  /// <summary>
  /// Gets a list of files in a directory.
  /// </summary>
  /// <param name="directory">The directory root to search.</param>
  /// <returns>A list of files in the directory.</returns>
  public static List<string> DirectorySearch(string directory)
  {
    if (!Directory.Exists(directory))
    {
      Directory.CreateDirectory(directory);
    }

    var fileList = FileHelper.RecursiveFileSearch(directory);

    if (fileList.Count == 0)
    {
      // GeneralHelper.WriteToConsole(Properties.Resources.NoFilesFound);
    }

    return fileList;
  }

  /// <summary>
  /// Recursively searches for all files within the given directory.
  /// </summary>
  /// <param name="directory">The directory root to search.</param>
  /// <returns>A list of files in the directory.</returns>
  private static List<string> RecursiveFileSearch(string directory)
  {
    var fileList = new List<string>();

    foreach (var dir in Directory.GetDirectories(directory))
    {
      try
      {
        foreach (var file in Directory.GetFiles(dir))
        {
          fileList.Add(file);
        }

        fileList.AddRange(FileHelper.RecursiveFileSearch(dir));
      }
      catch (Exception ex)
      {
        // GeneralHelper.WriteToConsole(Properties.Resources.FailedToReadDirectory, directory);
        throw new Exception(ex.Message);
      }
    }

    return fileList;
  }

  /// <summary>
  /// Gets the complete list of extensions of the files in the given file list.
  /// </summary>
  /// <param name="fileList">The file list to scan.</param>
  /// <returns>The list of file extensions.</returns>
  public static List<string> GetFileExtensions(List<string> fileList)
  {
    var extensions = new List<string>();

    foreach (var file in fileList)
    {
      var extension = Path.GetExtension(file);

      if (!extensions.Contains(extension))
      {
        extensions.Add(extension);
      }
    }

    return extensions;
  }

  /// <summary>
  /// Determines if the file path is a .GR2 mesh.
  /// </summary>
  /// <param name="path">The file path.</param>
  /// <returns>Whether or not the file is a .GR2 file.</returns>
  public static bool IsGR2(string path) =>
    Path.GetExtension(path)
        .ToLower()
        .Contains(".gr2");

  /// <summary>
  /// Determines if the file path is a .GTP virtual texture file.
  /// </summary>
  /// <param name="path">The file path.</param>
  /// <returns>Whether or not the file is a .GTP file.</returns>
  public static bool IsGTP(string path) =>
    Path.GetExtension(path)
        .ToLower()
    == ".gtp";

  /// <summary>
  /// Gets a standard path for files.
  /// </summary>
  /// <param name="file">The file to generate a path for.</param>
  /// <returns>The full file path.</returns>
  public static string GetPath(string file)
  {
    if (!string.IsNullOrEmpty(file)
        && (file.Contains(FileHelper.UnpackedDataPath) || file.Contains(Path.GetTempPath())))
    {
      return file;
    }

    return $"{FileHelper.UnpackedDataPath}\\{file}";
  }

  /// <summary>
  /// Opens the given file path in the default program.
  /// </summary>
  /// <param name="file">The file to open.</param>
  /// <param name="line">The matching file line</param>
  public static void OpenFile(string file, int? line = null)
  {
    var path = FileHelper.GetPath(file);

    if (File.Exists(@"\\?\" + path))
    {
      try
      {
        if (FileHelper.IsGR2(path))
        {
          var dae = Path.ChangeExtension(path: path, extension: ".dae");

          // determine if you can determine if there is a default program
          var fileAssociation = new FileAssociationInfo(".dae");

          if (fileAssociation.Exists)
          {
            Process.Start(dae);
          }

          // open folder
          Process.Start(fileName: "explorer.exe", arguments: $"/select,{dae}");
        }
        else
        {
          var exe = FileHelper.FindExecutable(path);

          if (exe.Contains("notepad++")
              && line.HasValue)
          {
            var npp = new Process
            {
              StartInfo =
              {
                FileName = exe,
                Arguments = $"{file} -n{line}",
              },
            };

            npp.Start();
          }
          else
          {
            Process.Start(path);
          }
        }
      }
      catch (Exception ex)
      {
        // GeneralHelper.WriteToConsole(ex.Message);
        throw new Exception(ex.Message);
      }
    }
    else
    {
      // GeneralHelper.WriteToConsole(Properties.Resources.FileNoExist, path);
    }
  }

  /// <summary>
  /// Serializes an object and saves it as a json file.
  /// </summary>
  /// <param name="serialObject">The object to serialize</param>
  /// <param name="filename">The filename to use, without extension</param>
  public static void SerializeObject(object serialObject, string filename)
  {
    var file = $"Cache/{filename}.json";

    if (!File.Exists(file))
    {
      var listObject = serialObject as IList;

      if (listObject != null
          && listObject.Count == 0)
      {
        return;
      }

      var dictObject = serialObject as IDictionary;

      if (dictObject != null
          && dictObject.Count == 0)
      {
        return;
      }

      // GeneralHelper.WriteToConsole(Properties.Resources.CachingFile, filename);
      System.IO.TextWriter writer = null;

      try
      {
        if (!Directory.Exists("Cache"))
        {
          Directory.CreateDirectory("Cache");
        }

        var contentsToWriteToFile = JsonConvert.SerializeObject(
          value: serialObject,
          formatting: Formatting.Indented,
          settings: new JsonSerializerSettings
          {
            NullValueHandling = NullValueHandling.Ignore,
          }
        );

        writer = new System.IO.StreamWriter(path: file, append: false);
        writer.Write(contentsToWriteToFile);
      }
      finally
      {
        if (writer != null)
        {
          writer.Close();
        }
      }
    }
  }

  /// <summary>
  /// Deserializes an object from a saved json file.
  /// </summary>
  /// <typeparam name="T">The object type.</typeparam>
  /// <param name="filename">The filename, without extension.</param>
  /// <returns>The deserialized object, or null if not found.</returns>
  public static T DeserializeObject<T>(string filename)
  {
    var file = $"Cache/{filename}.json";
    var objectOut = default(T);

    if (File.Exists(file))
    {
      // GeneralHelper.WriteToConsole(Properties.Resources.LoadingCachedFile, filename);
      System.IO.TextReader reader = null;

      try
      {
        var stream = File.OpenText(file);
        reader = stream;
        var fileContents = reader.ReadToEnd();

        return JsonConvert.DeserializeObject<T>(fileContents);
      }
      finally
      {
        if (reader != null)
        {
          reader.Close();
        }
      }
    }

    return objectOut;
  }

  /// <summary>
  /// Determines if the file is valid xml
  /// </summary>
  /// <param name="filepath">The file path to test</param>
  /// <returns>Whether the file is valid or not</returns>
  public static bool TryParseXml(string filepath)
  {
    try
    {
      System.Xml.Linq.XDocument.Load(filepath);

      return true;
    }
    catch
    {
      return false;
    }
  }

  /// <summary>
  /// Loads the file template text from the assembly
  /// </summary>
  /// <param name="templateName">The file template name</param>
  /// <returns>The file template file content string</returns>
  public static string LoadFileTemplate(string templateName)
  {
    using (var stream = Assembly.GetExecutingAssembly()
                                .GetManifestResourceStream($"Bg3LocaHelper.FileTemplates.{templateName}"))
    using (var reader = new System.IO.StreamReader(stream))
    {
      return reader.ReadToEnd();
    }
  }

  /// <summary>
  /// Checks if the appliation can write to the given directory
  /// </summary>
  /// <param name="dirPath">The directory to check</param>
  /// <returns>Whether or not the application can successfully write to the given directory</returns>
  public static bool IsDirectoryWritable(string dirPath)
  {
    try
    {
      using (var fs = File.Create(
               path: Path.Combine(dirPath, Path.GetRandomFileName()),
               bufferSize: 1,
               options: System.IO.FileOptions.DeleteOnClose
             )) { }

      return true;
    }
    catch
    {
      return false;
    }
  }

  #region Process Finder

  [DllImport("shell32.dll")]
  private static extern long FindExecutable(string lpFile, string lpDirectory, [Out] StringBuilder lpResult);

  /// <summary>
  /// Checks if the file has an associated program that will open it
  /// </summary>
  /// <param name="path">The file path</param>
  /// <returns>Whether or not the file has a default program association</returns>
  public static bool HasExecutable(string path)
  {
    var executable = FileHelper.FindExecutable(path);

    return !string.IsNullOrEmpty(executable);
  }

  /// <summary>
  /// Gets the name of the executable that is associated with the file
  /// </summary>
  /// <param name="path">The file path</param>
  /// <returns>The executable name</returns>
  public static string FindExecutable(string path)
  {
    var executable = new StringBuilder(1024);
    FileHelper.FindExecutable(lpFile: path, lpDirectory: string.Empty, lpResult: executable);

    return executable.ToString();
  }

  #endregion

}
