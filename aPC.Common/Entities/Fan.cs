using System.Xml.Serialization;

namespace aPC.Common.Entities
{
  public class Fan : Component
  {
    [XmlAttribute]
    public float Intensity;

    public override eComponentType ComponentType()
    {
      return eComponentType.Fan;
    }
  }
}