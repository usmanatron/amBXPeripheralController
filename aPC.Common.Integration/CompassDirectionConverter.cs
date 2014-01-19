using System;
using aPC.Common.Entities;
using amBXLib;

namespace aPC.Common.Integration
{
  public static class CompassDirectionConverter
  {
    public static Light GetLight(CompassDirection xiDirection, LightSection xiLights)
    {
      switch (xiDirection)
      {
        case CompassDirection.North:
          return xiLights.North;
        case CompassDirection.NorthEast:
          return xiLights.NorthEast;
        case CompassDirection.East:
          return xiLights.East;
        case CompassDirection.SouthEast:
          return xiLights.SouthEast;
        case CompassDirection.South:
          return xiLights.South;
        case CompassDirection.SouthWest:
          return xiLights.SouthWest;
        case CompassDirection.West:
          return xiLights.West;
        case CompassDirection.NorthWest:
          return xiLights.NorthWest;
        default:
          throw new InvalidOperationException("Unexpected Compass Direction: " + xiDirection);
      }
    }

    public static Fan GetFan(CompassDirection xiDirection, FanSection xiFans)
    {
      switch (xiDirection)
      {
        case CompassDirection.NorthEast:
        case CompassDirection.East:
          return xiFans.East;
        case CompassDirection.NorthWest:
        case CompassDirection.West:
          return xiFans.West;
        default:
          return null;
      }
    }
  }
}
