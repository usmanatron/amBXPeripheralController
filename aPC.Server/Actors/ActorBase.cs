using aPC.Common;
using aPC.Server.Engine;
using aPC.Server.Snapshots;

namespace aPC.Server.Actors
{
  public abstract class ActorBase<T> where T : SnapshotBase
  {
    protected ActorBase(IEngine xiEngine)
    {
      Engine = xiEngine;
    }

    public abstract void ActNextFrame(eDirection xiDirection, T xiSnapshot);

    protected IEngine Engine;
  }
}
