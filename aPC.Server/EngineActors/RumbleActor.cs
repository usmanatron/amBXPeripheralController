using aPC.Common.Server.EngineActors;
using aPC.Common.Entities;
using aPC.Common.Server.Managers;
using aPC.Server.Managers;
using amBXLib;
using System;

namespace aPC.Server.EngineActors
{
  class RumbleActor : EngineActorBase<Rumble>
  {
    public RumbleActor(CompassDirection xiDirection, EngineManager xiEngine, Action xiEventCallback) 
      : base (xiEngine, new RumbleManager(xiDirection, xiEventCallback))
    {
      mDirection = xiDirection;
    }

    protected override void ActNextFrame()
    {
      var lRumbleData = (ComponentData)Manager.GetNext();

      if (lRumbleData != null)
      {
        Engine.UpdateRumble(mDirection, null);//qqUMI null needs to be filled in
        WaitforInterval(lRumbleData.Length);
      }
      else
      {
        WaitforInterval(1000); //qqUMI constantify
      }
    }

    private readonly CompassDirection mDirection;
  }
}
