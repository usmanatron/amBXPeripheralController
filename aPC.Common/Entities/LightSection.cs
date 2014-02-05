using System.Xml.Serialization;

namespace aPC.Common.Entities
{
  public class LightSection : SectionBase
  {
    [XmlElement]
    [Direction(eDirection.North)]
    public Light North;

    [XmlElement]
    [Direction(eDirection.NorthEast)]
    public Light NorthEast;

    [XmlElement]
    [Direction(eDirection.East)]
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
    public Light West;

    [XmlElement]
    [Direction(eDirection.NorthWest)]
    public Light NorthWest;
  }
}