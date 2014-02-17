using aPC.Common;
using aPC.Common.Server.EngineActors;
using aPC.Common.Entities;
using aPC.Common.Server.Managers;
using aPC.Server.Managers;
using System;

namespace aPC.Server.EngineActors
{
  class RumbleActor : EngineActorBase
  {
    public RumbleActor(eDirection xiDirection, EngineManager xiEngine) 
      : base (xiEngine)
    {
      mDirection = xiDirection;
    }

    public override void ActNextFrame(Data xiData)
    {
      var lRumbleData = (ComponentData)xiData;

      if (!lRumbleData.IsComponentNull)
      {
        Engine.UpdateRumble(mDirection, (Rumble)lRumbleData.Component);
      }
    }

    private readonly eDirection mDirection;
  }
}
