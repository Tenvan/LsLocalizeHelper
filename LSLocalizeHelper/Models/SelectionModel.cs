namespace LSLocalizeHelper.Models;

public class SelectionModel
{

  #region Fields

  public string ModName;

  public string Name;

  #endregion

  #region Constructors

  public SelectionModel(string modName, string name)
  {
    this.ModName = modName;
    this.Name = name;
  }

  #endregion

}
