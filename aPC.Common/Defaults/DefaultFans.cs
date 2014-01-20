using aPC.Common.Entities;

namespace aPC.Common.Defaults
{
  class DefaultFans
  {
    public static readonly Fan Off = new Fan { Intensity = 0 };
    public static readonly Fan QuarterPower = new Fan { Intensity = 0.25f };
    public static readonly Fan HalfPower = new Fan { Intensity = 0.5f };
    public static readonly Fan FullPower = new Fan{ Intensity = 1 };
  }
}
