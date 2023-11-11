namespace LsLocalizeHelperLib.Models;

public class ModModelListBoxItem
{

  #region Constructors

  public ModModelListBoxItem(ModModel mod) => this.Mod = mod;

  #endregion

  #region Properties

  public bool IsChecked { get; set; }

  public ModModel Mod { get; set; }

  #endregion

}
