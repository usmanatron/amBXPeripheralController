using aPC.Common.Builders;
using aPC.Common.Entities;

namespace aPC.Common.Defaults
{
  public class DefaultRumbleSections
  {
    public static readonly RumbleSection Off = new RumbleSectionBuilder().WithRumble(DefaultRumbles.Off).Build();
    public static readonly RumbleSection Boing = new RumbleSectionBuilder().WithRumble(DefaultRumbles.Boing).Build();
    public static readonly RumbleSection Thunder = new RumbleSectionBuilder().WithRumble(DefaultRumbles.Thunder).Build();
    public static readonly RumbleSection SoftThunder = new RumbleSectionBuilder().WithRumble(DefaultRumbles.SoftThunder).Build();
  }
}