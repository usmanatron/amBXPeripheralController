using aPC.Common.Defaults;
using aPC.Common.Entities;

namespace aPC.Client.Morse
{
  public class Settings
  {
    public string Message;
    public Light Colour;
    public int UnitLength;
    public bool LightsEnabled;
    public bool RumblesEnabled;
    public bool RepeatMessage;

    public Rumble Rumble { get; private set; }

    public Settings(string xiMessage)
    {
      Message = xiMessage;

      Colour = DefaultLights.White;
      UnitLength = 200;
      LightsEnabled = true;
      RumblesEnabled = false;
      RepeatMessage = false;

      // The following settings cannot be overridden:
      Rumble = DefaultRumbles.Thunder;
    }
  }
}