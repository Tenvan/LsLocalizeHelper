using System;
using System.Windows.Forms;

using Alphaleonis.Win32.Filesystem;

namespace Bg3LocaHelper;

internal class FileEngine
{

  public string ModsPath { get; }

  public string ModeName { get; }

  public FileEngine(string modsPath, string modeName)
  {
    this.ModsPath = modsPath;
    this.ModeName = modeName;
  }

  public bool CopyFile(string? fileName)
  {
    var newFileName = Path.GetFileNameWithoutExtension(fileName);
    using var form = new InputBoxForm(title: "Set new file name", textContent: newFileName);
    var result = form.ShowDialog();

    if (result != DialogResult.OK)
    {
      return true;
    }

    var inputText = form.InputText;

    var fullPathSource = Path.Combine(
      this.ModsPath,
      this.ModeName,
      "Work",
      fileName
    );

    newFileName = Path.Combine(Path.GetDirectoryName(fileName), $"{inputText}{Path.GetExtension(fileName)}");

    var fullPathTarget = Path.Combine(
      this.ModsPath,
      this.ModeName,
      "Work",
      newFileName
    );

    if (fullPathSource.Equals(fullPathTarget))
    {
      MessageBox.Show("The same name for both files is not allowed.");

      return false;
    }

    File.Copy(sourcePath: fullPathSource, destinationPath: fullPathTarget);
    MessageBox.Show($"File successfully copied:\n{fullPathSource}\nto:\n{fullPathTarget}");

    return true;
  }

  public bool DeleteFile(string fileName)
  {
    var fullPathSource = Path.Combine(
      this.ModsPath,
      this.ModeName,
      "Work",
      fileName
    );

    using var form = new ConfirmForm($"Sure to delete file\n{fullPathSource} ?");
    var result = form.ShowDialog();

    if (result != DialogResult.OK
        && result != DialogResult.Yes)
    {
      return false;
    }

    File.Delete(fullPathSource);
    MessageBox.Show($"File successfully deleted:\n{fullPathSource}");

    return true;
  }

  public bool RenameFile(string fileName)
  {
    var newFileName = Path.GetFileNameWithoutExtension(fileName);
    using var form = new InputBoxForm(title: "Set new file name", textContent: newFileName);
    var result = form.ShowDialog();

    if (result != DialogResult.OK)
    {
      return true;
    }

    var inputText = form.InputText;

    var fullPathSource = Path.Combine(
      this.ModsPath,
      this.ModeName,
      "Work",
      fileName
    );

    newFileName = Path.Combine(Path.GetDirectoryName(fileName), $"{inputText}{Path.GetExtension(fileName)}");

    var fullPathTarget = Path.Combine(
      this.ModsPath,
      this.ModeName,
      "Work",
      newFileName
    );

    if (fullPathSource.Equals(fullPathTarget))
    {
      MessageBox.Show("The same name for both files is not allowed.");

      return false;
    }

    File.Move(sourcePath: fullPathSource, destinationPath: fullPathTarget);
    MessageBox.Show($"File successfully renamed from:\n{fullPathSource}\nto:\n{fullPathTarget}");

    return true;
  }

  public bool CopyFileToNewLanguage(string fileName)
  {
    var newFileName = Path.GetFileNameWithoutExtension(fileName);
    using var form = new InputBoxForm(title: "Input language name", textContent: "German");
    var result = form.ShowDialog();

    if (result != DialogResult.OK)
    {
      return true;
    }

    var inputText = form.InputText;

    var fullPathSource = Path.Combine(
      this.ModsPath,
      this.ModeName,
      "Work",
      fileName
    );

    newFileName = Path.Combine(inputText, $"{newFileName}{Path.GetExtension(fileName)}");

    var fullPathTarget = Path.Combine(
      this.ModsPath,
      this.ModeName,
      "Work",
      "Localization",
      newFileName
    );

    if (fullPathSource.Equals(fullPathTarget))
    {
      MessageBox.Show("The same name for both files is not allowed.");

      return false;
    }

    Directory.CreateDirectory(Path.GetDirectoryName(fullPathTarget));
    File.Copy(sourcePath: fullPathSource, destinationPath: fullPathTarget);
    MessageBox.Show($"File successfully copied:\n{fullPathSource}\nto:\n{fullPathTarget}");

    return true;
  }

}
