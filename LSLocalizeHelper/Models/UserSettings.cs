using LSLocalizeHelper.Views;

namespace LSLocalizeHelper.Models;

public class UserSettings
{
  #region Properties

  public string?[] LastMods { get; set; }

  public SelectionModel[] LastOriginsCurrent { get; set; }

  public SelectionModel[] LastOriginsPrevious { get; set; }

  public SelectionModel[] LastOriginsTranslated { get; set; }

  public string ModsPath { get; set; }

  public double WindowHeight { get; set; }

  public double WindowLeft { get; set; }

  public double WindowTop { get; set; }

  public double WindowWidth { get; set; }

  #endregion
}