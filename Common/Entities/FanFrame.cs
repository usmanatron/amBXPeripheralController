using System.Xml.Serialization;

namespace Common.Entities
{
  public class FanFrame : Frame
  {
    [XmlAttribute]
    public bool IsRepeated;

    [XmlElement]
    public Fan East;

    [XmlElement]
    public Fan West;
  }
}