using aPC.Common.Server.EngineActors;
using aPC.Common.Server.Snapshots;
using aPC.Common.Server.SceneHandlers;

namespace aPC.Common.Server.Conductors
{
  public class FrameConductor : ConductorBase<FrameSnapshot>
  {
    public FrameConductor(FrameActor xiActor, FrameHandler xiHandler)
      : base(eDirection.Everywhere, xiActor, xiHandler)
    {
    }
  }
}
