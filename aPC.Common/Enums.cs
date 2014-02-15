namespace aPC.Common
{
  // qqUMI TODO: Merge these in such a way that makes sense!
  public enum eActorType
  {
    Frame,
    Light,
    Fan,
    Rumble
  }

  public enum eSectionType
  {
    Light,
    Fan,
    Rumble
  }

  public enum eComponentType
  {
    Light,
    Fan,
    Rumble
  }

  //qqUMI Write a unit test that forces equivalence
  // between this and CompassDirection
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
}
