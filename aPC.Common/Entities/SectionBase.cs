using System.Xml.Serialization;

namespace aPC.Common.Entities
{
  public class SectionBase<T> where T : Component
  {
    [XmlAttribute]
    public int FadeTime;
  }
}
