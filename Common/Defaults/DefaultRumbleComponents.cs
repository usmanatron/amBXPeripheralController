using Common.Entities;

namespace Common.Defaults
{
  class DefaultRumbleComponents
  {
    // We need to set the type to be a valid Rumble Type.  Note however that
    // the intensity is set to 0
    public static readonly RumbleComponent Off = new RumbleComponent
    {
      Intensity = 0,
      FadeTime = 500,
      RumbleType = "Boing",
      Speed = 0
    };

    public static readonly RumbleComponent Boing = new RumbleComponent
    {
      Intensity = 1,
      FadeTime = 500,
      RumbleType = "Boing",
      Speed = 1f
    };

    public static readonly RumbleComponent Thunder = new RumbleComponent
    {
      Intensity = 1,
      FadeTime = 500,
      RumbleType = "Thunder",
      Speed = 1f
    };
  }
}
