using aPC.Common;
using aPC.Common.Server.EngineActors;
using aPC.Common.Entities;
using aPC.Common.Server.Managers;
using aPC.Common.Server.Snapshots;
using aPC.Server.Managers;
using System;

namespace aPC.Server.EngineActors
{
  class LightActor : ComponentActor<Light>
  {
    public LightActor(eDirection xiDirection, EngineManager xiEngine) 
      : base (xiDirection, xiEngine)
    {
    }

    public override void ActNextFrame(ComponentSnapshot<Light> xiSnapshot)
    {
      if (!xiSnapshot.IsComponentNull)
      {
        Engine.UpdateLight(mDirection, xiSnapshot.Item, xiSnapshot.FadeTime);
      }
    }
  }
}
