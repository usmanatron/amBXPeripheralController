using aPC.Common.Entities;
using aPC.Common.Server.Engine;
using aPC.Common.Server.Snapshots;

namespace aPC.Common.Server.Actors
{
  public class LightActor : ComponentActor<Light>
  {
    public LightActor(IEngine engine)
      : base(engine)
    {
    }

    public override void ActNextFrame(eDirection direction, ComponentSnapshot<Light> snapshot)
    {
      if (!snapshot.IsComponentNull)
      {
        Engine.UpdateLight(direction, snapshot.Item, snapshot.FadeTime);
      }
    }
  }
}