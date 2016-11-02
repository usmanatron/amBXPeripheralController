using System;
using System.Xml.Serialization;

namespace aPC.Common.Entities
{
  /// <summary>
  /// The base interface for individual light / fan / rumble sources
  /// </summary>
  [Serializable]
  public class DirectionalComponent
  {
    [XmlElement]
    public eDirection Direction;

    [XmlIgnore]
    public eComponentType ComponentType { get; }

    public DirectionalComponent(eComponentType componentType, eDirection direction)
      : this(componentType)
    {
      Direction = direction;
    }

    public DirectionalComponent(eComponentType componentType)
    {
      ComponentType = componentType;
    }

    public Light GetLight()
    {
      return (Light) this;
    }

    public Fan GetFan()
    {
      return (Fan)this;
    }

    public Rumble GetRumble()
    {
      return (Rumble)this;
    }

    public override bool Equals(object other)
    {
      if (!(other is DirectionalComponent))
      {
        return false;
      }

      var otherComponent = (DirectionalComponent)other;

      return ComponentType == otherComponent.ComponentType &&
             Direction == otherComponent.Direction;
    }
  }
}