using aPC.Common.Entities;

namespace aPC.Common.Defaults
{
  class DefaultFanSections
  {
    public static FanSection Off = new FanSection
    {
      East = DefaultFans.Off,
      West = DefaultFans.Off
    };

    public static FanSection Quarter = new FanSection
    {
      East = DefaultFans.QuarterPower,
      West = DefaultFans.QuarterPower
    };

    public static FanSection Half = new FanSection
    {
      East = DefaultFans.HalfPower,
      West = DefaultFans.HalfPower
    };

    public static FanSection Full = new FanSection
    {
      East = DefaultFans.FullPower,
      West = DefaultFans.FullPower
    };
  }
}
