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
      return new Light
      {
        FadeTime = FadeTime,
        Red = Red,
        Green = Green,
        Blue = Blue,
        Direction = Direction
      };
    }

    public override bool Equals(object other)
    {
      if (!(other is Light))
      {
        return false;
      }

      var otherLight = (Light)other;

      return FadeTime == otherLight.FadeTime &&
             Red == otherLight.Red &&
             Green == otherLight.Green &&
             Blue == otherLight.Blue;
    }
  }
}