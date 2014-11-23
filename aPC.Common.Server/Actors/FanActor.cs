using aPC.Common.Entities;
using aPC.Common.Server.Engine;
using aPC.Common.Server.Snapshots;

namespace aPC.Common.Server.Actors
{
  public class FanActor : ComponentActor<Fan>
  {
    public FanActor(IEngine engine)
      : base(engine)
    {
    }

    public override void ActNextFrame(eDirection direction, ComponentSnapshot<Fan> snapshot)
    {
      if (!snapshot.IsComponentNull)
      {
        Engine.UpdateFan(direction, snapshot.Item);
      }
    }
  }
}