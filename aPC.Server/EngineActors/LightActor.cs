using aPC.Common;
using aPC.Common.Server.EngineActors;
using aPC.Common.Entities;
using aPC.Common.Server.Managers;
using aPC.Server.Managers;
using amBXLib;
using System;

namespace aPC.Server.EngineActors
{
  class LightActor : EngineActorBase
  {
    public LightActor(eDirection xiDirection, EngineManager xiEngine, Action xiEventCallback) 
      : base (xiEngine, new LightManager(xiDirection, xiEventCallback))
    {
      mDirection = xiDirection;
    }

    public override eActorType ActorType()
    {
      return eActorType.Light;
    }

    protected override void ActNextFrame()
    {
      var lLightData = (ComponentData)Manager.GetNext();
      
      
      if (!lLightData.IsComponentNull)
      {
        // Temporary Debug trace:
        Console.WriteLine(mDirection + " - UpdateLight - " + DateTime.Now.Ticks);
        Engine.UpdateLight(mDirection, (Light)lLightData.Component, lLightData.FadeTime);
      }

      WaitforInterval(lLightData.Length);
    }

    private readonly eDirection mDirection;
  }
}
