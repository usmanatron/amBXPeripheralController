using aPC.Common;
using aPC.Common.Entities;
using aPC.Server.Actors;
using aPC.Server.Snapshots;
using aPC.Server.SceneHandlers;
using log4net;

namespace aPC.Server.Conductors
{
  public abstract class ComponentConductor<T> : ConductorBase<ComponentSnapshot<T>> where T : IComponent
  {
    protected ComponentConductor(eDirection xiDirection, ComponentActor<T> xiActor, ComponentHandler<T> xiHandler)
      : base(xiDirection, xiActor, xiHandler)
    {
    }

    protected override void Log(string xiNotification)
    {
      mLog.InfoFormat("Component:{0}, Direction:{1}, Message:{2}", ComponentType, Direction, xiNotification);
    }

    private static ILog mLog = LogManager.GetLogger("ComponentConductor");
  }
}
