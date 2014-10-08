namespace aPC.Common
{
  public enum eComponentType
  {
    Light,
    Fan,
    Rumble
  }

  /// <summary>
  /// Desync - Allow all components to run by themselves
  /// Sync  - Force all sections to be in sync with each other
  /// Event - Ignore IsRepeatable.  Once all frames have been run, return to the previously running Scene.
  /// </summary>
  /// <remarks>
  ///   Being an event implies that your also Sync (i.e. a desync event doesn't make sense)
  /// </remarks>
  public enum eSceneType
  {
    Desync,
    Sync,
    Event
  }

  #region amBXLib-equivalent enums

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

  #endregion
}
