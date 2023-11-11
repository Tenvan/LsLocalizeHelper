using System.ComponentModel;
using System.Runtime.CompilerServices;

using LsLocalizeHelperLib.Enums;

namespace LsLocalizeHelperLib.Models;

public class DataRowModel : INotifyPropertyChanged
{

  #region Fields

  private DatSetFlag flag;

  private TranslationStatus originStatus;

  private TranslationStatus status;

  private string text;

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

  public DatSetFlag Flag
  {
    get => this.flag;

    set
    {
      if (value == this.flag) { return; }

      this.flag = value;
      this.OnPropertyChanged();
    }
  }

  public ModModel Mod { get; set; }

  public string? Origin { get; set; }

  public TranslationStatus OriginStatus
  {
    get => this.originStatus;

    set
    {
      if (value == this.originStatus) { return; }

      this.originStatus = value;
      this.OnPropertyChanged();
    }
  }

  public string? Previous { get; set; }

  public XmlFileModel SourceFile { get; set; }

  public TranslationStatus Status { get => this.status; set => this.SetField(field: ref this.status, value: value); }

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
    this.PropertyChanged?.Invoke(sender: this, e: new PropertyChangedEventArgs(propertyName));
  }

  protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
  {
    if (EqualityComparer<T>.Default.Equals(x: field, y: value)) return false;

    field = value;
    this.OnPropertyChanged(propertyName);

    return true;
  }

  #endregion

  #region All Other Members

  public event PropertyChangedEventHandler? PropertyChanged;

  #endregion

}
