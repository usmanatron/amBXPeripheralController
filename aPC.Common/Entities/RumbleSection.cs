using System.Xml.Serialization;

namespace aPC.Common.Entities
{
  public class RumbleSection : SectionBase<Rumble>
  {
    [XmlElement]
    [PhysicalComponent]
    public Rumble Rumble;
  }
}