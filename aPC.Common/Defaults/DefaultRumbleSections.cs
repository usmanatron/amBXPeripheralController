using aPC.Common.Builders;
using aPC.Common.Entities;

namespace aPC.Common.Defaults
{
  public class DefaultRumbleSections
  {
    public static readonly RumbleSection Off = new RumbleSectionBuilder().WithRumbleInDirection(eDirection.Center, DefaultRumbles.Off).Build();
    public static readonly RumbleSection Boing = new RumbleSectionBuilder().WithRumbleInDirection(eDirection.Center, DefaultRumbles.Boing).Build();
    public static readonly RumbleSection Thunder = new RumbleSectionBuilder().WithRumbleInDirection(eDirection.Center, DefaultRumbles.Thunder).Build();
    public static readonly RumbleSection SoftThunder = new RumbleSectionBuilder().WithRumbleInDirection(eDirection.Center, DefaultRumbles.SoftThunder).Build();
  }
}