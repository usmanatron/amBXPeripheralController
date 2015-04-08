using System.Xml.Serialization;

namespace aPC.SceneMigrator.EntitiesV1
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
    public LightSection Lights;

    [XmlElement]
    public FanSection Fans;

    [XmlElement]
    public RumbleSection Rumbles;
  }
}