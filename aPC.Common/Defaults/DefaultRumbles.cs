using aPC.Common.Entities;

namespace aPC.Common.Defaults
{
  class DefaultRumbles
  {
    // Note that "Off" has an intensity of 0, which makes it actually off!
    public static readonly Rumble Off = new Rumble { Intensity = 0, RumbleType = "Boing", Speed = 0 };
    public static readonly Rumble Boing = new Rumble { Intensity = 1, RumbleType = "Boing", Speed = 1f };
    public static readonly Rumble Thunder = new Rumble { Intensity = 1, RumbleType = "Thunder", Speed = 1f };
    public static readonly Rumble SoftThunder = new Rumble { Intensity = 0.5f, RumbleType = "Thunder", Speed = 1f };
  }
}
