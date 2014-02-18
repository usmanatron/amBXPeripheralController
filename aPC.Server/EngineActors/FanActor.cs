using aPC.Common;
using aPC.Common.Server.EngineActors;
using aPC.Common.Entities;
using aPC.Common.Server.Managers;
using aPC.Common.Server.Snapshot;
using aPC.Server.Managers;
using System;

namespace aPC.Server.EngineActors
{
  class FanActor : ComponentActor
  {
    public FanActor(eDirection xiDirection, EngineManager xiEngine) 
      : base (xiDirection, xiEngine)
    {
    }

    public override void ActNextFrame(SnapshotBase xiSnapshot)
    {
      var lSnapshot = (ComponentSnapshot)xiSnapshot;

      if (!lSnapshot.IsComponentNull)
      {
        Engine.UpdateFan(mDirection, (Fan)lSnapshot.Component);
      }
    }
  }
}
