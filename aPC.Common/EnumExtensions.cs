using System;
using System.Collections.Generic;

namespace aPC.Common
{
  public class EnumExtensions
  {
    public static IEnumerable<eDirection> GetCompassDirections()
    {
      yield return eDirection.North;
      yield return eDirection.NorthEast;
      yield return eDirection.East;
      yield return eDirection.SouthEast;
      yield return eDirection.South;
      yield return eDirection.SouthWest;
      yield return eDirection.West;
      yield return eDirection.NorthWest;
    }
  }
}