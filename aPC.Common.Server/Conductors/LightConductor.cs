using aPC.Common.Entities;
using aPC.Common.Server.Actors;
using aPC.Common.Server.SceneHandlers;

namespace aPC.Common.Server.Conductors
{
  public class LightConductor : ComponentConductor<Light>
  {
    public LightConductor(eDirection xiDirection, LightActor xiActor, LightHandler xiHandler)
      : base(xiDirection, xiActor, xiHandler)
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