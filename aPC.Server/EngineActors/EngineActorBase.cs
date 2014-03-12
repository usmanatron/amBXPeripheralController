using aPC.Common;
using aPC.Server.Engine;
using aPC.Server.Snapshots;

namespace aPC.Server.EngineActors
{
  public abstract class EngineActorBase<T> where T : SnapshotBase
  {
    protected EngineActorBase(IEngine xiEngine)
    {
      Engine = xiEngine;
    }

    public abstract void ActNextFrame(eDirection xiDirection, T xiSnapshot);

    protected IEngine Engine;
  }
}
