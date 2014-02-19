using aPC.Common;
using aPC.Common.Entities;
using aPC.Common.Server.EngineActors;
using aPC.Common.Server.Snapshots;
using System;

namespace aPC.Common.Server.Managers
{
  public abstract class ComponentConductor<T> : ConductorBase<ComponentSnapshot<T>> where T : IComponent
  {
    public ComponentConductor(eDirection xiDirection, ComponentActor<T> xiActor, Action xiEventComplete)
      : base(xiActor, xiEventComplete)
    {
      Direction = xiDirection;
    }

    public abstract eComponentType ComponentType();
    protected eDirection Direction;
  }
}
