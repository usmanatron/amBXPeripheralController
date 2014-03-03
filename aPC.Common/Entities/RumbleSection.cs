using System.Xml.Serialization;

namespace aPC.Common.Entities
{
  public class RumbleSection : SectionBase<Rumble>
  {
    [XmlElement]
    [PhysicalComponent]
    [Direction(eDirection.Center)]
    public Rumble Rumble;
  }
}