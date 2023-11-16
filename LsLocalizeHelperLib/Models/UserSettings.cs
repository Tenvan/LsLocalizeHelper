using System.Globalization;

namespace LsLocalizeHelperLib.Models;

[Serializable]
public class UserSettings
{

  #region Properties

  public string?[] LastMods { get; set; } =
    { };

  public SelectionModel[] LastOriginsCurrent { get; set; } =
    { };

  public SelectionModel[] LastOriginsPrevious { get; set; } =
    { };

  public SelectionModel[] LastOriginsTranslated { get; set; } =
    { };

  public string ModsPath { get; set; } = "";

  public double ProjectHeight { get; set; }

  public string RapidApiKey { get; set; } = "";

  public string SourceLanguage { get; set; } = "en";

  public string TargetLanguage { get; set; } = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;

  public double TranslationHeight { get; set; }

  public double WindowHeight { get; set; }

  public double WindowLeft { get; set; } = 10;

  public double WindowTop { get; set; } = 10;

  public double WindowWidth { get; set; }

  #endregion

  #region Methods

  [OnDeserialized]
  private void OnDeserialized(StreamingContext context)
  {
    this.WindowLeft = Math.Max(val1: 0, val2: this.WindowLeft);
    this.WindowTop = Math.Max(val1: 0, val2: this.WindowTop);
    this.ProjectHeight = Math.Max(val1: 100, val2: this.ProjectHeight);
    this.TranslationHeight = Math.Max(val1: 100, val2: this.TranslationHeight);
  }

  #endregion

}
