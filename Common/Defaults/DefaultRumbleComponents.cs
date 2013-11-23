using Common.Entities;

namespace Common.Defaults
{
  class DefaultRumbleComponents
  {
    public static readonly RumbleComponent Off = new RumbleComponent
    {
      Intensity = 0,
      FadeTime = 500,
      RumbleType = "Off",
      Speed = 0
    };
  }
}
