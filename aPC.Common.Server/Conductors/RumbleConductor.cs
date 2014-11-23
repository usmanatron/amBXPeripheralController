using aPC.Common.Entities;
using aPC.Common.Server.Actors;
using aPC.Common.Server.SceneHandlers;

namespace aPC.Common.Server.Conductors
{
  public class RumbleConductor : ComponentConductor<Rumble>
  {
    public RumbleConductor(eDirection direction, RumbleActor actor, RumbleHandler handler)
      : base(direction, actor, handler)
    {
    }

    public override eComponentType ComponentType
    {
      get
      {
        return eComponentType.Rumble;
      }
    }
  }
}