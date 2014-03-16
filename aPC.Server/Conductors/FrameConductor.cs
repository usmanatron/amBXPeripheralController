using aPC.Common;
using aPC.Server.Actors;
using aPC.Server.Snapshots;
using aPC.Server.SceneHandlers;

namespace aPC.Server.Conductors
{
  public class FrameConductor : ConductorBase<FrameSnapshot>
  {
    public FrameConductor(FrameActor xiActor, FrameHandler xiHandler)
      : base(eDirection.Everywhere, xiActor, xiHandler)
    {
    }

    public override eComponentType ComponentType
    {
      get { throw new System.InvalidOperationException("The FrameConductor doesn't have a component!"); }
    }
  }
}
