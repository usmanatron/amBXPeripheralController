using amBXLib;
using System;

namespace aPC.Common.Server.Engine
{
  public static class ConversionHelpers
  {
    public static CompassDirection GetDirection(eDirection xiDirection)
    {
      return (CompassDirection)Enum.Parse(typeof(CompassDirection), xiDirection.ToString());
    }

    public static RumbleType GetRumbleType(eRumbleType xiRumbleType)
    {
      return (RumbleType)Enum.Parse(typeof(RumbleType), xiRumbleType.ToString());
    }
  }
}