using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using LSLocalizeHelper.Enums;

namespace LSLocalizeHelper.Models;

public class DataRowModel : INotifyPropertyChanged
{

  private string text;

  #region Properties

  public DatSetFlag Flag { get; set; }

  public ModModel Mod { get; set; }

  public string Origin { get; set; }

  public string Previous { get; set; }

  public XmlFileModel SourceFile { get; set; }

  public TranslationStatus Status { get; set; }

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

  public event PropertyChangedEventHandler? PropertyChanged;

  protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
  {
    this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
  }

  protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
  {
    if (EqualityComparer<T>.Default.Equals(field, value)) return false;

    field = value;
    OnPropertyChanged(propertyName);

    return true;
  }

}
