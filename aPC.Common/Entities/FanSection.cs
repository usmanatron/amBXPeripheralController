using System.Xml.Serialization;

namespace aPC.Common.Entities
{
  public class FanSection : IComponentSection
  {
    [XmlElement]
    [Direction(eDirection.East)]
    [Direction(eDirection.NorthEast)]
    [PhysicalComponent]
    public Fan East;

    [XmlElement]
    [Direction(eDirection.West)]
    [Direction(eDirection.NorthWest)]
    [PhysicalComponent]
    public Fan West;
  }
}