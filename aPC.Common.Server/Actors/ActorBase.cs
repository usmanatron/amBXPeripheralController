using aPC.Common.Server.Engine;
using aPC.Common.Server.Snapshots;

namespace aPC.Common.Server.Actors
{
  public abstract class ActorBase<T> : IActor<T> where T : SnapshotBase
  {
    protected ActorBase(IEngine xiEngine)
    {
      Engine = xiEngine;
    }

    public abstract void ActNextFrame(eDirection xiDirection, T xiSnapshot);

    protected IEngine Engine;
  }
}