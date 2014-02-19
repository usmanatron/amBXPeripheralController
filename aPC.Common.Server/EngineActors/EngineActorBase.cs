using aPC.Common.Server.Managers;
using aPC.Common.Server.Snapshots;
using aPC.Common.Entities;
using System.Threading;

namespace aPC.Common.Server.EngineActors
{
  public abstract class EngineActorBase<T> where T : SnapshotBase
  {
    protected EngineActorBase(EngineManager xiEngine)
    {
      Engine = xiEngine;
    }

    public abstract void ActNextFrame(T xiData);

    protected EngineManager Engine;
  }
}
