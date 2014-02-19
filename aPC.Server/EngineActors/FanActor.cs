using aPC.Common;
using aPC.Common.Server.EngineActors;
using aPC.Common.Entities;
using aPC.Common.Server.Managers;
using aPC.Common.Server.Snapshots;
using aPC.Server.Managers;
using System;

namespace aPC.Server.EngineActors
{
  class FanActor : ComponentActor<Fan>
  {
    public FanActor(eDirection xiDirection, EngineManager xiEngine) 
      : base (xiDirection, xiEngine)
    {
    }

    public override void ActNextFrame(ComponentSnapshot<Fan> xiSnapshot)
    {
      if (!xiSnapshot.IsComponentNull)
      {
        Engine.UpdateFan(mDirection, xiSnapshot.Item);
      }
    }
  }
}
