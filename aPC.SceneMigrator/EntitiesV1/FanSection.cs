using aPC.Common;
using System.Xml.Serialization;

namespace aPC.SceneMigrator.EntitiesV1
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