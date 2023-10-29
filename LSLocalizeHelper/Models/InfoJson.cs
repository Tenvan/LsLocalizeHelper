/// <summary>
/// The structure of the info.json file
/// </summary>

namespace bg3_modders_multitool.Models;

using System.Collections.Generic;

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
