using aPC.Common;
using aPC.Common.Server.EngineActors;
using aPC.Common.Entities;
using aPC.Common.Server;
using aPC.Common.Server.Snapshots;

namespace aPC.Server.EngineActors
{
  class FanActor : ComponentActor<Fan>
  {
    public FanActor(IEngine xiEngine) : base (xiEngine)
    {
    }

    public override void ActNextFrame(eDirection xiDirection, ComponentSnapshot<Fan> xiSnapshot)
    {
      if (!xiSnapshot.IsComponentNull)
      {
        Engine.UpdateFan(xiDirection, xiSnapshot.Item);
      }
    }
  }
}
