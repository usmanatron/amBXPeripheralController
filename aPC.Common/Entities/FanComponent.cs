using System.Xml.Serialization;

namespace aPC.Common.Entities
{
  public class FanComponent : ComponentBase
  {
    [XmlElement]
    public Fan East;

    [XmlElement]
    public Fan West;
  }
}