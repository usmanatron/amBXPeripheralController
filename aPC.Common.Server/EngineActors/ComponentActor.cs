using aPC.Common.Server.Managers;
using aPC.Common.Server.Snapshot;
using System;
using aPC.Common.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aPC.Common.Server.EngineActors
{
  public abstract class ComponentActor : EngineActorBase
  {
    protected ComponentActor(eDirection xiDirection, EngineManager xiEngine)
      : base (xiEngine)
    {
      mDirection = xiDirection;
    }

    protected readonly eDirection mDirection;
  }
}
