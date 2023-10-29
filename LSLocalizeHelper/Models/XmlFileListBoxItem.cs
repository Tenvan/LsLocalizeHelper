namespace LSLocalizeHelper.Models;

public class XmlFileListBoxItem
{

  #region Constructors

  public XmlFileListBoxItem(XmlFileModel fileModel) => this.FileModel = fileModel;

  #endregion

  #region Properties

  public XmlFileModel FileModel { get; set; }

  public bool IsChecked { get; set; }

  #endregion

}
