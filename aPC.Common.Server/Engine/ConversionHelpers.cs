using amBXLib;
using System;

namespace aPC.Common.Server.Engine
{
  public static class ConversionHelpers
  {
    public static CompassDirection GetDirection(eDirection direction)
    {
      return (CompassDirection)Enum.Parse(typeof(CompassDirection), direction.ToString());
    }

    public static RumbleType GetRumbleType(eRumbleType rumbleType)
    {
      return (RumbleType)Enum.Parse(typeof(RumbleType), rumbleType.ToString());
    }
  }
}