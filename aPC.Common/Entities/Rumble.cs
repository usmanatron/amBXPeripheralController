using System.Xml.Serialization;

namespace aPC.Common.Entities
{
  public class Rumble : Component
  {
    [XmlElement]
    public string RumbleType;

    [XmlElement]
    public float Intensity;

    [XmlElement]
    public float Speed;

    public override eComponentType ComponentType()
    {
      return eComponentType.Rumble;
    }
  }
}
