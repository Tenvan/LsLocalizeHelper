using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace LsLocalizeHelperLib.Models;

public class OriginModel : ReactiveObject
{

  #region Constructors

  public OriginModel(ModModel mod,
                     XmlFileModel sourceFile,
                     string text,
                     string uuid,
                     string version
  )
  {
    this.Mod = mod;
    this.SourceFile = sourceFile;
    this.Text = text;
    this.Uuid = uuid;
    this.Version = version;
  }

  #endregion

  #region Properties

  [Reactive]
  public ModModel Mod { get; set; }

  [Reactive]
  public XmlFileModel SourceFile { get; set; }

  [Reactive]
  public string Text { get; set; }

  [Reactive]
  public string Uuid { get; set; }

  [Reactive]
  public string Version { get; set; }

  #endregion

}
