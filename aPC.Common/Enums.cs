namespace aPC.Common
{
  public enum eComponentType
  {
    Light,
    Fan,
    Rumble
  }

  #region amBXLib-equivalent enums

  //qqUMI Write a unit test that forces equivalence
  // between these and the amBXLib equivalents
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
