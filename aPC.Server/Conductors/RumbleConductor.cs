using aPC.Common.Entities;
using aPC.Server.Conductors;
using aPC.Common;
using aPC.Server.EngineActors;
using aPC.Server.SceneHandlers;

namespace aPC.Server.Conductors
{
  class RumbleConductor : ComponentConductor<Rumble>
  {
    public RumbleConductor(eDirection xiDirection, RumbleActor xiActor, RumbleHandler xiHandler) 
      : base(xiDirection, xiActor, xiHandler)
    {
    }

    public override eComponentType ComponentType()
    {
      return eComponentType.Rumble;
    }
  }
}
