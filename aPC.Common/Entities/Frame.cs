using System.Xml.Serialization;

namespace aPC.Common.Entities
{
  public class Frame
  {
    [XmlElement]
    public int Length;

    [XmlElement]
    public bool IsRepeated;

    [XmlElement]
    public LightSection LightSection;

    [XmlElement]
    public FanSection FanSection;

    [XmlElement]
    public RumbleSection RumbleSection;
  }
}