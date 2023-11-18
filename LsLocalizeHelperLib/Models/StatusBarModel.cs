using LsLocalizeHelperLib.Helper;

using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace LsLocalizeHelperLib.Models;

public class StatusBarModel : ReactiveObject
{

  #region Fields

  private bool modified;

  private string modifiedText = "Unchanged".FromResource();

  private bool notModified = true;

  #endregion

  #region Properties

  public bool NotModified => this.notModified;

  [Reactive]
  public int Count { get; set; }

  [Reactive]
  public int CountDeleted { get; set; }

  [Reactive]
  public int CountNew { get; set; }

  [Reactive]
  public int CountOrigins { get; set; }

  [Reactive]
  public int CountTranslated { get; set; }

  [Reactive]
  public bool Loaded { get; set; }

  [Reactive]
  public bool Modified
  {
    get => this.modified;

    set
    {
      this.modified = value;
      this.ModifiedText = this.modified ? "Changed".FromResource() : "Unchanged".FromResource();
    }
  }

  [Reactive]
  public string ModifiedText { get; set; } = "Unchanged".FromResource();

  #endregion

}
