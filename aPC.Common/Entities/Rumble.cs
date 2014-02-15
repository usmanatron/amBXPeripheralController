using System.Xml.Serialization;

namespace aPC.Common.Entities
{
  public class Rumble : Component
  {
    [XmlElement]
    public eRumbleType RumbleType;

    [XmlElement]
    public float Intensity;

    [XmlElement]
    public float Speed;
  }
}
