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
    public LightActor(CompassDirection xiDirection, EngineManager xiEngine, Action xiEventCallback) 
      : base (xiEngine, new LightManager(xiDirection, xiEventCallback))
    {
      mDirection = xiDirection;
    }

    protected override void ActNextFrame()
    {
      var lLightData = (ComponentData)Manager.GetNext();

      if (lLightData != null)
      {
        //Debug
        Console.WriteLine(mDirection + " - UpdateLight - " + DateTime.Now.Ticks);
        Engine.UpdateLight(mDirection, (Light) lLightData.Item, lLightData.FadeTime);
        WaitforInterval(lLightData.Length);
      }
      else
      {
        WaitForDefaultInterval();
      }
    }

    private readonly CompassDirection mDirection;
  }
}
