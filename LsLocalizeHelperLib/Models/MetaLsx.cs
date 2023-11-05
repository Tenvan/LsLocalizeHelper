namespace LsLocalizeHelperLib.Models;

public class MetaLsx
{

  public string Author { get; set; }

  public string Name { get; set; }

  public string Folder { get; set; }

  public string Version { get; set; }

  public string Description { get; set; }

  public string UUID { get; set; }

  public DateTime? Created { get; set; }

  public List<ModuleShortDesc> Dependencies { get; set; }

  public string Group { get; set; }

}
