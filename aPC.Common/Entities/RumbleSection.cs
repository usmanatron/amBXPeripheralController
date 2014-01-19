using System.Xml.Serialization;

namespace aPC.Common.Entities
{
  public class RumbleSection : SectionBase
  {
    [XmlElement]
    public Rumble Rumble;
  }
}