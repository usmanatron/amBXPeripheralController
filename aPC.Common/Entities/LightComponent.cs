using System.Xml.Serialization;

namespace aPC.Common.Entities
{
  public class LightComponent : ComponentBase
  {
    [XmlElement]
    public Light North;

    [XmlElement]
    public Light NorthEast;

    [XmlElement]
    public Light East;

    [XmlElement]
    public Light SouthEast;

    [XmlElement]
    public Light South;

    [XmlElement]
    public Light SouthWest;

    [XmlElement]
    public Light West;

    [XmlElement]
    public Light NorthWest;
  }
}