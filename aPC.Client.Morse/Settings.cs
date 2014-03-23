using aPC.Common.Entities;
using aPC.Common.Defaults;

namespace aPC.Client.Morse
{
  public class Settings
  {
    public Settings()
    {
      Colour = DefaultLights.White;
      LightsEnabled = true;
      RumblesEnabled = false;
      RepeatMessage = false;
    }

    public Light Colour;
    public bool LightsEnabled;
    public bool RumblesEnabled;
    public bool RepeatMessage;
  }
}
