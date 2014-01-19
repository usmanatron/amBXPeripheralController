using amBXLib;
using aPC.Common.Entities;
using aPC.Common.Integration;
using System.Linq;
using aPC.Common.Server.Managers;
using System;

namespace aPC.Server.Managers
{
  class LightManager : ManagerBase
  {
    public LightManager(CompassDirection xiDirection) 
      : this(xiDirection, null)
    {
    }

    public LightManager(CompassDirection xiDirection, Action xiEventCallback) 
      : base(xiEventCallback)
    {
      mDirection = xiDirection;
      SetupNewScene(CurrentScene);
    }

    // A scene is applicable if there is at least one non-null light in the right direction defined.
    protected override bool SceneIsApplicable(amBXScene xiNewScene)
    {
      var lLights = xiNewScene
        .Frames
        .Select(frame => frame.Lights)
        .Where(section => section != null)
        .Select(section => CompassDirectionConverter.GetLight(mDirection, section));

      return lLights.Any(light => light != null);
    }

    public override Data GetNext()
    {
      //Debug
      Console.WriteLine(mDirection + " - GetNext     - " + DateTime.Now.Ticks);

      var lFrame = GetNextFrame();

      var lLength = lFrame.Length;
      int lFadeTime;
      Light lLight;

      if (lFrame.Lights == null)
      {
        lFadeTime = 0;
        lLight = null;
      }
      else
      {
        lFadeTime = lFrame.Lights.FadeTime;
        lLight = CompassDirectionConverter.GetLight(mDirection, lFrame.Lights);
      }

      return new ComponentData(lLight, lFadeTime, lLength);
    }

    readonly CompassDirection mDirection;
  }
}
