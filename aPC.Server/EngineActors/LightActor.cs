using aPC.Common;
using aPC.Common.Server.EngineActors;
using aPC.Common.Entities;
using aPC.Common.Server.Managers;
using aPC.Server.Managers;
using System;

namespace aPC.Server.EngineActors
{
  class LightActor : EngineActorBase
  {
    public LightActor(eDirection xiDirection, EngineManager xiEngine, LightManager xiManager) 
      : base (xiEngine, xiManager)
    {
      mDirection = xiDirection;
    }

    public override void ActNextFrame(Data xiData)
    {
      var lLightData = (ComponentData) xiData;
      
      if (!lLightData.IsComponentNull)
      {
        Engine.UpdateLight(mDirection, (Light)lLightData.Component, lLightData.FadeTime);
      }
    }

    private readonly eDirection mDirection;
  }
}
