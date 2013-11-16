using System.Xml.Serialization;

namespace Common.Entities
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
    public LightComponent Lights;

    [XmlElement]
    public FanComponent Fans;

    [XmlElement]
    public RumbleComponent Rumble;
  }
}
