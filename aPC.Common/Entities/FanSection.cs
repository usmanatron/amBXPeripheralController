using System.Xml.Serialization;

namespace aPC.Common.Entities
{
  public class FanSection : SectionBase
  {
    [XmlElement]
    public Fan East;

    [XmlElement]
    public Fan West;
  }
}