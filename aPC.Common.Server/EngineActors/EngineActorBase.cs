using aPC.Common.Server.Managers;
using aPC.Common.Server.Snapshot;
using aPC.Common.Entities;
using System.Threading;

namespace aPC.Common.Server.EngineActors
{
  public abstract class EngineActorBase
  {
    protected EngineActorBase(EngineManager xiEngine)
    {
      Engine = xiEngine;
    }

    public abstract void ActNextFrame(SnapshotBase xiData);

    protected EngineManager Engine;
  }
}
