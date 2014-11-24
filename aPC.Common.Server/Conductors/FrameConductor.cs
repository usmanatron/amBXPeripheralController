using aPC.Common.Server.Actors;
using aPC.Common.Server.SceneHandlers;
using aPC.Common.Server.Snapshots;
using log4net;

namespace aPC.Common.Server.Conductors
{
  public class FrameConductor : ConductorBase<FrameSnapshot>
  {
    private static ILog log = LogManager.GetLogger(typeof(FrameConductor));

    public FrameConductor(FrameActor actor, FrameHandler handler)
      : base(eDirection.Everywhere, actor, handler)
    {
    }

    protected override void Log(string xiNotification)
    {
      log.Info(xiNotification);
    }
  }
}