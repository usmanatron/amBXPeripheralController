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
