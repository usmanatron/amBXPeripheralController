using aPC.Common;
using aPC.Common.Server.EngineActors;
using aPC.Common.Entities;
using aPC.Common.Server;
using aPC.Common.Server.Snapshots;
using System;

namespace aPC.Server.EngineActors
{
  class RumbleActor : ComponentActor<Rumble>
  {
    public RumbleActor(eDirection xiDirection, EngineManager xiEngine) 
      : base (xiDirection, xiEngine)
    {
    }

    public override void ActNextFrame(ComponentSnapshot<Rumble> xiSnapshot)
    {
      if (!xiSnapshot.IsComponentNull)
      {
        Engine.UpdateRumble(mDirection, xiSnapshot.Item);
      }
    }
  }
}
