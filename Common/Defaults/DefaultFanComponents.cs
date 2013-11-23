using Common.Entities;

namespace Common.Defaults
{
  class DefaultFanComponents
  {
    public static FanComponent Off = new FanComponent
    {
      East = DefaultFans.Off,
      West = DefaultFans.Off
    };

    public static FanComponent FullPower = new FanComponent
    {
      East = DefaultFans.FullPower,
      West = DefaultFans.FullPower
    };
  }
}
