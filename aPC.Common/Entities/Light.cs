using System;
using System.Xml.Serialization;

namespace aPC.Common.Entities
{
  public class Light : DirectionalComponent, ICloneable
  {
    [XmlElement]
    public int FadeTime;

    [XmlElement]
    public float Red;

    [XmlElement]
    public float Green;

    [XmlElement]
    public float Blue;

    public Light()
      : base(eComponentType.Light)
    {
    }

    public object Clone()
    {
      return new Light()
      {
        FadeTime = this.FadeTime,
        Red = this.Red,
        Green = this.Green,
        Blue = this.Blue,
        Direction = this.Direction
      };
    }

    public override bool Equals(object other)
    {
      if (!(other is Light))
      {
        return false;
      }

      var otherLight = (Light)other;

      return this.FadeTime == otherLight.FadeTime &&
             this.Red == otherLight.Red &&
             this.Green == otherLight.Green &&
             this.Blue == otherLight.Blue;
    }
  }
}