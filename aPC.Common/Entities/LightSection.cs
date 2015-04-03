using System.Collections.Generic;
using System.Xml.Serialization;

namespace aPC.Common.Entities
{
  public class LightSection : IComponentSection
  {
    [XmlArray]
    [XmlArrayItem("Light")]
    public List<Light> Lights;

    public IEnumerable<DirectionalComponent> GetComponents()
    {
      foreach (var light in Lights)
      {
        yield return (DirectionalComponent)light;
      }
    }
  }
}