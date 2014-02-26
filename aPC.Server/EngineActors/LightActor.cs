using aPC.Common;
using aPC.Common.Server.EngineActors;
using aPC.Common.Entities;
using aPC.Common.Server;
using aPC.Common.Server.Snapshots;

namespace aPC.Server.EngineActors
{
  class LightActor : ComponentActor<Light>
  {
    public LightActor(EngineManager xiEngine) : base (xiEngine)
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
