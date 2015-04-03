using System.Collections.Generic;
using System.Xml.Serialization;

namespace aPC.Common.Entities
{
  public class FanSection : IComponentSection
  {
    [XmlArray]
    [XmlArrayItem("Fan")]
    public List<Fan> Fans;

    public IEnumerable<DirectionalComponent> GetComponents()
    {
      foreach (var fan in Fans)
      {
        yield return (DirectionalComponent)fan;
      }
    }
  }
}