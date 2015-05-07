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
      Direction = direction;
    }

    public DirectionalComponent(eComponentType componentType)
    {
      ComponentType = componentType;
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