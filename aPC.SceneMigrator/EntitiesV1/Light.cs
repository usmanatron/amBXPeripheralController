using aPC.Common;
using System.Xml.Serialization;

namespace aPC.SceneMigrator.EntitiesV1
{
  public class Light : IComponent
  {
    [XmlElement]
    public int FadeTime;

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