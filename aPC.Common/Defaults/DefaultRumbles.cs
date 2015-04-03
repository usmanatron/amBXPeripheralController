using aPC.Common.Entities;

namespace aPC.Common.Defaults
{
  public class DefaultRumbles
  {
    // There is no "Off" RumbleType, hence we set Intensity to 0 to ensure nothing happens
    public static readonly Rumble Off = new Rumble { Intensity = 0, RumbleType = eRumbleType.Boing, Speed = 0 };

    public static readonly Rumble Boing = new Rumble { Intensity = 1, RumbleType = eRumbleType.Boing, Speed = 1f };
    public static readonly Rumble Thunder = new Rumble { Intensity = 1, RumbleType = eRumbleType.Thunder, Speed = 1f };
    public static readonly Rumble SoftThunder = new Rumble { Intensity = 0.5f, RumbleType = eRumbleType.Thunder, Speed = 1f };
  }
}