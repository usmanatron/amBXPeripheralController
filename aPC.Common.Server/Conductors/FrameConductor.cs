using aPC.Common.Server.Actors;
using aPC.Common.Server.SceneHandlers;
using aPC.Common.Server.Snapshots;
using log4net;

namespace aPC.Common.Server.Conductors
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