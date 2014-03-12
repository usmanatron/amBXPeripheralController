using aPC.Common;
using aPC.Common.Entities;
using aPC.Server.EngineActors;
using aPC.Server.Snapshots;
using aPC.Server.SceneHandlers;

namespace aPC.Server.Conductors
{
  public abstract class ComponentConductor<T> : ConductorBase<ComponentSnapshot<T>> where T : IComponent
  {
    protected ComponentConductor(eDirection xiDirection, ComponentActor<T> xiActor, ComponentHandler<T> xiHandler)
      : base(xiDirection, xiActor, xiHandler)
    {
    }

    public abstract eComponentType ComponentType();
  }
}
