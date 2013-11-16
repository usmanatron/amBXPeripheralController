using System.Xml.Serialization;

namespace Common.Entities
{
  public class RumbleFrame : Frame
  {
    [XmlElement]
    public bool IsRepeated;
    // rumble setup
  }
}