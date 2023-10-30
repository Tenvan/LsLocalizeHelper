using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using LSLocalizeHelper.Enums;

namespace LSLocalizeHelper.Models;

public class DataRowModel : INotifyPropertyChanged
{

  #region Fields

  private TranslationStatus status_;

  private string text;

  private TranslationStatus status;

  #endregion

  #region Constructors

  public DataRowModel(string text,
                      DatSetFlag flag,
                      ModModel mod,
                      XmlFileModel sourceFile,
                      TranslationStatus status,
                      string uuid,
                      string version
  )
  {
    this.text = text;
    this.Flag = flag;
    this.Mod = mod;
    this.SourceFile = sourceFile;
    this.Status = status;
    this.Uuid = uuid;
    this.Version = version;
  }

  #endregion

  #region Properties

  public DatSetFlag Flag { get; set; }

  public ModModel Mod { get; set; }

  public string? Origin { get; set; }

  public string? Previous { get; set; }

  public XmlFileModel SourceFile { get; set; }

  public TranslationStatus Status { get => this.status; set => this.SetField(ref this.status, value); }

  public string Text
  {
    get => this.text;

    set
    {
      if (value == this.text) { return; }

      this.text = value;
      this.OnPropertyChanged();
    }
  }

  public string Uuid { get; set; }

  public string Version { get; set; }

  #endregion

  #region Methods

  protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
  {
    this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
  }

  protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
  {
    if (EqualityComparer<T>.Default.Equals(field, value)) return false;

    field = value;
    this.OnPropertyChanged(propertyName);

    return true;
  }

  #endregion

  #region All Other Members

  public event PropertyChangedEventHandler? PropertyChanged;

  #endregion

}
