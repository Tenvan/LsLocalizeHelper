using ReactiveUI;

namespace LsLocalizeHelperLib.Models;

public class SelectionModel : ReactiveObject
{

  #region Fields

  public string ModName { get; }

  public string Name { get; }

  #endregion

  #region Constructors

  public SelectionModel(string modName, string name)
  {
    this.ModName = modName;
    this.Name = name;
  }

  #endregion

}
