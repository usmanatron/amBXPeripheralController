using aPC.Common;
using aPC.Common.Entities;
using aPC.Common.Server.SceneHandlers;
using aPC.Common.Server.Snapshots;
using System;
using System.Collections.Generic;
using System.Linq;

namespace aPC.Common.Server.SceneHandlers
{
  public class LightHandler : ComponentHandler<Light>
  {
    public LightHandler(amBXScene xiScene, Action xiEventcomplete) : base(xiScene, xiEventcomplete)
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
