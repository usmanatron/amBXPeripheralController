using aPC.Common;
using aPC.Common.Entities;
using System;
using System.Threading;

namespace aPC.ServerV3.Engine
{
  internal class EngineActor
  {
    private AmbxEngineWrapper ambxEngineWrapper;

    public EngineActor(AmbxEngineWrapper ambxEngineWrapper)
    {
      this.ambxEngineWrapper = ambxEngineWrapper;
    }

    public void UpdateComponent(IComponent component, eDirection direction)
    {
      switch (component.ComponentType())
      {
        case eComponentType.Light:
          ThreadPool.QueueUserWorkItem(_ => ambxEngineWrapper.UpdateLight(direction, (Light)component));
          break;
        case eComponentType.Fan:
          ThreadPool.QueueUserWorkItem(_ => ambxEngineWrapper.UpdateFan(direction, (Fan)component));
          break;
        case eComponentType.Rumble:
          ThreadPool.QueueUserWorkItem(_ => ambxEngineWrapper.UpdateRumble(direction, (Rumble)component));
          break;
        default:
          throw new ArgumentException("Unexpected Component Type");
      }
    }
  }
}