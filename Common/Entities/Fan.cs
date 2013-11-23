using System.Xml.Serialization;

namespace Common.Entities
{
  public class Fan
  {
    [XmlAttribute]
    public float Intensity;
  }
}