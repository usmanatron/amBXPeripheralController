using System;
using System.Xml.Serialization;

namespace aPC.Common.Entities
{
  /// <summary>
  /// The base interface for individual light / fan / rumble sources
  /// </summary>
  public abstract class DirectionalComponent : ICloneable
  {
    [XmlElement]
    public eDirection Direction;

    public abstract eComponentType ComponentType();

    public abstract object Clone();

    //public abstract bool Equals(object other);
  }
}