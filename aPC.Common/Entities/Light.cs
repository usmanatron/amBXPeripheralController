using System.Xml.Serialization;

namespace aPC.Common.Entities
{
  public class Light : IDirectionalComponent
  {
    [XmlElement]
    public int FadeTime;

    [XmlElement]
    public float Red;

    [XmlElement]
    public float Green;

    [XmlElement]
    public float Blue;

    public override eComponentType ComponentType()
    {
      return eComponentType.Light;
    }
  }
}