using aPC.Common;
using aPC.Common.Entities;
using aPC.Server;
using aPC.Server.Engine;
using aPC.Server.Snapshots;

namespace aPC.Server.Actors
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
