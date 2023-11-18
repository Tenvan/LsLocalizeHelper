using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace LsLocalizeHelperLib.Models;

public class XmlFileListBoxItem : ReactiveObject
{

  #region Constructors

  public XmlFileListBoxItem(XmlFileModel fileModel) => this.FileModel = fileModel;

  #endregion

  #region Properties

  [Reactive]
  public XmlFileModel FileModel { get; set; }

  [Reactive]
  public bool IsChecked { get; set; }

  #endregion

}
