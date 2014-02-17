using aPC.Common;
using aPC.Common.Server.EngineActors;
using aPC.Common.Entities;
using aPC.Common.Server.Managers;
using aPC.Server.Managers;
using System;

namespace aPC.Server.EngineActors
{
  class LightActor : ComponentActor
  {
    public LightActor(eDirection xiDirection, EngineManager xiEngine) 
      : base (xiDirection, xiEngine)
    {
    }

    public override void ActNextFrame(Snapshot xiData)
    {
      var lLightData = (ComponentSnapshot) xiData;
      
      if (!lLightData.IsComponentNull)
      {
        Engine.UpdateLight(mDirection, (Light)lLightData.Component, lLightData.FadeTime);
      }
    }
  }
}
