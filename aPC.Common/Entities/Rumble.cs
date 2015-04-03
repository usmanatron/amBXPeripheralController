using System.Xml.Serialization;

namespace aPC.Common.Entities
{
  public class Rumble : IDirectionalComponent
  {
    [XmlElement]
    public eRumbleType RumbleType;

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