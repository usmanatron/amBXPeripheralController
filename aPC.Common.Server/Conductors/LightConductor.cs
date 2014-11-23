using aPC.Common.Entities;
using aPC.Common.Server.Actors;
using aPC.Common.Server.SceneHandlers;

namespace aPC.Common.Server.Conductors
{
  public class LightConductor : ComponentConductor<Light>
  {
    public LightConductor(eDirection direction, LightActor actor, LightHandler handler)
      : base(direction, actor, handler)
    {
    }

    public override eComponentType ComponentType
    {
      get
      {
        return eComponentType.Light;
      }
    }
  }
}