using aPC.Common.Entities;
using aPC.Common.Server.Engine;
using System.Collections.Generic;

namespace aPC.Common.Server.Tests
{
  public class TestEngineManager : IEngine
  {
    public Frame Status;
    public Dictionary<eComponentType, bool> Updated;

    public TestEngineManager()
    {
      Status = new Frame()
      {
        Lights = new LightSection(),
        Fans = new FanSection(),
        Rumbles = new RumbleSection()
      };
      Updated = new Dictionary<eComponentType, bool>()
        {
          {eComponentType.Light, false}, {eComponentType.Fan, false}, {eComponentType.Rumble, false}
        };
    }

    public void UpdateLight(eDirection direction, Light light, int fadeTime)
    {
      Updated[eComponentType.Light] = true;
      Status.Lights.SetComponentValueInDirection(light, direction);
      Status.Lights.FadeTime = fadeTime;
    }

    public void UpdateFan(eDirection direction, Fan fan)
    {
      Updated[eComponentType.Fan] = true;
      Status.Fans.SetComponentValueInDirection(fan, direction);
    }

    public void UpdateRumble(eDirection direction, Rumble rumble)
    {
      Updated[eComponentType.Rumble] = true;
      Status.Rumbles.SetComponentValueInDirection(rumble, direction);
    }

    // Nothing to dispose.
    public void Dispose()
    {
    }
  }
}