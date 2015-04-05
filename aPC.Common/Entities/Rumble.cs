using System;
using System.Xml.Serialization;

namespace aPC.Common.Entities
{
  public class Rumble : DirectionalComponent, ICloneable
  {
    [XmlElement]
    public eRumbleType RumbleType;

    [XmlElement]
    public float Intensity;

    [XmlElement]
    public float Speed;

    public Rumble()
      : base(eComponentType.Rumble)
    {
    }

    public object Clone()
    {
      return new Rumble()
      {
        RumbleType = this.RumbleType,
        Intensity = this.Intensity,
        Speed = this.Speed,
        Direction = this.Direction
      };
    }

    public override bool Equals(object other)
    {
      if (!(other is Rumble))
      {
        return false;
      }

      var otherRumble = (Rumble)other;

      return this.RumbleType == otherRumble.RumbleType &&
             this.Intensity == otherRumble.Intensity &&
             this.Speed == otherRumble.Speed;
    }
  }
}