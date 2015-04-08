using aPC.Common;
using System.Xml.Serialization;

namespace aPC.SceneMigrator.EntitiesV1
{
  public class RumbleSection : IComponentSection
  {
    [XmlElement]
    [PhysicalComponent]
    [Direction(eDirection.Everywhere)]
    [Direction(eDirection.North)]
    [Direction(eDirection.NorthEast)]
    [Direction(eDirection.East)]
    [Direction(eDirection.SouthEast)]
    [Direction(eDirection.South)]
    [Direction(eDirection.SouthWest)]
    [Direction(eDirection.West)]
    [Direction(eDirection.NorthWest)]
    [Direction(eDirection.Center)]
    public Rumble Rumble;
  }
}