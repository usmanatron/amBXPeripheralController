using amBXLib;
using aPC.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aPC.Common.Server
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
