using aPC.Common;
using aPC.Common.Entities;
using aPC.Server;
using aPC.Server.Engine;
using aPC.Server.Snapshots;

namespace aPC.Server.Actors
{
  class RumbleActor : ComponentActor<Rumble>
  {
    public RumbleActor(IEngine xiEngine) : base (xiEngine)
    {
    }

    public override void ActNextFrame(eDirection xiDirection, ComponentSnapshot<Rumble> xiSnapshot)
    {
      if (!xiSnapshot.IsComponentNull)
      {
        Engine.UpdateRumble(xiDirection, xiSnapshot.Item);
      }
    }
  }
}
