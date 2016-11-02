using aPC.Common;
using aPC.Common.Entities;
using System;

namespace aPC.Server.Engine
{
  public class EngineActorSync
  {
    private readonly AmbxEngineWrapper ambxEngineWrapper;

    public EngineActorSync(AmbxEngineWrapper ambxEngineWrapper)
    {
      this.ambxEngineWrapper = ambxEngineWrapper;
    }

    public void UpdateComponent(DirectionalComponent component)
    {
      switch (component.ComponentType)
      {
        case eComponentType.Light:
          ambxEngineWrapper.UpdateLight(component.GetLight());
          break;

        case eComponentType.Fan:
          ambxEngineWrapper.UpdateFan(component.GetFan());
          break;

        case eComponentType.Rumble:
          ambxEngineWrapper.UpdateRumble(component.GetRumble());
          break;

        default:
          throw new ArgumentException("Unexpected Component Type");
      }
    }
  }
}