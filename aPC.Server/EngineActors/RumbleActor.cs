using aPC.Common;
using aPC.Common.Server.EngineActors;
using aPC.Common.Entities;
using aPC.Common.Server.Managers;
using aPC.Common.Server.Snapshots;
using aPC.Server.Managers;
using System;

namespace aPC.Server.EngineActors
{
  class RumbleActor : ComponentActor
  {
    public RumbleActor(eDirection xiDirection, EngineManager xiEngine) 
      : base (xiDirection, xiEngine)
    {
    }

    public override void ActNextFrame(SnapshotBase xiSnapshot)
    {
      var lSnapshot = (ComponentSnapshot<Rumble>)xiSnapshot;

      if (!lSnapshot.IsComponentNull)
      {
        Engine.UpdateRumble(mDirection, lSnapshot.Item);
      }
    }
  }
}
