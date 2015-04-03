using System.Xml.Serialization;

namespace aPC.Common.Entities
{
  public class Fan : DirectionalComponent
  {
    [XmlElement]
    public float Intensity;

    public override eComponentType ComponentType()
    {
      return eComponentType.Fan;
    }

    public override object Clone()
    {
      return new Fan()
      {
        Intensity = this.Intensity,
        Direction = this.Direction
      };
    }

    public override bool Equals(object other)
    {
      if (!(other is Fan))
      {
        return false;
      }

      var otherFan = (Fan)other;

      return this.Intensity == otherFan.Intensity;
    }
  }
}