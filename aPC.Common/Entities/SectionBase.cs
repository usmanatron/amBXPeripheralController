using System.Xml.Serialization;

namespace aPC.Common.Entities
{
  public class SectionBase
  {
    [XmlAttribute]
    public int FadeTime;
  }

  // qqUMI This is used by Sections AND Components.  Move to a better place
  public enum eSectionType
  {
    Light,
    Fan,
    Rumble
  }
}
