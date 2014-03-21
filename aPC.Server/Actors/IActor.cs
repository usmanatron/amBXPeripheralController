using aPC.Common;
using aPC.Server.Snapshots;

namespace aPC.Server.Actors
{
  interface IActor<T> where T : SnapshotBase
  {
    void ActNextFrame(eDirection xiDirection, T xiSnapshot);
  }
}
