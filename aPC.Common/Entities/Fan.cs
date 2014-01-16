using System.Xml.Serialization;

namespace aPC.Common.Entities
{
  public class Fan
  {
    [XmlAttribute]
    public float Intensity;
  }
}