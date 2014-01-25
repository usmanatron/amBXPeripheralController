using aPC.Common;
using aPC.Common.Server.EngineActors;
using aPC.Common.Entities;
using aPC.Common.Server.Managers;
using aPC.Server.Managers;
using amBXLib;
using System;

namespace aPC.Server.EngineActors
{
  class RumbleActor : EngineActorBase
  {
    public RumbleActor(CompassDirection xiDirection, EngineManager xiEngine, Action xiEventCallback) 
      : base (xiEngine, new RumbleManager(xiDirection, xiEventCallback))
    {
      mDirection = xiDirection;
    }

    public override eActorType ActorType()
    {
      return eActorType.Rumble;
    }

    protected override void ActNextFrame()
    {
      var lRumbleData = (ComponentData)Manager.GetNext();

      if (!lRumbleData.IsComponentNull)
      {
        Engine.UpdateRumble(mDirection, (Rumble)lRumbleData.Item);
      }

      WaitforInterval(lRumbleData.Length);
    }

    private readonly CompassDirection mDirection;
  }
}
