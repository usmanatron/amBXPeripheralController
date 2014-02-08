using aPC.Common.Builders;
using aPC.Common.Entities;

namespace aPC.Common.Defaults
{
  public class DefaultFanSections
  {
    public static FanSection Off = new FanSectionBuilder().WithAllFans(DefaultFans.Off).Build();
    public static FanSection Quarter = new FanSectionBuilder().WithAllFans(DefaultFans.QuarterPower).Build();
    public static FanSection Half = new FanSectionBuilder().WithAllFans(DefaultFans.HalfPower).Build();
    public static FanSection Full = new FanSectionBuilder().WithAllFans(DefaultFans.FullPower).Build();
  }
}
