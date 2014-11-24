using System.Xml.Serialization;

namespace aPC.Common.Entities
{
  public class RumbleSection : IComponentSection
  {
    [XmlElement]
    [PhysicalComponent]
    [Direction(eDirection.Center)]
    public Rumble Rumble;
  }
}