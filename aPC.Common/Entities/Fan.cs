using System.Xml.Serialization;

namespace aPC.Common.Entities
{
  public class Fan : Component
  {
    [XmlAttribute]
    public float Intensity;

    public override eSectionType ComponentType()
    {
      return eSectionType.Fan;
    }
  }
}