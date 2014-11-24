using aPC.Common.Entities;
using aPC.Common.Server.Actors;
using aPC.Common.Server.SceneHandlers;
using aPC.Common.Server.Snapshots;
using log4net;

namespace aPC.Common.Server.Conductors
{
  public class ComponentConductor : ConductorBase<ComponentSnapshot>
  {
    private static ILog log = LogManager.GetLogger("ComponentConductor");
    public readonly eComponentType ComponentType;

    public ComponentConductor(eComponentType componentType, eDirection direction, ComponentActor actor, ComponentHandler handler)
      : base(direction, actor, handler)
    {
      this.ComponentType = componentType;
    }

    protected override void Log(string message)
    {
      log.InfoFormat("Component:{0}, Direction:{1}, Message:{2}", ComponentType, Direction, message);
    }
  }
}