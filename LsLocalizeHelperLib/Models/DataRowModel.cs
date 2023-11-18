using LsLocalizeHelperLib.Enums;

using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace LsLocalizeHelperLib.Models;

public class DataRowModel : ReactiveObject
{

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
    this.Text = text;
    this.Flag = flag;
    this.Mod = mod;
    this.SourceFile = sourceFile;
    this.Status = status;
    this.Uuid = uuid;
    this.Version = version;
  }

  #endregion

  #region Properties

  [Reactive]
  public DatSetFlag Flag  { get; set; }

  [Reactive]
  public ModModel Mod { get; set; }

  [Reactive]
  public string? Origin { get; set; }

  [Reactive]
  public TranslationStatus OriginStatus { get; set; }

  [Reactive]
  public string? Previous { get; set; }

  public XmlFileModel SourceFile { get; set; }

  [Reactive]
  public TranslationStatus Status { get; set; }

  [Reactive]
  public string Text { get; set; }

  [Reactive]
  public string Uuid { get; set; }

  [Reactive]
  public string Version { get; set; }

  #endregion

}
