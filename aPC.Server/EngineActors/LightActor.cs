using aPC.Common;
using aPC.Common.Server.EngineActors;
using aPC.Common.Entities;
using aPC.Common.Server;
using aPC.Common.Server.Snapshots;

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
        Engine.UpdateLight(Direction, xiSnapshot.Item, xiSnapshot.FadeTime);
      }
    }
  }
}
