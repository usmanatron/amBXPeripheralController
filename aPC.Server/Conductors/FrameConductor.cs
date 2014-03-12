using aPC.Common;
using aPC.Server.EngineActors;
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
  }
}
