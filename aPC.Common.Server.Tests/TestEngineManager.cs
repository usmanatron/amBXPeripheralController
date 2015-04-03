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
        LightSection = new LightSection() { Lights = new List<Light>() },
        FanSection = new FanSection() { Fans = new List<Fan>() },
        RumbleSection = new RumbleSection() { Rumbles = new List<Rumble>() }
      };
      Updated = new Dictionary<eComponentType, bool>()
        {
          {eComponentType.Light, false}, {eComponentType.Fan, false}, {eComponentType.Rumble, false}
        };
    }

    public void UpdateComponent(eDirection direction, DirectionalComponent component)
    {
      if (component == null)
      {
        return;
      }

      switch (component.ComponentType())
      {
        case eComponentType.Light:
          UpdateLight(direction, (Light)component);
          break;
        case eComponentType.Fan:
          UpdateFan(direction, (Fan)component);
          break;
        case eComponentType.Rumble:
          UpdateRumble(direction, (Rumble)component);
          break;
      }
    }

    private void UpdateLight(eDirection direction, Light light)
    {
      Updated[eComponentType.Light] = true;
      Status.LightSection.Lights.Add(light);
    }

    private void UpdateFan(eDirection direction, Fan fan)
    {
      Updated[eComponentType.Fan] = true;
      Status.FanSection.Fans.Add(fan);
    }

    private void UpdateRumble(eDirection direction, Rumble rumble)
    {
      Updated[eComponentType.Rumble] = true;
      Status.RumbleSection.Rumbles.Add(rumble);
    }

    // Nothing to dispose.
    public void Dispose()
    {
    }
  }
}