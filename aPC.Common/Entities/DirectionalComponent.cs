using System;
using System.Xml.Serialization;

namespace aPC.Common.Entities
{
  /// <summary>
  /// The base interface for individual light / fan / rumble sources
  /// </summary>
  public class DirectionalComponent
  {
    [XmlElement]
    public eDirection Direction;

    [XmlIgnore]
    public eComponentType ComponentType { get; private set; }

    public DirectionalComponent(eComponentType componentType, eDirection direction)
      : this(componentType)
    {
      this.Direction = direction;
    }

    public DirectionalComponent(eComponentType componentType)
    {
      this.ComponentType = componentType;
    }

    public override bool Equals(object other)
    {
      if (!(other is DirectionalComponent))
      {
        return false;
      }

      var otherComponent = (DirectionalComponent)other;

      return this.ComponentType == otherComponent.ComponentType &&
             this.Direction == otherComponent.Direction;
    }
  }
}