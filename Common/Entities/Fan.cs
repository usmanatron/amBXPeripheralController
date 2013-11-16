using System.Xml.Serialization;

namespace Common.Entities
{
  public class Fan
  {
    [XmlElement]
    public float Intensity;
  }
}