using aPC.Common.Entities;
using aPC.Common.Server.Conductors;
using aPC.Common;
using aPC.Common.Server.Actors;
using aPC.Common.Server.SceneHandlers;

namespace aPC.Common.Server.Conductors
{
  public class RumbleConductor : ComponentConductor<Rumble>
  {
    public RumbleConductor(eDirection xiDirection, RumbleActor xiActor, RumbleHandler xiHandler) 
      : base(xiDirection, xiActor, xiHandler)
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
