using aPC.Common.Entities;

namespace aPC.Common.Server.Snapshot
{
  public abstract class SnapshotBase
  {
    protected SnapshotBase(int xiFadeTime, int xiLength)
    {
      FadeTime = xiFadeTime;
      Length = xiLength;
    }

    public int FadeTime;
    public int Length;
  }
}
