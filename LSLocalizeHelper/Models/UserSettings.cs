using System;
using System.Runtime.Serialization;
using System.Windows;

using LSLocalizeHelper.Views;

namespace LSLocalizeHelper.Models;

[Serializable]
public class UserSettings
{

  #region Properties

  [OnDeserialized]
  private void OnDeserialized(StreamingContext context)
  {
    this.WindowLeft = Math.Max(val1: 0, val2: this.WindowLeft);
    this.WindowTop = Math.Max(val1: 0, val2: this.WindowTop);
    this.ProjectHeight = Math.Max(100, this.ProjectHeight);
    this.TranslationHeight = Math.Max(100, this.TranslationHeight);
  }

  public string?[] LastMods { get; set; } =
    { };

  public SelectionModel[] LastOriginsCurrent { get; set; } =
    { };

  public SelectionModel[] LastOriginsPrevious { get; set; } =
    { };

  public SelectionModel[] LastOriginsTranslated { get; set; } =
    { };

  public string ModsPath { get; set; } = "";

  public double WindowHeight { get; set; }

  public double WindowLeft { get; set; } = 10;

  public double WindowTop { get; set; } = 10;

  public double WindowWidth { get; set; }

  public double ProjectHeight { get; set; }

  public double TranslationHeight { get; set; }

  #endregion

}
