using aPC.Common.Server.Engine;
using aPC.Common.Server.Snapshots;

namespace aPC.Common.Server.Actors
{
  public abstract class ActorBase<T> : IActor<T> where T : SnapshotBase
  {
    protected IEngine Engine;

    protected ActorBase(IEngine engine)
    {
      Engine = engine;
    }

    public abstract void ActNextFrame(eDirection direction, T snapshot);
  }
}