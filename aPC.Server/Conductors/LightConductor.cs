using aPC.Common.Entities;
using aPC.Common.Server.Conductors;
using aPC.Common.Server.Snapshots;
using aPC.Server.EngineActors;
using aPC.Server.SceneHandlers;
using aPC.Common;

namespace aPC.Server.Conductors
{
  class LightConductor : ComponentConductor<Light>
  {
    public LightConductor(LightActor xiActor, LightHandler xiHandler) 
      : base(xiActor, xiHandler)
    {
    }

    public override eComponentType ComponentType()
    {
      return eComponentType.Light;
    }
  }
}
