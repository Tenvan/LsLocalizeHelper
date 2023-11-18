using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace LsLocalizeHelperLib.Models;

public class ModModelListBoxItem : ReactiveObject
{

  #region Constructors

  public ModModelListBoxItem(ModModel mod) => this.Mod = mod;

  #endregion

  #region Properties

  [Reactive]
  public bool IsChecked { get; set; }

  [Reactive]
  public ModModel Mod { get; set; }

  #endregion

}
