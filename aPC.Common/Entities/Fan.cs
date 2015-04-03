using System.Xml.Serialization;

namespace aPC.Common.Entities
{
  public class Fan : IDirectionalComponent
  {
    [XmlElement]
    public float Intensity;

    public override eComponentType ComponentType()
    {
      return eComponentType.Fan;
    }
  }
}