using amBXLib;
using Common.Entities;
using Common.Integration;
using Common.Server.Managers;

namespace Server
{
  // Manages the amBXEngine interface - deals with adding and setting stuff etc.
  class EngineManager : EngineManagerBase
  {

    internal void UpdateLights(LightComponent xiLights)
    {
      UpdateLight(NorthLight, xiLights.North, xiLights.FadeTime);
      UpdateLight(NorthEastLight, xiLights.NorthEast, xiLights.FadeTime);
      UpdateLight(EastLight, xiLights.East, xiLights.FadeTime);
      UpdateLight(SouthEastLight, xiLights.SouthEast, xiLights.FadeTime);
      UpdateLight(SouthLight, xiLights.South, xiLights.FadeTime);
      UpdateLight(SouthWestLight, xiLights.SouthWest, xiLights.FadeTime);
      UpdateLight(WestLight, xiLights.West, xiLights.FadeTime);
      UpdateLight(NorthWestLight, xiLights.NorthWest, xiLights.FadeTime);
    }

    private void UpdateLight(amBXLight xiLight, Light xiInputLight, int xiFadeTime)
    {
      if (xiInputLight == null)
      {
        // No change - don't touch!
        return;
      }
      xiLight.Color = new amBXColor { Red = xiInputLight.Red, Green = xiInputLight.Green, Blue = xiInputLight.Blue };
      xiLight.FadeTime = xiFadeTime;
    }

    internal void UpdateFans(FanComponent xiFans)
    {
      UpdateFan(EastFan, xiFans.East);
      UpdateFan(WestFan, xiFans.West);
    }

    private void UpdateFan(amBXFan xiFan, Fan xiInputFan)
    {
      if (xiInputFan == null)
      {
        return;
      }
      xiFan.Intensity = xiInputFan.Intensity;
    }

    internal void UpdateRumbles(RumbleComponent xiInputRumble)
    {
      UpdateRumble(Rumble, xiInputRumble);
    }

    private void UpdateRumble(amBXRumble xiRumble, RumbleComponent xiInputRumble)
    {
      if (xiInputRumble == null)
      {
        return;
      }

      RumbleType lRumbleType;

      try
      {
        lRumbleType = RumbleTypeConverter.GetRumbleType(xiInputRumble.RumbleType);
      }
      catch (InvalidRumbleException)
      {
        return;
      }

      xiRumble.RumbleSetting = new amBXRumbleSetting
                               {
                                 Intensity = xiInputRumble.Intensity,
                                 Speed = xiInputRumble.Speed,
                                 Type = lRumbleType
                               };
    }
  }
}
