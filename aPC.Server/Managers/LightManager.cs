using amBXLib;
using aPC.Common.Entities;
using System.Linq;
using aPC.Common.Server.Managers;
using aPC.Common;
using System;
using System.Collections.Generic;
using aPC.Server.EngineActors;

namespace aPC.Server.Managers
{
  class LightManager : ComponentManager
  {
    public LightManager(eDirection xiDirection, LightActor xiActor) 
      : this(xiDirection, xiActor, null)
    {
    }

    public LightManager(eDirection xiDirection, LightActor xiActor,  Action xiEventCallback) 
      : base(xiActor, xiEventCallback)
    {
      Direction = xiDirection;
      SetupNewScene(CurrentScene);
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

    public override Data GetNextData()
    {
      var lFrame = GetNextFrame();
      var lLight = GetLight(Direction, lFrame.Lights);

      return lLight == null
        ? new ComponentData(lFrame.Length)
        : new ComponentData(lLight, lFrame.Lights.FadeTime, lFrame.Length);
    }

    private Light GetLight(eDirection xiDirection, LightSection xiLights)
    {
      return xiLights.GetComponentValueInDirection(xiDirection);
    }

    public override eComponentType ComponentType()
    {
      return eComponentType.Light;
    }
  }
}
