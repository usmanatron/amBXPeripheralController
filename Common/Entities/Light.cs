using System.Xml.Serialization;

namespace Common.Entities
{
  public class Light
  {
    [XmlAttribute]
    public float Intensity;

    [XmlAttribute] 
    public float Red;

    [XmlAttribute] 
    public float Green;

    [XmlAttribute] 
    public float Blue;
  }
}