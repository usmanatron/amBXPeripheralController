using amBXLib;
using aPC.Common.Entities;
using System.Linq;
using aPC.Common.Server.Managers;
using aPC.Common;
using System;
using System.Collections.Generic;

namespace aPC.Server.Managers
{
  class LightManager : ManagerBase
  {
    public LightManager(eDirection xiDirection) 
      : this(xiDirection, null)
    {
    }

    public LightManager(eDirection xiDirection, Action xiEventCallback) 
      : base(xiEventCallback)
    {
      mDirection = xiDirection;
      SetupNewScene(CurrentScene);
    }

    // A scene is applicable if there is at least one non-null light in the right direction defined.
    protected override bool FramesAreApplicable(List<Frame> xiFrames)
    {
      var lLights = xiFrames
        .Select(frame => frame.Lights)
        .Where(section => section != null)
        .Select(section => GetLight(mDirection, section));

      return lLights.Any(light => light != null);
    }

    public override Data GetNext()
    {
      // Temporary debug trace
      Console.WriteLine(mDirection + " - GetNext     - " + DateTime.Now.Ticks);

      var lFrame = GetNextFrame();

      var lLight = GetLight(mDirection, lFrame.Lights);

      return lLight == null
        ? new ComponentData(lFrame.Length)
        : new ComponentData(lLight, lFrame.Lights.FadeTime, lFrame.Length);
    }

    private Light GetLight(eDirection xiDirection, LightSection xiLights)
    {
      var lComponentInfo = xiLights.GetComponentInfoInDirection(xiDirection);
      return (Light)lComponentInfo.GetValue(xiLights);
    }

    readonly eDirection mDirection;
  }
}
