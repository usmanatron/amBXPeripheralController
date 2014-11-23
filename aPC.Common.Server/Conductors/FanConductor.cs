using aPC.Common.Entities;
using aPC.Common.Server.Actors;
using aPC.Common.Server.SceneHandlers;

namespace aPC.Common.Server.Conductors
{
  public class FanConductor : ComponentConductor<Fan>
  {
    public FanConductor(eDirection direction, FanActor actor, FanHandler handler)
      : base(direction, actor, handler)
    {
    }

    public override eComponentType ComponentType
    {
      get
      {
        return eComponentType.Fan;
      }
    }
  }
}