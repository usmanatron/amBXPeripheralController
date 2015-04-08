using aPC.Common;
using System.Xml.Serialization;

namespace aPC.SceneMigrator.EntitiesV1
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