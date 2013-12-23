using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Entities;
using amBXLib;

namespace Common.Integration
{
  public static class CompassDirectionConverter
  {
    public static Light GetLight(CompassDirection xiDirection, LightComponent xiLights)
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
  }
}
