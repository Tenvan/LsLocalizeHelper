namespace LSLocalizeHelper.Models;

public class OriginModel
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

  public ModModel Mod { get; set; }

  public XmlFileModel SourceFile { get; set; }

  public string Text { get; set; }

  public string Uuid { get; set; }

  public string Version { get; set; }

  #endregion

}
