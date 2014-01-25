using aPC.Common;
using aPC.Common.Server.EngineActors;
using aPC.Common.Entities;
using aPC.Common.Server.Managers;
using aPC.Server.Managers;
using System;
using amBXLib;

namespace aPC.Server.EngineActors
{
  class FanActor : EngineActorBase
  {
    public FanActor(CompassDirection xiDirection, EngineManager xiEngine, Action xiEventCallback) 
      : base (xiEngine, new FanManager(xiDirection, xiEventCallback))
    {
      mDirection = xiDirection;
    }

    public override eActorType ActorType()
    {
      return eActorType.Fan;
    }

    protected override void ActNextFrame()
    {
      var lFanData = (ComponentData)Manager.GetNext();

      if (!lFanData.IsComponentNull)
      {
        Engine.UpdateFan(mDirection, (Fan)lFanData.Item);
        
      }
      WaitforInterval(lFanData.Length);
    }

    private CompassDirection mDirection;
  }
}
