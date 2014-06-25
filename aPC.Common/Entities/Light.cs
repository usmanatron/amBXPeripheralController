using System.Xml.Serialization;

namespace aPC.Common.Entities
{
  public class Light : IComponent
  {
    [XmlElement]
    public float Intensity;

    [XmlElement] 
    public float Red;

    [XmlElement] 
    public float Green;

    [XmlElement] 
    public float Blue;

    public eComponentType ComponentType()
    {
      return eComponentType.Light;
    }
  }
}