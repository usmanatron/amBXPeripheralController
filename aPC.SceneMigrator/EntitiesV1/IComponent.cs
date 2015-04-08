using aPC.Common;

namespace aPC.SceneMigrator.EntitiesV1
{
  /// <summary>
  /// The base interface for individual light / fan / rumble sources
  /// </summary>
  public interface IComponent
  {
    eComponentType ComponentType();
  }
}