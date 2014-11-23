using System.Xml.Serialization;

namespace aPC.Common.Entities
{
  public class Rumble : IComponent
  {
    [XmlElement]
    public eRumbleType RumbleType;

    [XmlElement]
    public float Intensity;

    [XmlElement]
    public float Speed;

    public eComponentType ComponentType()
    {
      return eComponentType.Rumble;
    }
  }
}