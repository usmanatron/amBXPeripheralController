using amBXLib;
using Common.Entities;
using Common.Integration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Server.Managers;

namespace ServerMT
{
  class LightManager : ManagerBase<Light>
  {
    public LightManager(CompassDirection xiDirection) 
      : base()
    {
      mComponentType = eComponentType.Light;
      mDirection = xiDirection;
    }

    // A scene is applicable if there is at least one non-null light in the right direction defined.
    protected override bool SceneIsApplicable(amBXScene xiNewScene)
    {
      var lLights = xiNewScene
        .Frames
        .Select(frame => frame.Lights)
        .Select(cnt => CompassDirectionConverter.GetLight(mDirection, cnt));

      return lLights.Any(light => light != null);
    }

    public override Data<Light> GetNext()
    {
      var lFrame = GetNextFrame();

      var lLength = lFrame.Length;
      var lFadeTime = lFrame.Lights.FadeTime;
      var lLight = CompassDirectionConverter.GetLight(mDirection, lFrame.Lights);

      return new Data<Light>(lLight, lFadeTime, lLength);
    }

    eComponentType mComponentType;
    CompassDirection mDirection;
  }
}
