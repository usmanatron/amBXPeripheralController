using aPC.Common;
using aPC.Common.Entities;
using aPC.Common.Server.EngineActors;
using System;

namespace aPC.Common.Server.Managers
{
  public abstract class ComponentManager : ManagerBase
  {
    public ComponentManager(eDirection xiDirection, ComponentActor xiActor, Action xiEventComplete)
      : base(xiActor, xiEventComplete)
    {
      Direction = xiDirection;
    }

    public abstract eComponentType ComponentType();
    protected eDirection Direction;
  }
}
