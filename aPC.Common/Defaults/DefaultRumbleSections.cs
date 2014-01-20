using aPC.Common.Entities;

namespace aPC.Common.Defaults
{
  class DefaultRumbleSections
  {
    public static readonly RumbleSection Off = new RumbleSection { FadeTime = 500, Rumble = DefaultRumbles.Off };
    public static readonly RumbleSection Boing = new RumbleSection { FadeTime = 500, Rumble = DefaultRumbles.Boing };
    public static readonly RumbleSection Thunder = new RumbleSection { FadeTime = 500, Rumble = DefaultRumbles.Thunder };
    public static readonly RumbleSection SoftThunder = new RumbleSection { FadeTime = 500, Rumble = DefaultRumbles.SoftThunder };
  }
}
