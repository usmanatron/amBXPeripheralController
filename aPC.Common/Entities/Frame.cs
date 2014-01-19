using System.Xml.Serialization;

namespace aPC.Common.Entities
{
  public class Frame
  {
    [XmlAttribute] 
    public int Length;

    // Each frame can be set seperately.  the idea is that you can have a certain animation as a one off and
    // then certain parts don't show again.
    [XmlAttribute]
    public bool IsRepeated;

    [XmlElement] 
    public LightSection Lights;

    [XmlElement]
    public FanSection Fans;

    [XmlElement]
    public RumbleSection Rumbles;
  }
}
