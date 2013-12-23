using amBXLib;
using Common.Entities;
using Common.Integration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Server.Managers
{
  class LightManager : ManagerBase<Light>
  {
    public LightManager(CompassDirection xiDirection) 
      : base(new amBXScene())//qqUMI fix!
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

    public override Light GetNext()
    {
      var lFrame = GetNextFrame();
      var lLight = CompassDirectionConverter.GetLight(mDirection, lFrame.Lights);

      return lLight;
    }

    eComponentType mComponentType;
    CompassDirection mDirection;
  }
}
