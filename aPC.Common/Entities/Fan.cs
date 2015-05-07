using System;
using System.Xml.Serialization;

namespace aPC.Common.Entities
{
  public class Fan : DirectionalComponent, ICloneable
  {
    [XmlElement]
    public float Intensity;

    public Fan()
      : base(eComponentType.Fan)
    {
    }

    public object Clone()
    {
      return new Fan
      {
        Intensity = Intensity,
        Direction = Direction
      };
    }

    public override bool Equals(object other)
    {
      if (!(other is Fan))
      {
        return false;
      }

      var otherFan = (Fan)other;

      return Intensity == otherFan.Intensity;
    }
  }
}