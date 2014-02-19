using System.Xml.Serialization;

namespace aPC.Common.Entities
{
  public class SectionBase<T> where T : IComponent
  {
    [XmlAttribute]
    public int FadeTime;
  }
}
