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

  public enum eDirection
  {
    [PhysicalDirection]
    North,
    [PhysicalDirection]
    NorthEast,
    [PhysicalDirection]
    East,

    SouthEast,
    
    South,
    
    SouthWest,
    [PhysicalDirection]
    West,
    [PhysicalDirection]
    NorthWest
  }
}
