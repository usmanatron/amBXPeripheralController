using System.Xml.Serialization;

namespace Common.Entities
{
  public class LightFrame : Frame
  {

    // Each light frame can be set seperately.  the idea is that you can have a certain animation as a one off and
    // then certain parts don't show again.
    [XmlAttribute]
    public bool IsRepeated;

    [XmlAttribute]
    public int FadeTime;

    [XmlElement]
    public Light North;

    [XmlElement]
    public Light NorthEast;

    [XmlElement]
    public Light East;

    [XmlElement]
    public Light SouthEast;

    [XmlElement]
    public Light South;

    [XmlElement]
    public Light SouthWest;

    [XmlElement]
    public Light West;

    [XmlElement]
    public Light NorthWest;
  }
}