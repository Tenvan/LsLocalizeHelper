using System.Collections.Generic;

namespace LsLocalizeHelperLib.Models;

internal class InfoJson
{

  public InfoJson(List<MetaLsx> mods, string md5)
  {
    this.Mods = mods;
    this.MD5 = md5;
  }

  public List<MetaLsx> Mods { get; set; }

  public string MD5 { get; set; }

}
