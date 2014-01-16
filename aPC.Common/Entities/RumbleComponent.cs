using System.Xml.Serialization;

namespace aPC.Common.Entities
{
  public class RumbleComponent : ComponentBase
  {
    [XmlElement] 
    public string RumbleType;

    [XmlElement]
    public float Intensity;

    [XmlElement]
    public float Speed;

  }
}