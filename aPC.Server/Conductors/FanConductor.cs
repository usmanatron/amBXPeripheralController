using aPC.Common.Entities;
using aPC.Common.Server.Conductors;
using aPC.Server.EngineActors;
using aPC.Common;
using aPC.Server.SceneHandlers;

namespace aPC.Server.Conductors
{
  class FanConductor : ComponentConductor<Fan>
  {
    public FanConductor(FanActor xiActor, FanHandler xiHandler)
      : base (xiActor, xiHandler)
    {
    }

    public override eComponentType ComponentType()
    {
      return eComponentType.Fan;
    }
  }
}
