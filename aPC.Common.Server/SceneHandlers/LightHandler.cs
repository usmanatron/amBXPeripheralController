using aPC.Common.Entities;
using aPC.Common.Server.Snapshots;
using System;

namespace aPC.Common.Server.SceneHandlers
{
  public class LightHandler : ComponentHandler<Light>
  {
    public LightHandler(amBXScene scene, Action eventComplete)
      : base(scene, eventComplete)
    {
    }

    public override ComponentSnapshot<Light> GetNextSnapshot(eDirection direction)
    {
      var frame = GetNextFrame();
      var light = GetLight(direction, frame.Lights);

      return light == null
        ? new ComponentSnapshot<Light>(frame.Length)
        : new ComponentSnapshot<Light>(light, frame.Lights.FadeTime, frame.Length);
    }

    private Light GetLight(eDirection direction, LightSection lights)
    {
      return lights.GetComponentValueInDirection(direction);
    }
  }
}