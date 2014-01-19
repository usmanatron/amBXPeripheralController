using System.Xml.Serialization;

namespace aPC.Common.Entities
{
  public class SectionBase
  {
    [XmlAttribute]
    public int FadeTime;
  }

  public enum eSectionType
  {
    Light,
    Fan,
    Rumble
  }
}
