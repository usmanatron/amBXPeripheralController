using System.Xml.Serialization;

namespace aPC.Common.Entities
{
  public class Frame
  {
    [XmlElement]
    public int Length;

    // Each frame can be set seperately.  the idea is that you can have a certain animation as a one off and
    // then certain parts don't show again.
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