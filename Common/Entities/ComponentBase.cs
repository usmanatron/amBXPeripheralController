using System.Xml.Serialization;

namespace Common.Entities
{
  public class ComponentBase
  {
    [XmlAttribute]
    public int FadeTime;
  }

  public enum eComponentType
  {
    Light,
    Fan,
    Rumble
  }
}
