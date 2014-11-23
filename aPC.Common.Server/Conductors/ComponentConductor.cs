using aPC.Common.Entities;
using aPC.Common.Server.Actors;
using aPC.Common.Server.SceneHandlers;
using aPC.Common.Server.Snapshots;
using log4net;

namespace aPC.Common.Server.Conductors
{
  public abstract class ComponentConductor<T> : ConductorBase<ComponentSnapshot<T>> where T : IComponent
  {
    private static ILog log = LogManager.GetLogger("ComponentConductor");

    protected ComponentConductor(eDirection direction, ComponentActor<T> actor, ComponentHandler<T> handler)
      : base(direction, actor, handler)
    {
    }

    protected override void Log(string message)
    {
      log.InfoFormat("Component:{0}, Direction:{1}, Message:{2}", ComponentType, Direction, message);
    }
  }
}