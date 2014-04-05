using aPC.Common.Entities;
using aPC.Common.Defaults;

namespace aPC.Client.Morse
{
  public class Settings
  {
    public Settings(string xiMessage)
    {
      Message = xiMessage;

      Colour = DefaultLights.White;
      UnitLength = 100;
      LightsEnabled = true;
      RumblesEnabled = false;
      RepeatMessage = false;

      // The following settings cannot be overridden:
      Rumble = DefaultRumbles.Thunder;
    }

    public string Message;
    public Light Colour;
    public int UnitLength;
    public bool LightsEnabled;
    public bool RumblesEnabled;
    public bool RepeatMessage;

    public Rumble Rumble { get; private set; }
  }
}
