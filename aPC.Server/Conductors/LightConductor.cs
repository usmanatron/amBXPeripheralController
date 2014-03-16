using aPC.Common.Entities;
using aPC.Server.Conductors;
using aPC.Server.Actors;
using aPC.Server.SceneHandlers;
using aPC.Common;

namespace aPC.Server.Conductors
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
