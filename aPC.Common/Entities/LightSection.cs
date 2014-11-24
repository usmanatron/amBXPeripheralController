using System.Xml.Serialization;

namespace aPC.Common.Entities
{
  public class LightSection : IComponentSection
  {
    [XmlElement]
    [Direction(eDirection.North)]
    [PhysicalComponent]
    public Light North;

    [XmlElement]
    [Direction(eDirection.NorthEast)]
    [PhysicalComponent]
    public Light NorthEast;

    [XmlElement]
    [Direction(eDirection.East)]
    [PhysicalComponent]
    public Light East;

    [XmlElement]
    [Direction(eDirection.SouthEast)]
    public Light SouthEast;

    [XmlElement]
    [Direction(eDirection.South)]
    public Light South;

    [XmlElement]
    [Direction(eDirection.SouthWest)]
    public Light SouthWest;

    [XmlElement]
    [Direction(eDirection.West)]
    [PhysicalComponent]
    public Light West;

    [XmlElement]
    [Direction(eDirection.NorthWest)]
    [PhysicalComponent]
    public Light NorthWest;

    [XmlElement]
    public int FadeTime;
  }
}