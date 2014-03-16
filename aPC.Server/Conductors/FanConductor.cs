using aPC.Common.Entities;
using aPC.Server.Conductors;
using aPC.Server.Actors;
using aPC.Common;
using aPC.Server.SceneHandlers;

namespace aPC.Server.Conductors
{
  public class FanConductor : ComponentConductor<Fan>
  {
    public FanConductor(eDirection xiDirection, FanActor xiActor, FanHandler xiHandler)
      : base (xiDirection, xiActor, xiHandler)
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
