using LsLocalizeHelperLib.Helper;

namespace LsLocalizeHelperLib.Models;

public class StatusBarModel : ViewModelBase
{

  #region Fields

  private int count = 0;

  private int countDeleted = 0;

  private int countNew;

  private int countOrigins;

  private int countTranslated;

  private bool loaded;

  private bool modified;

  private string modifiedText = "Unchanged".FromResource();

  private bool notModified = true;

  #endregion

  #region Properties

  public bool NotModified => this.notModified;

  public int Count { get => this.count; set => this.SetProperty(member: ref this.count, value: value); }

  public int CountDeleted { get => this.countDeleted; set => this.SetProperty(member: ref this.countDeleted, value: value); }

  public int CountNew { get => this.countNew; set => this.SetProperty(member: ref this.countNew, value: value); }

  public int CountOrigins { get => this.countOrigins; set => this.SetProperty(member: ref this.countOrigins, value: value); }

  public int CountTranslated { get => this.countTranslated; set => this.SetProperty(member: ref this.countTranslated, value: value); }

  public bool Loaded { get => this.loaded; set => this.SetProperty(member: ref this.loaded, value: value); }

  public bool Modified
  {
    get => this.modified;

    set
    {
      this.SetProperty(member: ref this.modified, value: value);
      this.SetProperty(member: ref this.notModified, value: !value);

      this.ModifiedText = this.modified
                            ? "Changed".FromResource()
                            : "Unchanged".FromResource();
    }
  }

  public string ModifiedText { get => this.modifiedText; set => this.SetProperty(member: ref this.modifiedText, value: value); }

  #endregion

}
