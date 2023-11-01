using LSLocalizeHelper.Helper;

namespace LSLocalizeHelper.Models;

public class StatusBarModel : ViewModelBase
{

  #region Fields

  private int count;

  private int countDeleted;

  private int countNew;

  private bool loaded;

  private bool modified;

  private int countTranslated;

  private int countOrigins;

  private string modifiedText = "Unchanged".FromResource();

  #endregion

  #region Properties

  public string ModifiedText { get => this.modifiedText; set => this.SetProperty(ref this.modifiedText, value); }

  public int Count { get => this.count; set => this.SetProperty(ref this.count, value); }

  public int CountTranslated { get => this.countTranslated; set => this.SetProperty(ref this.countTranslated, value); }

  public int CountDeleted { get => this.countDeleted; set => this.SetProperty(ref this.countDeleted, value); }

  public int CountNew { get => this.countNew; set => this.SetProperty(ref this.countNew, value); }

  public bool Loaded { get => this.loaded; set => this.SetProperty(ref this.loaded, value); }

  public bool Modified
  {
    get => this.modified;

    set
    {
      this.SetProperty(ref this.modified, value);

      this.ModifiedText = this.modified
                            ? "Changed".FromResource()
                            : "Unchanged".FromResource();
    }
  }

  public int CountOrigins { get => this.countOrigins; set => this.SetProperty(ref this.countOrigins, value); }

  #endregion

}
