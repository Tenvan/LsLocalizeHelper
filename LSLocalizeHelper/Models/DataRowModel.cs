using LSLocalizeHelper.Enums;

namespace LSLocalizeHelper.Models;

public class DataRowModel
{

  #region Properties

  public ModModel Mod { get; set; }

  public string Origin { get; set; }

  public XmlFileModel Source { get; set; }

  public TranslationStatus Status { get; set; }

  public string Text { get; set; }

  public string Uuid { get; set; }

  public string Version { get; set; }

  #endregion

}
