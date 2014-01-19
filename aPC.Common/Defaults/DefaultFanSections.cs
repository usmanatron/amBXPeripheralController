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

    public static FanSection FullPower = new FanSection
    {
      East = DefaultFans.FullPower,
      West = DefaultFans.FullPower
    };
  }
}
