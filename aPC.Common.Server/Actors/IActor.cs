using aPC.Common.Server.Snapshots;

namespace aPC.Common.Server.Actors
{
  public interface IActor<T> where T : SnapshotBase
  {
    void ActNextFrame(T snapshot);
  }
}