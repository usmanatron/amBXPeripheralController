using aPC.Common.Entities;

namespace aPC.Common.Defaults
{
  class DefaultRumbleComponents
  {
    public static readonly RumbleComponent Off = new RumbleComponent { FadeTime = 500, Rumble = DefaultRumbles.Off };
    public static readonly RumbleComponent Boing = new RumbleComponent { FadeTime = 500, Rumble = DefaultRumbles.Boing };
    public static readonly RumbleComponent Thunder = new RumbleComponent { FadeTime = 500, Rumble = DefaultRumbles.Thunder };
  }
}
