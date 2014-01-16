using System.Xml.Serialization;

namespace aPC.Common.Entities
{
  public class RumbleComponent : ComponentBase
  {
    [XmlElement]
    public Rumble Rumble;
  }
}