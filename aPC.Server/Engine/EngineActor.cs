using aPC.Common;
using aPC.Common.Entities;
using System;
using System.Threading;

namespace aPC.Server.Engine
{
  public class EngineActor
  {
    private readonly AmbxEngineWrapper ambxEngineWrapper;

    public EngineActor(AmbxEngineWrapper ambxEngineWrapper)
    {
      this.ambxEngineWrapper = ambxEngineWrapper;
    }

    public void UpdateComponent(DirectionalComponent component, RunMode runMode)
    {
      var action = GetAction(component);

      switch (runMode)
      {
        case RunMode.Asynchronous:
          ThreadPool.QueueUserWorkItem(_ => action.Invoke());
          break;
        case RunMode.Synchronous:
          action.Invoke();
          break;
        default:
          throw new ArgumentException("Unexpected RunMode");
      }
    }

    private Action GetAction(DirectionalComponent component)
    {
      switch (component.ComponentType)
      {
        case eComponentType.Light:
          return () => ambxEngineWrapper.UpdateLight(component.GetLight());
        case eComponentType.Fan:
          return () => ambxEngineWrapper.UpdateFan(component.GetFan());
        case eComponentType.Rumble:
          return () => ambxEngineWrapper.UpdateRumble(component.GetRumble());
        default:
          throw new ArgumentException("Unexpected Component Type");
      }
    }
  }
}