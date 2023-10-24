using System.Collections.ObjectModel;

using LSLocalizeHelper.Models;
using LSLocalizeHelper.Services;

namespace LSLocalizeHelper.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{

  #region Fields

  private Bg3ModsService bg3ModsService = new();

  private XmlFilesService XmlFilesService = new();

  #endregion

  #region Properties

  public ObservableCollection<XmlFileModel> OriginCurrentFileItems { get; set; } = new();

  public ObservableCollection<XmlFileModel> OriginPreviousFileItems { get; set; } = new();

  public ObservableCollection<ModModel> ProjectItems { get; set; } = new();

  public ObservableCollection<XmlFileModel> TranslatedFileItems { get; set; } = new();

  #endregion

}
