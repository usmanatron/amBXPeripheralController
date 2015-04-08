using aPC.Common;
using System.Xml.Serialization;

namespace aPC.SceneMigrator.EntitiesV1
{
  public class Fan : IComponent
  {
    [XmlElement]
    public float Intensity;

    public eComponentType ComponentType()
    {
      return eComponentType.Fan;
    }
  }
}