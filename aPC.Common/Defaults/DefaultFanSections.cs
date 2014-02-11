using aPC.Common.Builders;
using aPC.Common.Entities;

namespace aPC.Common.Defaults
{
  public class DefaultFanSections
  {
    public static FanSection Off = new FanSectionBuilder().WithFadeTime(200).WithAllFans(DefaultFans.Off).Build();
    public static FanSection Quarter = new FanSectionBuilder().WithFadeTime(200).WithAllFans(DefaultFans.QuarterPower).Build();
    public static FanSection Half = new FanSectionBuilder().WithFadeTime(200).WithAllFans(DefaultFans.HalfPower).Build();
    public static FanSection Full = new FanSectionBuilder().WithFadeTime(200).WithAllFans(DefaultFans.FullPower).Build();
  }
}
