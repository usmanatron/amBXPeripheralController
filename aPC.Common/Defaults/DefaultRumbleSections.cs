using aPC.Common.Builders;
using aPC.Common.Entities;

namespace aPC.Common.Defaults
{
  public class DefaultRumbleSections
  {
    public static readonly RumbleSection Off = new RumbleSectionBuilder().WithFadeTime(500).WithRumble(DefaultRumbles.Off).Build();
    public static readonly RumbleSection Boing = new RumbleSectionBuilder().WithFadeTime(500).WithRumble(DefaultRumbles.Boing).Build();
    public static readonly RumbleSection Thunder = new RumbleSectionBuilder().WithFadeTime(500).WithRumble(DefaultRumbles.Thunder).Build();
    public static readonly RumbleSection SoftThunder = new RumbleSectionBuilder().WithFadeTime(500).WithRumble(DefaultRumbles.SoftThunder).Build();
  }
}