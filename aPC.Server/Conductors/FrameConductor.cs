using aPC.Common;
using aPC.Server.Actors;
using aPC.Server.Snapshots;
using aPC.Server.SceneHandlers;
using log4net;

namespace aPC.Server.Conductors
{
  public class FrameConductor : ConductorBase<FrameSnapshot>
  {
    public FrameConductor(FrameActor xiActor, FrameHandler xiHandler)
      : base(eDirection.Everywhere, xiActor, xiHandler)
    {
    }

    protected override void Log(string xiNotification)
    {
      mLog.Info(xiNotification);
    }

    public override eComponentType ComponentType
    {
      get { throw new System.InvalidOperationException("The FrameConductor doesn't have a component!"); }
    }

    private static ILog mLog = LogManager.GetLogger(typeof(FrameConductor));
  }
}
