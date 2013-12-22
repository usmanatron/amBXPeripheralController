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
      UpdateLight(CompassDirection.North, xiLights.North, xiLights.FadeTime);
      UpdateLight(CompassDirection.NorthEast, xiLights.NorthEast, xiLights.FadeTime);
      UpdateLight(CompassDirection.East, xiLights.East, xiLights.FadeTime);
      UpdateLight(CompassDirection.SouthEast, xiLights.SouthEast, xiLights.FadeTime);
      UpdateLight(CompassDirection.South, xiLights.South, xiLights.FadeTime);
      UpdateLight(CompassDirection.SouthWest, xiLights.SouthWest, xiLights.FadeTime);
      UpdateLight(CompassDirection.West, xiLights.West, xiLights.FadeTime);
      UpdateLight(CompassDirection.NorthWest, xiLights.NorthWest, xiLights.FadeTime);
    }

    internal void UpdateFans(FanComponent xiFans)
    {
      UpdateFan(CompassDirection.East, xiFans.East);
      UpdateFan(CompassDirection.West, xiFans.West);
    }

    internal void UpdateRumbles(RumbleComponent xiInputRumble)
    {
      UpdateRumble(CompassDirection.Everywhere, xiInputRumble);
    }
  }
}
