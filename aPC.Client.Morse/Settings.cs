using aPC.Common.Entities;
using aPC.Common.Defaults;

namespace aPC.Client.Morse
{
  public class Settings
  {
    public Settings()
    {
      Message = "";
      Colour = DefaultLights.White;
      UnitLength = 100;
      LightsEnabled = true;
      RumblesEnabled = false;
      RepeatMessage = false;
    }

    public string Message;
    public Light Colour;
    public int UnitLength;
    public bool LightsEnabled;
    public bool RumblesEnabled;
    public bool RepeatMessage;
  }
}
