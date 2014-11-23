using aPC.Common.Entities;
using aPC.Common.Server.Engine;
using aPC.Common.Server.Snapshots;

namespace aPC.Common.Server.Actors
{
  public class RumbleActor : ComponentActor<Rumble>
  {
    public RumbleActor(IEngine xiEngine)
      : base(xiEngine)
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