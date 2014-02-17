using aPC.Common;
using aPC.Common.Server.EngineActors;
using System;

namespace aPC.Common.Server.Managers
{
  public abstract class ComponentManager : ManagerBase
  {
    public ComponentManager(EngineActorBase xiActor, Action xiEventComplete)
      : base(xiActor, xiEventComplete)
    {
    }

    public abstract eComponentType ComponentType();
  }
}
