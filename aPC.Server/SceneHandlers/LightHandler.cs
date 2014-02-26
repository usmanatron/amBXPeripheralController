using aPC.Common;
using aPC.Common.Entities;
using aPC.Common.Server.SceneHandlers;
using aPC.Common.Server.Snapshots;
using System;
using System.Collections.Generic;
using System.Linq;

namespace aPC.Server.SceneHandlers
{
  class LightHandler : ComponentHandler<Light>
  {
    public LightHandler(Action xiEventcomplete) : base(xiEventcomplete)
    { 
    }

    // A scene is applicable if there is at least one non-null light in the right direction defined.
    protected override bool FramesAreApplicable(List<Frame> xiFrames)
    {
      var lLights = xiFrames
        .Select(frame => frame.Lights)
        .Where(section => section != null)
        .Select(section => GetLight(Direction, section));

      return lLights.Any(light => light != null);
    }

    public override ComponentSnapshot<Light> GetNextSnapshot(eDirection xiDirection)
    {
      var lFrame = GetNextFrame();
      var lLight = GetLight(xiDirection, lFrame.Lights);

      return lLight == null
        ? new ComponentSnapshot<Light>(lFrame.Length)
        : new ComponentSnapshot<Light>(lLight, lFrame.Lights.FadeTime, lFrame.Length);
    }
    private Light GetLight(eDirection xiDirection, LightSection xiLights)
    {
      return xiLights.GetComponentValueInDirection(xiDirection);
    }
  }
}
