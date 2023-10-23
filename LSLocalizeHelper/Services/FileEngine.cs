using System;
using System.Windows.Forms;

using Alphaleonis.Win32.Filesystem;

using LSLocalizeHelper.Views;

namespace Bg3LocaHelper;

internal class FileEngine
{
  public string ModsPath { get; }

  public string ModeName { get; }

  public FileEngine(
    string modsPath,
    string modeName
  )
  {
    this.ModsPath = modsPath;
    this.ModeName = modeName;
  }

  public bool CopyFile(
    string? fileName
  )
  {
    var newFileName = Path.GetFileNameWithoutExtension(fileName);
    var form        = new InputBoxForm("Set new file name", newFileName);
    var result      = form.ShowDialog();

    if (result != true) return false;

    var inputText      = form.InputText;
    var fullPathSource = Path.Combine(this.ModsPath, this.ModeName, "Work", fileName);
    newFileName = Path.Combine(Path.GetDirectoryName(fileName), $"{inputText}{Path.GetExtension(fileName)}");
    var fullPathTarget = Path.Combine(this.ModsPath, this.ModeName, "Work", newFileName);

    if (fullPathSource.Equals(fullPathTarget))
    {
      MessageBox.Show("The same name for both files is not allowed.");

      return false;
    }

    File.Copy(fullPathSource, fullPathTarget);
    MessageBox.Show($"File successfully copied:\n{fullPathSource}\nto:\n{fullPathTarget}");

    return true;
  }

  public bool DeleteFile(
    string fileName
  )
  {
    var fullPathSource = Path.Combine(this.ModsPath, this.ModeName, "Work", fileName);
    var form           = new ConfirmForm($"Sure to delete file\n{fullPathSource} ?");
    var result         = form.ShowDialog();

    if (result != true) return false;

    File.Delete(fullPathSource);
    MessageBox.Show($"File successfully deleted:\n{fullPathSource}");

    return true;
  }

  public bool RenameFile(
    string fileName
  )
  {
    var newFileName = Path.GetFileNameWithoutExtension(fileName);
    var form        = new InputBoxForm("Set new file name", newFileName);
    var result      = form.ShowDialog();

    if (result != true) return false;

    var inputText      = form.InputText;
    var fullPathSource = Path.Combine(this.ModsPath, this.ModeName, "Work", fileName);
    newFileName = Path.Combine(Path.GetDirectoryName(fileName), $"{inputText}{Path.GetExtension(fileName)}");
    var fullPathTarget = Path.Combine(this.ModsPath, this.ModeName, "Work", newFileName);

    if (fullPathSource.Equals(fullPathTarget))
    {
      MessageBox.Show("The same name for both files is not allowed.");

      return false;
    }

    File.Move(fullPathSource, fullPathTarget);
    MessageBox.Show($"File successfully renamed from:\n{fullPathSource}\nto:\n{fullPathTarget}");

    return true;
  }

  public bool CopyFileToNewLanguage(
    string fileName
  )
  {
    var       newFileName = Path.GetFileNameWithoutExtension(fileName);
    var form        = new InputBoxForm("Input language name", "German");
    var       result      = form.ShowDialog();

    if (result == true) return true;

    var inputText      = form.InputText;
    var fullPathSource = Path.Combine(this.ModsPath, this.ModeName, "Work", fileName);
    newFileName = Path.Combine(inputText, $"{newFileName}{Path.GetExtension(fileName)}");
    var fullPathTarget = Path.Combine(this.ModsPath, this.ModeName, "Work", "Localization", newFileName);

    if (fullPathSource.Equals(fullPathTarget))
    {
      MessageBox.Show("The same name for both files is not allowed.");

      return false;
    }

    Directory.CreateDirectory(Path.GetDirectoryName(fullPathTarget));
    File.Copy(fullPathSource, fullPathTarget);
    MessageBox.Show($"File successfully copied:\n{fullPathSource}\nto:\n{fullPathTarget}");

    return true;
  }
}