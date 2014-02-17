using aPC.Common;
using aPC.Common.Server.EngineActors;
using aPC.Common.Entities;
using aPC.Common.Server.Managers;
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

    public override void ActNextFrame(Snapshot xiData)
    {
      var lFanData = (ComponentSnapshot)xiData;

      if (!lFanData.IsComponentNull)
      {
        Engine.UpdateFan(mDirection, (Fan)lFanData.Component);
      }
    }
  }
}
