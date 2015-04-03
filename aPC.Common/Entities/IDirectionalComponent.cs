using System.Xml.Serialization;

namespace aPC.Common.Entities
{
  /// <summary>
  /// The base interface for individual light / fan / rumble sources
  /// </summary>
  public abstract class IDirectionalComponent
  {
    [XmlElement]
    public eDirection Direction;

    public abstract eComponentType ComponentType();
  }
}