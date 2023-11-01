using LSLocalizeHelper.Helper;

namespace LSLocalizeHelper.Models;

public class StatusBarModel : ViewModelBase
{

  #region Fields

  private int count;

  private int countDeleted;

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

  public int Count { get => this.count; set => this.SetProperty(ref this.count, value); }

  public int CountDeleted { get => this.countDeleted; set => this.SetProperty(ref this.countDeleted, value); }

  public int CountNew { get => this.countNew; set => this.SetProperty(ref this.countNew, value); }

  public int CountOrigins { get => this.countOrigins; set => this.SetProperty(ref this.countOrigins, value); }

  public int CountTranslated { get => this.countTranslated; set => this.SetProperty(ref this.countTranslated, value); }

  public bool Loaded { get => this.loaded; set => this.SetProperty(ref this.loaded, value); }

  public bool Modified
  {
    get => this.modified;

    set
    {
      this.SetProperty(ref this.modified, value);
      this.SetProperty(ref this.notModified, !value);

      this.ModifiedText = this.modified
                            ? "Changed".FromResource()
                            : "Unchanged".FromResource();
    }
  }

  public string ModifiedText { get => this.modifiedText; set => this.SetProperty(ref this.modifiedText, value); }

  #endregion

}
