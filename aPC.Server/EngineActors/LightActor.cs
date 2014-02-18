using aPC.Common;
using aPC.Common.Server.EngineActors;
using aPC.Common.Entities;
using aPC.Common.Server.Managers;
using aPC.Common.Server.Snapshot;
using aPC.Server.Managers;
using System;

namespace aPC.Server.EngineActors
{
  class LightActor : ComponentActor
  {
    public LightActor(eDirection xiDirection, EngineManager xiEngine) 
      : base (xiDirection, xiEngine)
    {
    }

    public override void ActNextFrame(SnapshotBase xiSnapshot)
    {
      var lSnapshot = (ComponentSnapshot)xiSnapshot;

      if (!lSnapshot.IsComponentNull)
      {
        Engine.UpdateLight(mDirection, (Light)lSnapshot.Component, lSnapshot.FadeTime);
      }
    }
  }
}
