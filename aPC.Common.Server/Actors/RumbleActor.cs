using aPC.Common.Entities;
using aPC.Common.Server.Engine;
using aPC.Common.Server.Snapshots;

namespace aPC.Common.Server.Actors
{
  public class RumbleActor : ComponentActor<Rumble>
  {
    public RumbleActor(IEngine engine)
      : base(engine)
    {
    }

    public override void ActNextFrame(eDirection direction, ComponentSnapshot<Rumble> snapshot)
    {
      if (!snapshot.IsComponentNull)
      {
        Engine.UpdateRumble(direction, snapshot.Item);
      }
    }
  }
}