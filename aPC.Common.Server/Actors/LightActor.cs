using aPC.Common;
using aPC.Common.Entities;
using aPC.Common.Server;
using aPC.Common.Server.Engine;
using aPC.Common.Server.Snapshots;

namespace aPC.Common.Server.Actors
{
  public class LightActor : ComponentActor<Light>
  {
    public LightActor(IEngine xiEngine) : base (xiEngine)
    {
    }

    public override void ActNextFrame(eDirection xiDirection, ComponentSnapshot<Light> xiSnapshot)
    {
      if (!xiSnapshot.IsComponentNull)
      {
        Engine.UpdateLight(xiDirection, xiSnapshot.Item, xiSnapshot.FadeTime);
      }
    }
  }
}
