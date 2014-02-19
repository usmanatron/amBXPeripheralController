using aPC.Common.Server.Managers;
using aPC.Common.Server.Snapshots;
using System;
using aPC.Common.Entities;
using System.Collections.Generic;

namespace aPC.Common.Server.EngineActors
{
  public abstract class ComponentActor<T> : EngineActorBase<ComponentSnapshot<T>> where T : Component
  {
    protected ComponentActor(eDirection xiDirection, EngineManager xiEngine)
      : base (xiEngine)
    {
      mDirection = xiDirection;
    }

    protected readonly eDirection mDirection;
  }
}
