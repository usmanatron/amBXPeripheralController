namespace aPC.Common
{
  /// <summary>
  /// The type of component.
  /// </summary>
  public enum eComponentType
  {
    Light,
    Fan,
    Rumble
  }

  /// <summary>
  /// Composite - Allow all components to run by themselves
  /// Singular  - Force all components to be in sync with each other
  /// </summary>
  public enum eSceneType
  {
    Composite,
    Singular
  }

  /// <remarks>
  ///  *** WARNING ***
  ///  Do not change this enum!  It has to exactly map with the amBXLib equivalent (Direction)
  /// </remarks>
  public enum eDirection
  {
    Everywhere = 0,
    North = 1,
    NorthEast = 2,
    East = 4,
    SouthEast = 8,
    South = 16,
    SouthWest = 32,
    West = 64,
    NorthWest = 128,
    Center = 256
  }

  /// <remarks>
  ///  *** WARNING ***
  ///  Do not change this enum!  It has to exactly map with the amBXLib equivalent (RumbleType)
  /// </remarks>
  public enum eRumbleType
  {
    Boing = 0,
    Crash = 1,
    Engine = 2,
    Explosion = 3,
    Hit = 4,
    Quake = 5,
    Rattle = 6,
    Road = 7,
    Shot = 8,
    Thud = 9,
    Thunder = 10,
  }
}