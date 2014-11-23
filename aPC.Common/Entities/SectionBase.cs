using System.Xml.Serialization;

namespace aPC.Common.Entities
{
  public class SectionBase<T> where T : IComponent
  {
    [XmlElement]
    public int FadeTime;
  }
}